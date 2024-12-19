using System;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditEmployeeWindow : Window
    {
        private DataRowView employeeRow;
        private EmployeeTableAdapter employeeAdapter = new EmployeeTableAdapter();
        private DepartmentTableAdapter departmentAdapter = new DepartmentTableAdapter();  // Добавляем адаптер для таблицы Department

        public EditEmployeeWindow(DataRowView selectedRow)
        {
            InitializeComponent();
            employeeRow = selectedRow;

            // Загружаем данные выбранного сотрудника в поля
            LastNameTextBox.Text = employeeRow["last_name"].ToString();
            FirstNameTextBox.Text = employeeRow["first_name"].ToString();
            MiddleNameTextBox.Text = employeeRow["middle_name"].ToString();
            PositionTextBox.Text = employeeRow["position"].ToString();
            SalaryTextBox.Text = employeeRow["salary"].ToString();
            LoginTextBox.Text = employeeRow["login"].ToString();
            IsAdminCheckBox.IsChecked = (bool)employeeRow["is_admin"];

            // Загружаем список отделов в ComboBox
            LoadDepartments();

            // Устанавливаем выбранный отдел в ComboBox
            int departmentId = (int)employeeRow["department_id"];
            DepartmentComboBox.SelectedValue = departmentId;
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

                // Получаем id выбранного отдела
                int departmentId = (int)DepartmentComboBox.SelectedValue;
                decimal salary = decimal.Parse(salaryText);
                int id = (int)employeeRow["id"];

                // Используем текущий пароль из базы данных
                string password = employeeRow["password"].ToString();
                bool isAdmin = IsAdminCheckBox.IsChecked.GetValueOrDefault();

                // Обновляем информацию о сотруднике в базе данных
                employeeAdapter.UpdateQuery(lastName, firstName, middleName, position, salary, departmentId, login, password, isAdmin, id);

                MessageBox.Show("Сотрудник успешно обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
