using System;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditCarWindow : Window
    {
        private DataRowView selectedCar;
        private CarTableAdapter carAdapter = new CarTableAdapter();
        private EmployeeTableAdapter employeeAdapter = new EmployeeTableAdapter();
        private Car_BrandTableAdapter brandAdapter = new Car_BrandTableAdapter();
        private TariffTableAdapter tariffAdapter = new TariffTableAdapter();

        public EditCarWindow(DataRowView carData)
        {
            InitializeComponent();
            selectedCar = carData;

            // Загружаем данные для ComboBox
            LoadComboBoxData();

            // Заполняем поля текущими значениями
            LoadCarData();
        }

        // Метод для загрузки данных в ComboBox
        private void LoadComboBoxData()
        {
            // Загрузка сотрудников
            EmployeeComboBox.ItemsSource = employeeAdapter.GetData();
            // Загрузка марок автомобилей
            BrandComboBox.ItemsSource = brandAdapter.GetData();
            // Загрузка тарифов
            TariffComboBox.ItemsSource = tariffAdapter.GetData();
        }

        private void LoadCarData()
        {
            CarNumberTextBox.Text = selectedCar["car_number"].ToString();
            BrandComboBox.SelectedValue = selectedCar["brand_id"];
            EmployeeComboBox.SelectedValue = selectedCar["employee_id"];
            YearTextBox.Text = selectedCar["year_of_manufacture"].ToString();
            TariffComboBox.SelectedValue = selectedCar["tariff_id"];

            // Устанавливаем значение для is_booked
            if (selectedCar["is_booked"] != DBNull.Value)
            {
                IsBookedCheckBox.IsChecked = Convert.ToBoolean(selectedCar["is_booked"]);
            }
            else
            {
                IsBookedCheckBox.IsChecked = false;  // Значение по умолчанию, если NULL
            }

            // Устанавливаем значение для is_in_worked
            if (selectedCar["is_in_worked"] != DBNull.Value)
            {
                IsWorkCheckBox.IsChecked = Convert.ToBoolean(selectedCar["is_in_worked"]);
            }
            else
            {
                IsWorkCheckBox.IsChecked = false;  // Значение по умолчанию, если NULL
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка Car_number
                string carNumber = CarNumberTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(carNumber) || !System.Text.RegularExpressions.Regex.IsMatch(carNumber, @"^[а-я]{1}[0-9]{3}[а-я]{2}$"))
                {
                    MessageBox.Show("Введите корректный номер машины в формате [а-я][0-9]{3}[а-я]{2}, используя только строчные буквы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка Employee_id
                if (EmployeeComboBox.SelectedValue == null || !int.TryParse(EmployeeComboBox.SelectedValue.ToString(), out int employeeId))
                {
                    MessageBox.Show("Выберите корректного сотрудника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка Brand_id
                if (BrandComboBox.SelectedValue == null || !int.TryParse(BrandComboBox.SelectedValue.ToString(), out int brandId))
                {
                    MessageBox.Show("Выберите корректную марку машины.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка Year_of_manufacture
                if (string.IsNullOrWhiteSpace(YearTextBox.Text) || !int.TryParse(YearTextBox.Text, out int yearOfManufacture) || yearOfManufacture < 1886 || yearOfManufacture > DateTime.Now.Year)
                {
                    MessageBox.Show($"Введите корректный год выпуска (между 1886 и {DateTime.Now.Year}).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка Tariff_id
                if (TariffComboBox.SelectedValue == null || !int.TryParse(TariffComboBox.SelectedValue.ToString(), out int tariffId))
                {
                    MessageBox.Show("Выберите корректный тариф.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка Is_booked
                bool isBooked = IsBookedCheckBox.IsChecked ?? false;

                // Проверка Is_in_worked
                bool isWork = IsWorkCheckBox.IsChecked ?? false;

                // Обновление данных
                string originalCarNumber = selectedCar["car_number"].ToString();
                carAdapter.UpdateQuery(carNumber, employeeId, brandId, yearOfManufacture, tariffId, isBooked, isWork, originalCarNumber);

                MessageBox.Show("Машина успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
