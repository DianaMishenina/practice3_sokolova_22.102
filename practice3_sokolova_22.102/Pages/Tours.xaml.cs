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
    /// Логика взаимодействия для Tours.xaml
    /// </summary>
    public partial class Tours : Page
    {
        /// <summary>
        /// Конструктор класса для инициализации элементов и получения списка туров из таблицы Tours
        /// </summary>
        public Tours()
        {
            InitializeComponent();

            var tours = Helper.GetContext().Tours.ToList();
            LVTours.ItemsSource = tours;
        }
    }
}
