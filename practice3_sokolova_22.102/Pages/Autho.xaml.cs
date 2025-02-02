using practice3_sokolova_22._102.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace practice3_sokolova_22._102.Pages
{
    /// <summary>
    /// Класс для авторизации
    /// </summary>
    public partial class Autho : Page
    {
        private DispatcherTimer timer;
        private int remainingTime;
        int click;
        DateTime dt = DateTime.Now;
        private string code = string.Empty;
        private Customers findUser;


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public Autho()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            remainingTime = 10;
            click = 0;
        }

        /// <summary>
        /// Процедура включения (выключения) таймера
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (remainingTime >= 0)
            {
                tbTimer1.Foreground = new SolidColorBrush(Colors.Red);
                tbTimer1.FontSize = 20;
                tbTimer1.Text = remainingTime.ToString();
                remainingTime--;
            }
            if (remainingTime < 0)
            {
                UnblockElements();
                timer.Stop();
                tbTimer1.Text = string.Empty;
                timer.Tick -= Timer_Tick;

                remainingTime = 10;
                click = 0;
            }
        }

        /// <summary>
        /// Процедура перехода на страницу туров при нажатии на кнопку "Войти как гость"
        /// </summary>
        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Tours());
        }

        /// <summary>
        /// Процедура вызывающая функцию генерации капчи длины 6
        /// </summary>
        private void GenerateCapctcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;

            string capctchaText = CaptchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }

        /// <summary>
        /// Процедура входа в систему при нажатии на кнопку "Войти"
        /// </summary>
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = tbLogin.Text.Trim();
            string password = Hash.HashPassword(tbPassword.Password.Trim());
            string email = string.Empty;
            Agents agent;
            Contacts agentEmail;
            var db = Helper.GetContext();
            int h = dt.Hour;
            GenerateCode generateCode = new GenerateCode();


            if (h < 8 || h > 24)
            {
                Access();
            }
            else
            {
                UnblockElements();
                var authorization = db.Authorizations.Where(x => x.user_login == login && x.user_password == password).FirstOrDefault();
                

                if (click == 1)
                {
                    if (authorization != null)
                    {
                        findUser = db.Customers.Find(authorization.authorization_id);

                        if (findUser != null)
                        {
                            NavigationService.Navigate(new Client(authorization).tblGreet.Text = ShowGreeting(authorization));
                            MessageBox.Show("Вы вошли в систему");
                        }
                        else
                        {
                            VisibilityOn();
                            code = generateCode.CodeGenerate();
                            agent = db.Agents.FirstOrDefault(x => x.authorization_id == authorization.authorization_id);

                            if (agent != null)
                            {
                                agentEmail = db.Contacts.FirstOrDefault(x => x.contact_id == agent.contact_id);

                                if (agentEmail != null)
                                {
                                    email = agentEmail.email_address;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Турагент не найден");
                            }

                            SendEmail.SendEmailCode(email, code);
                        }                      
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели логин или пароль неверно!");
                        GenerateCapctcha();
                    }
                }
                if (click == 2)
                {
                    if (btnOffOnAutentification.Content.ToString() == "Включить двухфакторную аутентификацию")
                    {
                        NavigationService.Navigate(new Agent(authorization, tbPassword.Password));
                        MessageBox.Show("Вы вошли в систему");
                    }
                    else
                    {
                        if (tbCode.Text != code)
                        {
                            MessageBox.Show("Неверный код");
                        }
                        else 
                        { 
                            NavigationService.Navigate(new Agent(authorization, tbPassword.Password));
                            MessageBox.Show("Вы вошли в систему");
                        }
                    }                   
                }
                else if (click == 3)
                {
                    GenerateCapctcha();
                    if (authorization != null && tbCaptcha.Text == tblCaptcha.Text)
                    {
                        if (findUser != null)
                        {
                            NavigationService.Navigate(new Client(authorization).tblGreet.Text = ShowGreeting(authorization));
                            MessageBox.Show("Вы вошли в систему");
                        }
                        else
                        {
                            if (btnOffOnAutentification.Content.ToString() == "Включить двухфакторную аутентификацию")
                            {
                                NavigationService.Navigate(new Agent(authorization, tbPassword.Password));
                                MessageBox.Show("Вы вошли в систему");
                            }
                            else
                            {
                                VisibilityOn();
                                code = generateCode.CodeGenerate();
                                SendEmail.SendEmailCode(email, code);

                                if (btnOffOnAutentification.Content.ToString() == "Включить двухфакторную аутентификацию")
                                {
                                    NavigationService.Navigate(new Agent(authorization, tbPassword.Password));
                                    MessageBox.Show("Вы вошли в систему");
                                }
                                else
                                {
                                    if (tbCode.Text != string.Empty && tbCode.Text != code)
                                    {
                                        MessageBox.Show("Неверный код");
                                    }
                                    if (tbCode.Text == code)
                                    {
                                        NavigationService.Navigate(new Agent(authorization, tbPassword.Password));
                                        MessageBox.Show("Вы вошли в систему");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите данные заново!");
                        GenerateCapctcha();
                    }
                }
                else if (click == 4)
                {
                    BlockElements();
                    timer.Start();
                    timer.Tick += Timer_Tick;
                }
            }
        }

        /// <summary>
        /// Процедура блокировки элементов
        /// </summary>
        private void BlockElements()
        {
            btnEnter.IsEnabled = false;
            btnEnterGuests.IsEnabled = false;
            tbLogin.IsEnabled = false;
            tbPassword.IsEnabled = false;
            tbCaptcha.IsEnabled = false;
        }

        /// <summary>
        /// Процедура разблокировки элементов
        /// </summary>
        private void UnblockElements()
        {
            btnEnter.IsEnabled = true;
            btnEnterGuests.IsEnabled = true;
            tbLogin.IsEnabled = true;
            tbPassword.IsEnabled = true;
            tbCaptcha.IsEnabled = true;
        }

        /// <summary>
        /// Функция, выводящая вторую часть приветствия при входе в систему
        /// </summary>
        private string ShowGreeting(Authorizations autho)
        {
            /// <param name="autho">Запись об авторизации пользователя</param>
            /// <returns>Возвращает вторую часть приветствия, если клиент найден</returns>
            var db = Helper.GetContext();

            var customer = db.Customers.Where(c => c.authorization_id == autho.authorization_id).FirstOrDefault();

            if (customer != null && customer.patronymic != null)
            {
                return $"{GreetingMessage(dt)}{customer.surname} {customer.name} {customer.patronymic}!";
                
            }
            else if (customer != null)
            {
                return $"{GreetingMessage(dt)}{customer.surname} {customer.name}!";
            }
            else
            {
                return "err";
            }
        }

        /// <summary>
        /// Функция, выводящая первую часть приветствия при входе в систему в зависимости от времени суток 
        /// </summary>
        private string GreetingMessage(DateTime dt)
        {
            /// <param name="dt">Параметр представляющий текущее время</param>
            /// <returns>Возвращает первую часть приветствия в зависимости от часа</returns>
            
            int h = dt.Hour;
            
            if (h >= 10 && h < 12)
            {
                return "Доброе утро, ";
            }
            if (h >= 12 && h < 17)
            {
                return "Добрый день, ";
            }
            if (h >= 17 && h <= 19)
            {
                return "Добрый вечер, ";
            }
            return "";
        }

        /// <summary>
        /// Функция, блокирующая доступ к системе 
        /// </summary>
        private string Access()
        {
            BlockElements();
            return MessageBox.Show("В данное время доступ закрыт").ToString();
        }

        /// <summary>
        /// Процедура перехода на страницу ввода электронной почты (логина) при нажатии на кнопку "Забыли пароль?"
        /// </summary>
        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ForgotPassword());
        }

        /// <summary>
        /// Процедура, включающая видимость элемента для ввода кода
        /// </summary>
        private void VisibilityOn()
        {
            tblCode.Visibility = Visibility.Visible;
            tbCode.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Процедура, выключающая видимость элемента для ввода кода
        /// </summary>
        private void VisibilityOff()
        {
            tblCode.Visibility = Visibility.Hidden;
            tbCode.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Процедура, меняющая назначение кнопки "Отключить двухфакторную аутентификацию"
        /// </summary>
        private void btnOffOnAutentification_Click(object sender, RoutedEventArgs e)
        {
            if (btnOffOnAutentification.Content.ToString() == "Отключить двухфакторную аутентификацию")
            {
                VisibilityOff();
                btnOffOnAutentification.Content = "Включить двухфакторную аутентификацию";
            }
            else
            {
                VisibilityOn();
                btnOffOnAutentification.Content = "Отключить двухфакторную аутентификацию";
            }
        }
    }
}
