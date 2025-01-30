using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
using practice3_sokolova_22._102.Services;

namespace practice3_sokolova_22._102.Pages
{
    public partial class ForgotPassword : Page
    { 
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void btnSendCode_Click(object sender, RoutedEventArgs e)
        {
            var db = Helper.GetContext();

            string loginOrEmail = tblEmailLogin.Text.Trim();
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string email = string.Empty;
            var agentAutho = (Authorizations)null;

            if (!Regex.IsMatch(loginOrEmail, emailPattern))
            {
                agentAutho = db.Authorizations.FirstOrDefault(x => x.user_login == loginOrEmail);

                if (agentAutho != null)
                {
                    var agent = db.Agents.FirstOrDefault(x => x.authorization_id == agentAutho.authorization_id);

                    if (agent != null)
                    {
                        var agentEmail = db.Contacts.FirstOrDefault(x => x.contact_id == agent.contact_id);

                        if (agentEmail != null)
                        {
                            email = agentEmail.email_address;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Авторизация не найдена");
                }
            }
            else
            {
                email = loginOrEmail;
            }

            GenerateCode generateCode = new GenerateCode();
            string code = generateCode.CodeGenerate();

            SendEmail.SendEmailAsync(email, code).GetAwaiter();

            NavigationService.Navigate(new EnterCode(code, agentAutho));
        }
    }
}


