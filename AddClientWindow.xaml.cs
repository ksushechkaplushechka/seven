using System;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем ограничения
                if (!System.Text.RegularExpressions.Regex.IsMatch(LastNameTextBox.Text, @"^[А-Яа-я]{2,45}$"))
                {
                    MessageBox.Show("Введите корректную фамилию (2-45 символов кириллицей).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(FirstNameTextBox.Text, @"^[А-Яа-я]{2,45}$"))
                {
                    MessageBox.Show("Введите корректное имя (2-45 символов кириллицей).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(MiddleNameTextBox.Text, @"^[А-Яа-я]{2,45}$"))
                {
                    MessageBox.Show("Введите корректное отчество (2-45 символов кириллицей).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(PassportTextBox.Text, @"^[0-9]{10}$"))
                {
                    MessageBox.Show("Введите корректный номер паспорта (10 цифр).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(DriverLicenseTextBox.Text, @"^[0-9]{10}$"))
                {
                    MessageBox.Show("Введите корректный номер водительского удостоверения (10 цифр).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(AddressTextBox.Text, @"^[А-Яа-я0-9\s\.,\-]{5,100}$"))
                {
                    MessageBox.Show("Введите корректный адрес (5-100 символов кириллицей, цифрами или специальными символами).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(PhoneTextBox.Text, @"^\+7[0-9]{10}$"))
                {
                    MessageBox.Show("Введите корректный номер телефона (+7XXXXXXXXXX).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(PasswordTextBox.Password, @"^[A-Za-z0-9!@#$%^&*()]{8,20}$"))
                {
                    MessageBox.Show("Введите корректный пароль (8-20 символов, включая буквы, цифры и специальные символы).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Добавляем клиента
                ClientTableAdapter clientAdapter = new ClientTableAdapter();
                clientAdapter.InsertQuery(
                    LastNameTextBox.Text,
                    FirstNameTextBox.Text,
                    MiddleNameTextBox.Text,
                    PassportTextBox.Text,
                    DriverLicenseTextBox.Text,
                    AddressTextBox.Text,
                    PhoneTextBox.Text,
                    PasswordTextBox.Password
                );

                MessageBox.Show("Клиент добавлен успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Закрываем окно после добавления
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении клиента: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
