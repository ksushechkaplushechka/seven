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
using System.Windows.Shapes;

namespace Autobike
{
    /// <summary>
    /// Логика взаимодействия для EmployeeMain.xaml
    /// </summary>
    public partial class EmployeeMain : Window
    {
        public EmployeeMain()
        {
            InitializeComponent();
        }

        private void CarsButton_Click(object sender, RoutedEventArgs e)
        {
            CarWindow carWindow = new CarWindow();
            carWindow.Show();
        }

        private void CarBrandButton_Click(object sender, RoutedEventArgs e)
        {
            CarBrandWindow carBrandWindow = new CarBrandWindow();
            carBrandWindow.Show();
        }

        private void MaintenceButton_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceWindow maintenanceWindow = new MaintenanceWindow();
            maintenanceWindow.Show();
        }

        private void InsurenceButton_Click(object sender, RoutedEventArgs e)
        {
            InsuranceWindow insuranceWindow = new InsuranceWindow();
            insuranceWindow.Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }
    }
}
