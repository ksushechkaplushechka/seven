using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class AddMaintenanceWindow : Window
    {
        private MaintenanceTableAdapter maintenanceAdapter = new MaintenanceTableAdapter();
        private CarTableAdapter carAdapter = new CarTableAdapter();
        private EmployeeTableAdapter employeeAdapter = new EmployeeTableAdapter();

        public AddMaintenanceWindow()
        {
            InitializeComponent();
            LoadCars();         // Загружаем список автомобилей
            LoadEmployees();    // Загружаем список сотрудников
        }

        private void LoadCars()
        {
            var cars = carAdapter.GetData();
            CarComboBox.ItemsSource = cars;
            CarComboBox.DisplayMemberPath = "car_number";  // Показываем номер автомобиля
            CarComboBox.SelectedValuePath = "car_number";  // car_number будет использоваться для идентификации в базе данных
        }

        private void LoadEmployees()
        {
            // Загружаем всех сотрудников
            var employees = employeeAdapter.GetData();

            // Фильтруем сотрудников, исключая тех, у кого is_admin == true
            var filteredEmployees = from employee in employees
                                    where employee.is_admin == false
                                    select employee;

            // Заполняем ComboBox только разрешенными сотрудниками
            EmployeeComboBox.ItemsSource = filteredEmployees;
            EmployeeComboBox.DisplayMemberPath = "middle_name";  // Показываем имя сотрудника
            EmployeeComboBox.SelectedValuePath = "id";   // id будет использоваться для идентификации в базе данных
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

        private void SaveMaintenanceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string problemDescription = ProblemDescriptionTextBox.Text;

                // Получаем номер выбранного автомобиля и id сотрудника
                string carNumber = (string)CarComboBox.SelectedValue;  // Получаем номер машины как строку
                int employeeId = (int)EmployeeComboBox.SelectedValue;  // Получаем id сотрудника

                // Проверяем формат номера автомобиля
                if (!IsValidCarNumber(carNumber))
                {
                    MessageBox.Show("Неверный формат номера автомобиля. Пожалуйста, используйте формат [А-Я]{1}[0-9]{3}[А-Я]{2}[0-9]{2,3}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Вставляем новое техническое обслуживание в таблицу
                maintenanceAdapter.InsertQuery(problemDescription, carNumber, employeeId);  // Передаем номер машины вместо id
                MessageBox.Show("Запись успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Обновляем поле is_in_worked для соответствующего автомобиля
                carAdapter.UpdateIsInWorked(false, carNumber);  // Устанавливаем значение true для is_in_worked

                this.Close();  // Закрываем окно после добавления записи
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие окна
        }
    }
}
