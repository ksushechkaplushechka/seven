using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class AddInsuranceWindow : Window
    {
        // Адаптеры для работы с данными
        InsuranceTableAdapter insuranceAdapter = new InsuranceTableAdapter();
        CarTableAdapter carAdapter = new CarTableAdapter();

        public AddInsuranceWindow()
        {
            InitializeComponent();
            LoadCarNumbers(); // Загружаем номера автомобилей в ComboBox
        }

        // Метод для загрузки номеров автомобилей в ComboBox
        private void LoadCarNumbers()
        {
            var cars = carAdapter.GetData();  // Получаем данные о машинах
            var carNumbers = cars.Select(c => c.car_number).ToList(); // Извлекаем только номера машин
            CarNumberComboBox.ItemsSource = carNumbers;  // Привязываем номера машин к ComboBox
        }

        // Проверка формата даты
        private bool IsValidDate(string date)
        {
            string datePattern = @"^\d{4}-\d{2}-\d{2}$";  // Регулярное выражение для формата yyyy-MM-dd
            return Regex.IsMatch(date, datePattern);
        }

        // Обработчик для кнопки "Сохранить"
        private void SaveInsuranceButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранные значения
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            string carNumber = CarNumberComboBox.SelectedItem?.ToString();

            // Проверяем, что все поля заполнены
            if (startDate == null || endDate == null || string.IsNullOrEmpty(carNumber))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка формата даты
            if (!IsValidDate(startDate.Value.ToString("yyyy-MM-dd")) || !IsValidDate(endDate.Value.ToString("yyyy-MM-dd")))
            {
                MessageBox.Show("Неверный формат даты. Пожалуйста, используйте формат yyyy-MM-dd.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Вставляем новую запись в таблицу Insurance с помощью стандартного метода Insert
                insuranceAdapter.Insert(startDate.Value, endDate.Value, carNumber);
                MessageBox.Show("Страховка успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();  // Закрываем окно после сохранения
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении страховки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
