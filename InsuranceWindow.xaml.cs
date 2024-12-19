using System;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;  // Подключаем адаптер для работы с базой данных
using System.Data;
using System.Linq;
using System.Windows.Controls;

namespace Autobike
{
    public partial class InsuranceWindow : Window
    {
        // Создаем адаптер для работы с таблицей Insurance
        InsuranceTableAdapter insuranceAdapter = new InsuranceTableAdapter();
        CarTableAdapter carAdapter = new CarTableAdapter();  // Для получения car_number
        AutocarsDataSet autocarsDataSet = new AutocarsDataSet();

        public InsuranceWindow()
        {
            InitializeComponent();
            LoadInsuranceData(); // Загружаем данные при открытии окна
            LoadCarNumbers(); // Загружаем список номеров автомобилей
        }

        // Метод для загрузки данных о страховках
        private void LoadInsuranceData()
        {
            insuranceAdapter.Fill(autocarsDataSet.Insurance);  // Заполняем набор данных
            InsuranceDataGrid.ItemsSource = autocarsDataSet.Insurance.DefaultView;  // Привязываем данные к DataGrid
        }

        // Метод для загрузки номеров автомобилей в ComboBox
        private void LoadCarNumbers()
        {
            var cars = carAdapter.GetData();  // Получаем данные о машинах
            var carNumbers = cars.Select(c => c.car_number).ToList(); // Извлекаем только номера машин
            foreach (var column in InsuranceDataGrid.Columns.OfType<DataGridComboBoxColumn>())
            {
                column.ItemsSource = carNumbers; // Привязываем номера машин к ComboBoxColumn
            }
        }

        // Обработчик для добавления новой страховки
        private void AddInsuranceButton_Click(object sender, RoutedEventArgs e)
        {
            AddInsuranceWindow addInsuranceWindow = new AddInsuranceWindow();
            addInsuranceWindow.ShowDialog();

            // После закрытия окна добавления, обновляем таблицу
            LoadInsuranceData();
        }

        // Обработчик для редактирования выбранной страховки
        private void EditInsuranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InsuranceDataGrid.SelectedItem != null)
            {
                // Получаем выбранную строку
                DataRowView selectedRow = (DataRowView)InsuranceDataGrid.SelectedItem;

                // Извлекаем ID страховки из выбранной строки
                if (selectedRow["id"] != DBNull.Value)
                {
                    int insuranceId = Convert.ToInt32(selectedRow["id"]);

                    // Передаем ID страховки в окно редактирования
                    EditInsuranceWindow editInsuranceWindow = new EditInsuranceWindow(insuranceId);
                    editInsuranceWindow.ShowDialog();

                    // После закрытия окна редактирования, обновляем таблицу
                    LoadInsuranceData();
                }
                else
                {
                    MessageBox.Show("Не удалось получить ID страховки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите страховку для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработчик для удаления выбранной страховки
        private void DeleteInsuranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (InsuranceDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)InsuranceDataGrid.SelectedItem;
                int insuranceId = Convert.ToInt32(selectedRow["id"]);

                // Подтверждение удаления
                if (MessageBox.Show($"Вы уверены, что хотите удалить страховку с ID {insuranceId}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // Выполнение удаления
                    insuranceAdapter.DeleteQuery(insuranceId);

                    LoadInsuranceData();  // Обновляем таблицу после удаления
                }
            }
            else
            {
                MessageBox.Show("Выберите страховку для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Выбор строки в DataGrid
        private void InsuranceDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Логика для выбора, если необходимо
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
