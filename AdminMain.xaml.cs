using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using Microsoft.Data.Sqlite;

namespace Autobike
{
    /// <summary>
    /// Логика взаимодействия для AdminMain.xaml
    /// </summary>
    public partial class AdminMain : Window
    {
        public AdminMain()
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

        private void FineButton_Click(object sender, RoutedEventArgs e)
        {
            FineWindow fineWindow = new FineWindow();
            fineWindow.Show();
        }

        private void DepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            DepartmentWindow departmentWindow = new DepartmentWindow();
            departmentWindow.Show();
        }

        private void TariffButton_Click(object sender, RoutedEventArgs e)
        {
            TariffWindow tariffWindow = new TariffWindow();
            tariffWindow.Show();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow employeeWindow = new EmployeeWindow();
            employeeWindow.Show();
        }

        private void CLientButton_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            clientWindow.Show();
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow();
            accountWindow.Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна с графиком
            BookingChartWindow chartWindow = new BookingChartWindow();
            chartWindow.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        private void ExportWithoutDataButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Сохранить базу данных без данных",
                    Filter = "SQLite Database|*.db",
                    FileName = "export_without_data"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string destinationPath = saveFileDialog.FileName;

                    // Указываем полный путь к файлу базы данных
                    string databasePath = @"C:\Path\To\Database\AutocarsData.db"; // Укажите точный путь

                    // Проверяем, существует ли файл
                    if (!File.Exists(databasePath))
                    {
                        MessageBox.Show($"Файл базы данных не найден по пути: {databasePath}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    using (var sourceConnection = new SqliteConnection($"Data Source={databasePath}"))
                    using (var destinationConnection = new SqliteConnection($"Data Source={destinationPath}"))
                    {
                        sourceConnection.Open();
                        destinationConnection.Open();

                        // Копируем структуру таблиц из исходной базы данных в целевую
                        using (var command = sourceConnection.CreateCommand())
                        {
                            command.CommandText = "SELECT sql FROM sqlite_master WHERE type='table';";
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string createTableSQL = reader.GetString(0);
                                    using (var createCommand = destinationConnection.CreateCommand())
                                    {
                                        createCommand.CommandText = createTableSQL;
                                        createCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }

                    MessageBox.Show("Экспорт базы данных без данных успешно выполнен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте базы данных без данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportWithDataButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
