using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace practice3_sokolova_22._102.Pages
{
    /// <summary>
    /// Класс определения сотрудника, вошедшего в систему
    /// </summary>
    public partial class Agent : Page
    {
        Agents agent;
        string _password;

        /// <summary>
        /// Конструктор класса для инициализации элементов и нахождения сотрудника по его авторизации
        /// </summary>
        /// <param name="authorization">Авторизация сотрудника</param>
        /// <param name="password">Пароль введенный на этапе авторизации</param>
        public Agent(Authorizations authorization, string password)
        {
            InitializeComponent();

            agent = Helper.GetContext().Agents.FirstOrDefault(x=>x.authorization_id == authorization.authorization_id);
            DataContext = agent;
            _password = password;

        }

        /// <summary>
        /// Процедура перехода на страницу изменения данных сотрудника при нажатии на кнопку
        /// </summary>
        private void btnChangeAgent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeAgent(agent, _password));
        }

        private void btnPrintList_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = flowDocumentReader.Document;

            if (doc == null)
            {
                MessageBox.Show("Документ не найден");
                return;
            }

            PrintDialog pd = new PrintDialog();
            btnChangeAgent.Visibility = Visibility.Hidden;
            btnPrintList.Visibility = Visibility.Hidden;

            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idpSource = doc;

                pd.PrintDocument(idpSource.DocumentPaginator, "Список сотрудников");
            }
        }
    }
}
