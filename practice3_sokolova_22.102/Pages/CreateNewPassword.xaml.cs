using practice3_sokolova_22._102.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace practice3_sokolova_22._102.Pages
{
    public partial class CreateNewPassword : Page
    {
        private Authorizations _authorization;
        public CreateNewPassword(Authorizations authorization)
        {
            InitializeComponent();
            _authorization = authorization;
        }

        private void btnSaveNewPassword_Click(object sender, RoutedEventArgs e)
        {
            Helper helper = new Helper();

            if (tbNewPassword.Text.Trim() == tbConfirmNewPassword.Text.Trim())
            {
                string newPassword = tbNewPassword.Text.Trim();

                _authorization.user_password = Hash.HashPassword(newPassword);
                helper.UpdateAuthorization(_authorization);

                NavigationService.Navigate(new Autho());
            }
        }
    }
}
