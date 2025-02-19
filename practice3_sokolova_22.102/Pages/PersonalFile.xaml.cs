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
    /// Логика взаимодействия для PersonalFile.xaml
    /// </summary>
    public partial class PersonalFile : Page
    {
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _email;
        private string _phone;
        private string _extra_phone;
        private string _birth;
        private string _exp;
        private string _passportS;
        private string _passportN;
        private string _login;
        private string _password;

        public PersonalFile(string surname, string name, string patronymic, string email, string phone, string extra_phone, string birth, string exp, string passportS, string passportN, string login, string password)
        {
            InitializeComponent();

            _surname = surname; 
            _name = name;
            _patronymic = patronymic;
            _email = email;
            _phone = phone;
            _extra_phone = extra_phone;
            _birth = birth;
            _exp = exp;
            _passportS = passportS;
            _passportN = passportN;
            _login = login;
            _password = password;

            pSurname.Inlines.Add(new Run(_surname));
            pName.Inlines.Add(new Run(_name));
            pPatronymic.Inlines.Add(new Run(_patronymic));
            pEmail.Inlines.Add(new Run(_email));
            pPhone.Inlines.Add(new Run(_phone));
            pExtraPhone.Inlines.Add(new Run(_extra_phone));
            pBirth.Inlines.Add(new Run(_birth));
            pExp.Inlines.Add(new Run(_exp));
            pPassportS.Inlines.Add(new Run(_passportS));
            pPassportN.Inlines.Add(new Run(_passportN));
            pLogin.Inlines.Add(new Run(_login));
            pPassword.Inlines.Add(new Run(_password));
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            btnPrint.Visibility = Visibility.Hidden;

            FlowDocument doc = flowDocumentReader.Document;

            if (doc == null)
            {
                MessageBox.Show("Документ не найден");
                return;
            }

            PrintDialog pd = new PrintDialog();

            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idpSource = doc;

                pd.PrintDocument(idpSource.DocumentPaginator, "Личное дело сотрудника");
            }
        }
    }
}
