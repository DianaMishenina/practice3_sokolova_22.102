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
    public partial class EnterCode : Page
    {
        private string _code;
        Authorizations _authorization;
        
        public EnterCode(string code, Authorizations authorization)
        {
            InitializeComponent();
            _code = code;
            _authorization = authorization;    
            
        }

        private void btnSendCode_Click(object sender, RoutedEventArgs e)
        {
            if (tblCode.Text == _code)
            {
                NavigationService.Navigate(new CreateNewPassword(_authorization));
            }
            else
            {
                MessageBox.Show("Неверный код");
            }
        }
    }
}
