using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
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
            StringBuilder sb = new StringBuilder();

            long contact_id = 0;
            long authorization_id = 0;
            long document_id = 0;
            int gender_id = 0;

            try
            {
                Contacts contact = new Contacts
                {
                    email_address = tbEmail.Text,
                    phone_number = tbPhone.Text,
                    extra_phone_number = tbExtraPhone.Text
                };

                Documents document = new Documents
                {
                    passport_series = int.Parse(tbPassportSeries.Text),
                    passport_number = int.Parse(tbPassportNumber.Text)
                };

                Authorizations authorization = new Authorizations
                {
                    user_login = tbLogin.Text.Trim(),
                    user_password = tbPassword.Text.Trim(),
                };

                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var contactValid = !Validator.TryValidateObject(contact, new ValidationContext(contact), results, true);
                var documentValid = !Validator.TryValidateObject(document, new ValidationContext(document), results, true);
                var authValid = !Validator.TryValidateObject(authorization, new ValidationContext(authorization), results, true);

                if (contactValid || documentValid || authValid)
                {
                    foreach (var error in results)
                    {
                        sb.AppendLine(error.ErrorMessage);
                    }
                }
                if (sb.Length > 0) 
                {
                    MessageBox.Show(sb.ToString());
                    return;
                }

                helper.AddEmail(contact);
                helper.AddDocument(document);
                helper.AddAuthorization(authorization);

                contact_id = Convert.ToInt32(helper.GetContactId(contact));
                document_id = Convert.ToInt32(helper.GetDocumentId(document));
                authorization_id = Convert.ToInt32(helper.GetAuthorizationId(authorization));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //public void SB(List<System.ComponentModel.DataAnnotations.ValidationResult> results)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var error in results)
        //    {
        //        sb.AppendLine(error.ErrorMessage);
        //    }
        //    MessageBox.Show(sb.ToString());
        //}
    }
}
