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
    public partial class AddAgent : Page
    {
        public AddAgent()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbSurname.Clear();
            tbName.Clear();
            tbPatronymic.Clear();
            tbPhone.Clear();
            tbExtraPhone.Clear();
            tbEmail.Clear();
            tbLogin.Clear();
            tbPassword.Clear();
            //dpBirthday.ClearValue();
            rbMale.IsChecked = true;
            tbExp.Clear();
            tbPassportSeries.Clear();
            tbPassportNumber.Clear();
        }

        private void btnAddAgent_Click(object sender, RoutedEventArgs e)
        {
            Helper helper = new Helper();
            var db = Helper.GetContext();

            long contact_id = 0;
            long authorization_id = 0;
            long document_id = 0;
            int gender_id = 0;

            try
            {
                if (tbEmail.Text != string.Empty && tbPhone.Text != string.Empty)
                {
                    Contacts contact = new Contacts
                    {
                        email_address = tbEmail.Text,
                        phone_number = tbPhone.Text,
                        extra_phone_number = tbExtraPhone.Text
                    };
                    helper.AddEmail(contact);

                    contact_id = Convert.ToInt32(helper.GetContactId(contact));
                }
                else
                {
                    MessageBox.Show("Не заполнены поля телефон и/или электронная почта");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            try
            {
                if (tbLogin.Text != string.Empty && tbPassword.Text != string.Empty)
                {
                    Authorizations authorization = new Authorizations
                    {
                        user_login = tbLogin.Text.Trim(),
                        user_password = tbPassword.Text.Trim(),
                    };
                    helper.AddAuthorization(authorization);

                    authorization_id = Convert.ToInt32(helper.GetAuthorizationId(authorization));
                }
                else
                {
                    MessageBox.Show("Не заполнены поля логин и/или пароль");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            try
            {
                if (tbPassportSeries.Text != string.Empty && tbPassportNumber.Text != string.Empty)
                {
                    Documents document = new Documents
                    {
                        passport_series = int.Parse(tbPassportSeries.Text),
                        passport_number = int.Parse(tbPassportNumber.Text)
                    };
                    helper.AddDocument(document);

                    document_id = Convert.ToInt32(helper.GetDocumentId(document));
                }
                else
                {
                    MessageBox.Show("Не заполнены поля серия паспорта и/или номер паспорта");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            try
            {
                if (tbSurname.Text != string.Empty && tbName.Text != string.Empty && tbExp.Text != string.Empty
                        && dpBirthday.Text != string.Empty)
                {

                    if (rbMale.IsChecked == true)
                    {
                        gender_id = 1;
                    }
                    else
                    {
                        gender_id = 2;
                    }

                    Agents newAgent = new Agents
                    {
                        name = tbName.Text,
                        surname = tbSurname.Text,
                        patronymic = tbPatronymic.Text,
                        experience = int.Parse(tbExp.Text),
                        birthday = Convert.ToDateTime(dpBirthday.Text),
                        gender_id = gender_id,
                        contact_id = contact_id,
                        document_id = document_id,
                        authorization_id = authorization_id
                    };
                    helper.AddAgent(newAgent);

                }
                else
                {
                    MessageBox.Show("Не заполнены все или некоторые поля: Фамилия, Имя, Опыт работы, Дата рождения");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Новый турагент успешно добавлен");
        }
    }
}
