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
    public partial class Agent : Page
    {
        Agents agent;

        public Agent(Authorizations authorization)
        {
            InitializeComponent();

            agent = Helper.GetContext().Agents.FirstOrDefault(x=>x.authorization_id == authorization.authorization_id);
            DataContext = agent;

        }

        private void btnChangeAgent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeAgent(agent));
        }
    }
}
