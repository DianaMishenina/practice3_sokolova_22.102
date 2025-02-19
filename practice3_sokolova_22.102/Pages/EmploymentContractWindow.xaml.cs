using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Words.NET;

namespace practice3_sokolova_22._102.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmploymentContractWindow.xaml
    /// </summary>
    public partial class EmploymentContractWindow : Window
    {
        Agents _agent;
        string _passportS;
        string _passportN;
        
        public EmploymentContractWindow(Agents agent, string passportS, string passportN)
        {
            InitializeComponent();
            _agent = agent;
            _passportS = passportS;
            _passportN = passportN;

            try
            {
                EmployeeNameTextBlock.Text = _agent.surname + " " + _agent.name + " " + _agent.patronymic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            DepartmentTextBlock.Text = "Туризм";
            EmployeePositionTextBlock.Text = "Турагент";
        }

        private long GenerateINN()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var items = new Dictionary<string, string>()
            {
                {"{ContractNumber}", ContractNumberTextBox.Text},
                {"{City}", CityTextBox.Text},
                {"{ContractDay}", ContractDayTextBox.Text},
                {"{ContractMonth}", ContractMonthTextBox.Text},
                {"{ContractYear}", ContractYearTextBox.Text},
                {"{EmployerName}", EmployerNameTextBox.Text},
                {"{CEOName}", CEONameTextBox.Text},
                {"{EmployeeName}", EmployeeNameTextBlock.Text},
                {"{EmployeePosition}", EmployeePositionTextBlock.Text},
                {"{DepartmentName}", DepartmentTextBlock.Text},
                {"{TestPeriod}", TestPeriodTextBox.Text},
                {"{EmployeeSalary}", EmployeeSalaryTextBox.Text},
                {"{PassportS}", _passportS},
                {"{PassportN}", _passportN},
                {"{GovernmentAgency}", "ГУ МВД"},
                {"{Address}", "г. Новосибирск, ул. Красный проспект, 1"},
                {"{INN_KPP}", GenerateINN().ToString()}
            };

            string templatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "blank-trudovogo-dogovora.docx");
            string outputPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
$"Трудовой_договор.docx");

            using (var doc = DocX.Load(templatePath))
            {
                foreach (var item in items)
                {
                    foreach (var paragraph in doc.Paragraphs)
                    {
                        if (paragraph.Text.Contains(item.Key))
                        {
                            paragraph.ReplaceText(item.Key, item.Value);
                        }
                    }
                }
                try
                {
                    doc.SaveAs(outputPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            MessageBox.Show("Трудовой договор сформирован");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
