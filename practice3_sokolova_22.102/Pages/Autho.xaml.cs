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
    public partial class Autho : Page
    {
        private DispatcherTimer timer;
        private int remainingTime;
        int click;
        DateTime dt = DateTime.Now;


        public Autho()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            remainingTime = 10;
            click = 0;
        }

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

        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Tours());
        }

        private void GenerateCapctcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;

            string capctchaText = CaptchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }


        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Password.Trim();

            var db = Helper.GetContext();
            int h = dt.Hour;


            if (h < 8 || h > 24)
            {
                Access();
            }
            else
            {
                UnblockElements();
                var authorization = db.Authorizations.Where(x => x.user_login == login && x.user_password == password).FirstOrDefault();
                var findUser = db.Customers.Find(authorization.authorization_id);

                if (click == 1)
                {
                    if (authorization != null)
                    {                     
                        if (findUser != null)
                        {
                            NavigationService.Navigate(new Client(authorization).tblGreet.Text = ShowGreeting(authorization));
                            MessageBox.Show("Вы вошли в систему");
                        }
                        else
                        {
                            NavigationService.Navigate(new Agent(authorization));
                            MessageBox.Show("Вы вошли в систему");
                        }                      
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели логин или пароль неверно!");
                        GenerateCapctcha();
                    }
                }
                else if (click > 1 && click < 3)
                {
                    if (authorization != null && tbCaptcha.Text == tblCaptcha.Text)
                    {
                        if (findUser != null)
                        {
                            NavigationService.Navigate(new Client(authorization).tblGreet.Text = ShowGreeting(authorization));
                            MessageBox.Show("Вы вошли в систему");
                        }
                        else
                        {
                            NavigationService.Navigate(new Agent(authorization));
                            MessageBox.Show("Вы вошли в систему");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите данные заново!");
                        GenerateCapctcha();
                    }
                }
                else if (click == 3)
                {
                    BlockElements();
                    timer.Start();
                    timer.Tick += Timer_Tick;
                }
            }
        }

        private void BlockElements()
        {
            btnEnter.IsEnabled = false;
            btnEnterGuests.IsEnabled = false;
            tbLogin.IsEnabled = false;
            tbPassword.IsEnabled = false;
            tbCaptcha.IsEnabled = false;
        }

        private void UnblockElements()
        {
            btnEnter.IsEnabled = true;
            btnEnterGuests.IsEnabled = true;
            tbLogin.IsEnabled = true;
            tbPassword.IsEnabled = true;
            tbCaptcha.IsEnabled = true;
        }

        private string ShowGreeting(Authorizations autho)
        {
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

        private string GreetingMessage(DateTime dt)
        {
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

        private string Access()
        {
            BlockElements();
            return MessageBox.Show("В данное время доступ закрыт").ToString();
        }
    }
}
