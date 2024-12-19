using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditMaintenanceWindow : Window
    {
        private MaintenanceTableAdapter maintenanceAdapter = new MaintenanceTableAdapter();
        private CarTableAdapter carAdapter = new CarTableAdapter();
        private EmployeeTableAdapter employeeAdapter = new EmployeeTableAdapter();

        // Добавляем параметры для конструктора
        private int maintenanceId;

        public EditMaintenanceWindow(int id, string carNumber, int employeeId, string problemDescription)
        {
            InitializeComponent();

            // Сохраняем id записи для последующего обновления
            maintenanceId = id;

            // Сначала загружаем список автомобилей и сотрудников
            LoadCars();
            LoadEmployees();

            // Заполняем поля на основе переданных значений
            ProblemDescriptionTextBox.Text = problemDescription;

            // Устанавливаем выбранные значения в ComboBox
            CarComboBox.SelectedValue = carNumber;
            EmployeeComboBox.SelectedValue = employeeId;
        }

        private void LoadCars()
        {
            // Получаем все автомобили из базы данных и заполняем выпадающий список
            var cars = carAdapter.GetData(); // Получаем данные о всех автомобилях
            CarComboBox.ItemsSource = cars;
            CarComboBox.DisplayMemberPath = "car_number";  // Отображаем номер автомобиля в ComboBox
            CarComboBox.SelectedValuePath = "car_number";  // Используем car_number как значение, которое будет отправлено в базу данных
        }

        private void LoadEmployees()
        {
            // Получаем всех сотрудников из базы данных и заполняем выпадающий список
            var employees = employeeAdapter.GetData(); // Получаем данные о всех сотрудниках
            EmployeeComboBox.ItemsSource = employees;
            EmployeeComboBox.DisplayMemberPath = "last_name";  // Отображаем имя сотрудника в ComboBox
            EmployeeComboBox.SelectedValuePath = "id";   // Значение, которое будет отправлено в базу данных
        }

        // Проверка формата даты
        private bool IsValidDate(string date)
        {
            string datePattern = @"^\d{4}-\d{2}-\d{2}$";  // Регулярное выражение для формата yyyy-MM-dd
            return Regex.IsMatch(date, datePattern);
        }

        // Проверка формата номера машины
        private bool IsValidCarNumber(string carNumber)
        {
            string carNumberPattern = @"^[А-Я]{1}[0-9]{3}[А-Я]{2}[0-9]{2,3}$";  // Регулярное выражение для номера автомобиля
            return Regex.IsMatch(carNumber, carNumberPattern);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем данные из текстовых полей
                string problemDescription = ProblemDescriptionTextBox.Text;
                string carNumber = CarComboBox.SelectedValue.ToString();  // Теперь получаем номер машины как строку
                int employeeId = Convert.ToInt32(EmployeeComboBox.SelectedValue);

                // Проверяем формат номера автомобиля
                if (!IsValidCarNumber(carNumber))
                {
                    MessageBox.Show("Неверный формат номера автомобиля. Пожалуйста, используйте формат [А-Я]{1}[0-9]{3}[А-Я]{2}[0-9]{2,3}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Используем id, переданное в конструктор
                int id = maintenanceId;

                // Обновляем информацию о техническом обслуживании в базе данных
                maintenanceAdapter.UpdateQuery(problemDescription, carNumber, employeeId, id);

                MessageBox.Show("Запись успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
