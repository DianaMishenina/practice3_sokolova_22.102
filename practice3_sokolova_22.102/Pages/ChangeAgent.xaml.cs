using practice3_sokolova_22._102.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;

namespace practice3_sokolova_22._102.Pages
{
    public partial class ChangeAgent : Page
    {
        public ChangeAgent(Agents agent, string password)
        {
            InitializeComponent();

            tbPassword.Text = password;

            agent = Helper.GetContext().Agents.FirstOrDefault(x => x.authorization_id == agent.authorization_id);
            DataContext = agent;          
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
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Helper helper = new Helper();

            if (tbEmail.Text != string.Empty)
            {
                try
                {
                    Contacts contact = new Contacts { email_address = tbEmail.Text };
                    helper.UpdateEmail(contact);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (tbLogin.Text != string.Empty && tbPassword.Text != string.Empty)
            {
                try
                {
                    Authorizations authorization = new Authorizations
                    {
                        user_login = tbLogin.Text.Trim(),
                        user_password = Hash.HashPassword(tbPassword.Text.Trim())
                    };
                    helper.UpdateAuthorization(authorization);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (tbSurname.Text != string.Empty && tbName.Text != string.Empty && tbPatronymic.Text != string.Empty)
            {
                try
                {
                    Agents newAgent = new Agents { name = tbName.Text, surname = tbSurname.Text, patronymic = tbPatronymic.Text };
                    helper.UpdateAgent(newAgent);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            MessageBox.Show("Данные успешно обновлены");           
        }

        private void btnAddAgent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAgent());
        }
    }
}
