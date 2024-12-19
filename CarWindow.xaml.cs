using System;
using System.Windows;
using System.Data;
using Autobike.AutocarsDataSetTableAdapters; // Подключаем адаптер для работы с базой данных

namespace Autobike
{
    public partial class CarWindow : Window
    {
        CarTableAdapter carAdapter = new CarTableAdapter();
        AutocarsDataSet autocarsDataSet = new AutocarsDataSet();

        public CarWindow()
        {
            InitializeComponent();
            LoadCarData();
        }

        private void LoadCarData()
        {
            carAdapter.Fill(autocarsDataSet.Car);
            CarDataGrid.ItemsSource = autocarsDataSet.Car.DefaultView;
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            AddCarWindow addCarWindow = new AddCarWindow();
            addCarWindow.ShowDialog();
            LoadCarData();
        }

        private void EditCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarDataGrid.SelectedItem != null)
            {
                // Получаем выбранную строку
                DataRowView selectedRow = (DataRowView)CarDataGrid.SelectedItem;

                // Открываем окно редактирования с выбранной строкой
                EditCarWindow editCarWindow = new EditCarWindow(selectedRow);
                editCarWindow.ShowDialog();

                // Обновляем данные после редактирования
                LoadCarData();
            }
            else
            {
                MessageBox.Show("Выберите машину для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)CarDataGrid.SelectedItem;
                string carNumber = selectedRow["car_number"].ToString();

                if (MessageBox.Show($"Вы уверены, что хотите удалить машину с номером {carNumber}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    carAdapter.DeleteQuery(carNumber);
                    LoadCarData();
                }
            }
            else
            {
                MessageBox.Show("Выберите машину для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CarDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Код для обработки выбора строки, если требуется дополнительная логика
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
