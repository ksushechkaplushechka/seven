using System;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class AddCarWindow : Window
    {
        private CarTableAdapter carAdapter = new CarTableAdapter();
        private EmployeeTableAdapter employeeAdapter = new EmployeeTableAdapter();
        private Car_BrandTableAdapter carBrandAdapter = new Car_BrandTableAdapter();
        private TariffTableAdapter tariffAdapter = new TariffTableAdapter();

        public AddCarWindow()
        {
            InitializeComponent();

            // Заполняем выпадающие списки данными
            EmployeeComboBox.ItemsSource = employeeAdapter.GetData();
            BrandComboBox.ItemsSource = carBrandAdapter.GetData();
            TariffComboBox.ItemsSource = tariffAdapter.GetData();
        }

        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка Car_number
                string carNumber = CarNumberTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(carNumber) || !System.Text.RegularExpressions.Regex.IsMatch(carNumber, @"^[А-Я]{1}[0-9]{3}[А-Я]{2}[0-9]{2,3}$"))
                {
                    MessageBox.Show("Введите корректный номер машины в формате [А-Я][0-9]{3}[А-Я]{2}[0-9]{2,3}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (string.IsNullOrWhiteSpace(YearOfManufactureTextBox.Text) || !int.TryParse(YearOfManufactureTextBox.Text, out int yearOfManufacture) || yearOfManufacture < 1886 || yearOfManufacture > DateTime.Now.Year)
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

                string isBooked = IsBookedCheckBox.IsChecked == true ? "1" : "0";
                string isWork = IsWorkCheckBox.IsChecked == true ? "1" : "0";

                // Вставка данных
                carAdapter.InsertQuery(carNumber, employeeId, brandId, yearOfManufacture, tariffId, isBooked, isWork);

                MessageBox.Show("Машина успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении машины: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
