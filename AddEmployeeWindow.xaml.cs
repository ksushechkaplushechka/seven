using System;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Autobike
{
    public partial class AddEmployeeWindow : Window
    {
        private EmployeeTableAdapter employeeAdapter = new EmployeeTableAdapter();
        private DepartmentTableAdapter departmentAdapter = new DepartmentTableAdapter();  // Добавляем адаптер для таблицы Department

        public AddEmployeeWindow()
        {
            InitializeComponent();
            LoadDepartments();  // Загружаем список отделов при инициализации окна
        }

        private void LoadDepartments()
        {
            // Получаем все отделы из базы данных и заполняем выпадающий список
            var departments = departmentAdapter.GetData(); // Получаем данные о всех отделах
            DepartmentComboBox.ItemsSource = departments;
            DepartmentComboBox.DisplayMemberPath = "name";  // Отображаем название отдела в ComboBox
            DepartmentComboBox.SelectedValuePath = "id";   // Значение, которое будет отправлено в базу данных
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string lastName = LastNameTextBox.Text;
                string firstName = FirstNameTextBox.Text;
                string middleName = MiddleNameTextBox.Text;
                string position = PositionTextBox.Text;
                string salaryText = SalaryTextBox.Text;
                string login = LoginTextBox.Text;
                string password = PasswordBox.Password;

                // Проверяем поля на соответствие ограничениям
                if (!System.Text.RegularExpressions.Regex.IsMatch(lastName, @"^[А-Яа-я]{2,45}$"))
                {
                    MessageBox.Show("Фамилия должна содержать от 2 до 45 символов на кириллице.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(firstName, @"^[А-Яа-я]{2,45}$"))
                {
                    MessageBox.Show("Имя должно содержать от 2 до 45 символов на кириллице.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(middleName, @"^[А-Яа-я]{2,45}$"))
                {
                    MessageBox.Show("Отчество должно содержать от 2 до 45 символов на кириллице.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(position, @"^[А-Яа-я\s]{2,50}$"))
                {
                    MessageBox.Show("Должность должна содержать от 2 до 50 символов на кириллице и может включать пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(salaryText, @"^[0-9]+$"))
                {
                    MessageBox.Show("Зарплата должна быть целым положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (DepartmentComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Выберите отдел.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(login, @"^[A-Za-z0-9_]{3,20}$"))
                {
                    MessageBox.Show("Логин должен содержать от 3 до 20 символов и может включать буквы, цифры и нижнее подчеркивание.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"^[A-Za-z0-9!@#$%^&*()]{8,20}$"))
                {
                    MessageBox.Show("Пароль должен содержать от 8 до 20 символов, включая буквы, цифры и специальные символы (!@#$%^&*()).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Получаем id выбранного отдела
                int departmentId = (int)DepartmentComboBox.SelectedValue;
                decimal salary = decimal.Parse(salaryText);

                // Хэшируем пароль
                string hashedPassword = HashPassword(password);
                bool isAdmin = IsAdminCheckBox.IsChecked.GetValueOrDefault();

                // Добавляем сотрудника в базу данных
                employeeAdapter.Insert(lastName, firstName, middleName, position, salary, departmentId, login, hashedPassword, isAdmin);
                MessageBox.Show("Сотрудник успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для хэширования пароля
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
