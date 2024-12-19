using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из полей
            string lastName = LastNameTextBox.Text;
            string firstName = FirstNameTextBox.Text;
            string middleName = MiddleNameTextBox.Text;
            string passport = PassportTextBox.Text;
            string driverLicense = DriverLicenseTextBox.Text;
            string address = AddressTextBox.Text;
            string phone = PhoneTextBox.Text;
            string password = PasswordBox.Password;

            // Валидация данных
            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(middleName) || string.IsNullOrWhiteSpace(passport) ||
                string.IsNullOrWhiteSpace(driverLicense) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Хэшируем пароль
                string hashedPassword = HashPassword(password);

                // Подключаемся к базе данных через TableAdapter
                ClientTableAdapter clientAdapter = new ClientTableAdapter();

                // Добавляем нового клиента в таблицу Client
                clientAdapter.Insert(
                    lastName,           // Фамилия
                    firstName,          // Имя
                    middleName,         // Отчество
                    passport,           // Паспорт
                    driverLicense,      // Водительское удостоверение
                    address,            // Адрес
                    phone,              // Телефон
                    hashedPassword      // Хэшированный пароль
                );

                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при регистрации: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
        // Плейсхолдеры для TextBox-ов
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        // Плейсхолдер для PasswordBox
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordPlaceholder.Visibility = Visibility.Visible;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow mainWindow = new LoginWindow();
            mainWindow.Show();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
