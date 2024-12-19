using System;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditCarBrandWindow : Window
    {
        private DataRowView carBrandRow;
        private Car_BrandTableAdapter carBrandAdapter = new Car_BrandTableAdapter();

        public EditCarBrandWindow(DataRowView selectedRow)
        {
            InitializeComponent();
            carBrandRow = selectedRow;

            // Загружаем данные выбранной марки автомобиля в поля
            NameTextBox.Text = carBrandRow["name"].ToString();
            ClassTextBox.Text = carBrandRow["class"].ToString();
            SeatsTextBox.Text = carBrandRow["seats"].ToString();
            AirConditioningCheckBox.IsChecked = (bool)carBrandRow["air_conditioning"];
            EngineCapacityTextBox.Text = carBrandRow["engine_capacity"].ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка поля Name
                string name = NameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name) || !System.Text.RegularExpressions.Regex.IsMatch(name, @"^[А-Яа-я0-9\s\-]{2,50}$"))
                {
                    MessageBox.Show("Имя должно содержать от 2 до 50 символов и соответствовать формату [А-Яа-я0-9\\s\\-].", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка поля Class
                string carClass = ClassTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(carClass) || !System.Text.RegularExpressions.Regex.IsMatch(carClass, @"^[А-Яа-я]{2,20}$"))
                {
                    MessageBox.Show("Класс должен содержать от 2 до 20 символов и состоять только из букв [А-Яа-я].", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка поля Seats
                if (!int.TryParse(SeatsTextBox.Text, out int seats) || seats <= 0)
                {
                    MessageBox.Show("Количество мест должно быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка поля Engine_capacity (опционально)
                decimal? engineCapacity = null;
                if (!string.IsNullOrEmpty(EngineCapacityTextBox.Text))
                {
                    if (!decimal.TryParse(EngineCapacityTextBox.Text, out decimal parsedCapacity) || parsedCapacity <= 0)
                    {
                        MessageBox.Show("Объем двигателя должен быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    engineCapacity = parsedCapacity;
                }

                // Проверка AirConditioning
                bool airConditioning = AirConditioningCheckBox.IsChecked.GetValueOrDefault();

                // Обновление данных в базе
                int id = (int)carBrandRow["id"];
                carBrandAdapter.UpdateQuery(name, carClass, seats, airConditioning, engineCapacity, id);

                MessageBox.Show("Марка автомобиля успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении марки автомобиля: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
