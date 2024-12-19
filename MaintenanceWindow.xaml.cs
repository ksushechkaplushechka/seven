using System;
using System.Data;
using System.Linq;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class MaintenanceWindow : Window
    {
        // Адаптеры для работы с таблицами Maintenance и Car
        MaintenanceTableAdapter maintenanceAdapter = new MaintenanceTableAdapter();
        CarTableAdapter carAdapter = new CarTableAdapter();  // Адаптер для получения данных о машине
        AutocarsDataSet autocarsDataSet = new AutocarsDataSet();

        public MaintenanceWindow()
        {
            InitializeComponent();
            LoadMaintenanceData();  // Загружаем данные о техническом обслуживании
        }

        private void LoadMaintenanceData()
        {
            // Заполняем данные из таблиц
            maintenanceAdapter.Fill(autocarsDataSet.Maintenance);
            carAdapter.Fill(autocarsDataSet.Car);

            // Объединяем данные из Maintenance и Car по полю car_number
            var maintenanceWithCarNumber = from maintenanceRow in autocarsDataSet.Maintenance.AsEnumerable()
                                           join carRow in autocarsDataSet.Car.AsEnumerable()
                                           on maintenanceRow.Field<string>("car_number") equals carRow.Field<string>("car_number")
                                           select new
                                           {
                                               Id = maintenanceRow.Field<int>("id"),
                                               CarNumber = carRow.Field<string>("car_number"),
                                               EmployeeId = maintenanceRow.Field<int>("employee_id"),
                                               ProblemDescription = maintenanceRow.Field<string>("problem_description")
                                           };

            // Привязываем данные к DataGrid
            MaintenanceDataGrid.ItemsSource = maintenanceWithCarNumber.ToList();

        }

        // Обработчик для добавления нового обслуживания
        private void AddMaintenanceButton_Click(object sender, RoutedEventArgs e)
        {
            AddMaintenanceWindow addMaintenanceWindow = new AddMaintenanceWindow();
            addMaintenanceWindow.ShowDialog();

            // После закрытия окна добавления, обновляем таблицу
            LoadMaintenanceData();
        }

        private void EditMaintenanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (MaintenanceDataGrid.SelectedItem != null)
            {
                // Получаем значения из выбранной строки
                var selectedRow = (dynamic)MaintenanceDataGrid.SelectedItem;

                // Передаем нужные значения в конструктор EditMaintenanceWindow
                EditMaintenanceWindow editMaintenanceWindow = new EditMaintenanceWindow(
                    selectedRow.Id,          // ID обслуживания
                    selectedRow.CarNumber,   // Номер автомобиля
                    selectedRow.EmployeeId,  // ID сотрудника
                    selectedRow.ProblemDescription  // Описание проблемы
                );

                editMaintenanceWindow.ShowDialog();

                // Обновляем таблицу после редактирования
                LoadMaintenanceData();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteMaintenanceButton_Click(object sender, RoutedEventArgs e)
        {
            if (MaintenanceDataGrid.SelectedItem != null)
            {
                var selectedRow = (dynamic)MaintenanceDataGrid.SelectedItem;
                int maintenanceId = selectedRow.Id;
                string carNumber = selectedRow.CarNumber;  // Получаем номер машины

                if (MessageBox.Show($"Вы уверены, что хотите удалить обслуживание с ID {maintenanceId}?",
                                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // Удаляем запись обслуживания
                    maintenanceAdapter.DeleteQuery(maintenanceId);

                    // Устанавливаем флаг is_in_worked для машины в true после удаления записи
                    carAdapter.UpdateIsInWorked(true, carNumber);

                    // Обновляем таблицу после удаления
                    LoadMaintenanceData();
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработчик для выбора записи в DataGrid (если нужно)
        private void MaintenanceDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Логика при выборе записи (если требуется)
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
