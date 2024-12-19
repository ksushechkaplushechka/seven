using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditInsuranceWindow : Window
    {
        private InsuranceTableAdapter insuranceAdapter = new InsuranceTableAdapter();
        private CarTableAdapter carAdapter = new CarTableAdapter();

        private int insuranceId;  // ID редактируемой страховки

        public EditInsuranceWindow(int insuranceId)
        {
            InitializeComponent();
            this.insuranceId = insuranceId;
            LoadInsuranceData();
            LoadCarNumbers();
        }

        // Метод для загрузки данных о страховке
        private void LoadInsuranceData()
        {
            var insurance = insuranceAdapter.GetData().FirstOrDefault(i => i.id == insuranceId);

            if (insurance != null)
            {
                StartDatePicker.SelectedDate = insurance.start_date;
                EndDatePicker.SelectedDate = insurance.end_date;
                CarNumberComboBox.SelectedItem = insurance.car_number;  // Заполняем номер машины
            }
            else
            {
                MessageBox.Show("Страховка не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
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

        private void SaveInsuranceButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            string carNumber = CarNumberComboBox.SelectedItem?.ToString();

            // Проверка на заполненность всех полей
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
                // Преобразуем DateTime в строки (если дата не пуста)
                string startDateString = startDate?.ToString("yyyy-MM-dd");
                string endDateString = endDate?.ToString("yyyy-MM-dd");

                // Обновляем запись о страховке в базе данных
                insuranceAdapter.UpdateQuery(startDateString, endDateString, carNumber, insuranceId);

                MessageBox.Show("Страховка успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();  // Закрываем окно после обновления
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении страховки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
