using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Autobike.AutocarsDataSetTableAdapters;  // Подключаем адаптер для работы с базой данных

namespace Autobike
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Closes the window
        }

        // For TextBox GotFocus event
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Водительское удостоверение")
            {
                textBox.Text = "";
                textBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            }
        }

        // For TextBox LostFocus event
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Водительское удостоверение";
                textBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
            }
        }

        // For PasswordBox GotFocus event
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null && passwordBox.Password == "Пароль")
            {
                passwordBox.Password = "";
                passwordBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            }
        }

        // For PasswordBox LostFocus event
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null && string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Password = "Пароль";
                passwordBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string driverLicense = DriverLicenseTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(driverLicense) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, введите водительское удостоверение и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ClientTableAdapter clientAdapter = new ClientTableAdapter();
                EmployeeTableAdapter employeeAdapter = new EmployeeTableAdapter();

                var clients = clientAdapter.GetData();
                var employees = employeeAdapter.GetData();

                // Хэшируем введенный пароль
                string hashedPassword = HashPassword(password);

                // Проверка для клиентов
                var client = clients.FirstOrDefault(c => c.driver_license == driverLicense && c.password == hashedPassword);
                if (client != null)
                {
                    // Сохраняем ID клиента в AuthService
                    AuthService.SetCurrentUser(client.id, false);

                    UserMain userMainWindow = new UserMain();
                    userMainWindow.Show();
                    this.Close();
                    return;
                }

                // Проверка для сотрудников
                var employee = employees.FirstOrDefault(emp => emp.login == driverLicense && emp.password == hashedPassword);
                if (employee != null)
                {
                    AuthService.SetCurrentUser(employee.id, employee.is_admin);

                    if (employee.is_admin)
                    {
                        AdminMain adminMainWindow = new AdminMain();
                        adminMainWindow.Show();
                    }
                    else
                    {
                        EmployeeMain employeeMainWindow = new EmployeeMain();
                        employeeMainWindow.Show();
                    }
                    this.Close();
                    return;
                }

                MessageBox.Show("Неправильное водительское удостоверение или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при входе: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на окно регистрации (если требуется)
            MainWindow registerWindow = new MainWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
