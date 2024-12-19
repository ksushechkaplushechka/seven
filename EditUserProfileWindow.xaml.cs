using Autobike.AutocarsDataSetTableAdapters;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Autobike
{
    public partial class EditUserProfileWindow : Window
    {
        public EditUserProfileWindow()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                if (AuthService.CurrentClientId == null)
                {
                    MessageBox.Show("Ошибка: Пользователь не авторизован.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.Close();
                    return;
                }

                ClientTableAdapter clientAdapter = new ClientTableAdapter();
                var clientData = clientAdapter.GetData().FirstOrDefault(c => c.id == AuthService.CurrentClientId);

                if (clientData != null)
                {
                    LastNameTextBox.Text = clientData.last_name;
                    FirstNameTextBox.Text = clientData.first_name;
                    MiddleNameTextBox.Text = clientData.middle_name;
                    PassportTextBox.Text = clientData.passport;
                    LicenseTextBox.Text = clientData.driver_license;
                    PhoneTextBox.Text = clientData.phone;
                    AddressTextBox.Text = clientData.address;

                    PasswordTextBox.Password = clientData.password;
                }
                else
                {
                    MessageBox.Show("Ошибка: Данные пользователя не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        // Проверка на правильность ввода Last_name, First_name, Middle_name
        private bool IsValidName(string name)
        {
            string pattern = @"^[А-Яа-я]{2,45}$"; // Только буквы, длина от 2 до 45 символов
            return Regex.IsMatch(name, pattern);
        }

        // Проверка на правильность ввода Passport и Driver_license
        private bool IsValidNumber(string number, int length)
        {
            string pattern = @"^[0-9]{" + length + "}$"; // Число длиной length
            return Regex.IsMatch(number, pattern);
        }

        // Проверка на правильность ввода Address
        private bool IsValidAddress(string address)
        {
            string pattern = @"^[А-Яа-я0-9\s\.,\-]{5,100}$"; // Буквы, цифры, пробелы, запятые, дефисы, длина от 5 до 100 символов
            return Regex.IsMatch(address, pattern);
        }

        // Проверка на правильность ввода Phone
        private bool IsValidPhone(string phone)
        {
            string pattern = @"^\+7[0-9]{10}$"; // Телефон с префиксом +7 и 10 цифр
            return Regex.IsMatch(phone, pattern);
        }

        // Проверка на правильность ввода password
        private bool IsValidPassword(string password)
        {
            string pattern = @"^[A-Za-z0-9!@#$%^&*()]{8,20}$"; // Пароль длиной от 8 до 20 символов, включая буквы, цифры и специальные символы
            return Regex.IsMatch(password, pattern);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AuthService.CurrentClientId == null)
                {
                    MessageBox.Show("Ошибка: Пользователь не авторизован.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string lastName = LastNameTextBox.Text;
                string firstName = FirstNameTextBox.Text;
                string middleName = MiddleNameTextBox.Text;
                string passport = PassportTextBox.Text;
                string driverLicense = LicenseTextBox.Text;
                string phone = PhoneTextBox.Text;
                string address = AddressTextBox.Text;
                string password = PasswordTextBox.Password;

                // Валидация каждого поля
                if (!IsValidName(lastName))
                {
                    MessageBox.Show("Фамилия должна содержать только русские буквы (2-45 символов).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!IsValidName(firstName))
                {
                    MessageBox.Show("Имя должно содержать только русские буквы (2-45 символов).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!IsValidName(middleName))
                {
                    MessageBox.Show("Отчество должно содержать только русские буквы (2-45 символов).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!IsValidNumber(passport, 10))
                {
                    MessageBox.Show("Паспорт должен содержать 10 цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!IsValidNumber(driverLicense, 10))
                {
                    MessageBox.Show("Водительское удостоверение должно содержать 10 цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!IsValidAddress(address))
                {
                    MessageBox.Show("Адрес должен содержать от 5 до 100 символов (буквы, цифры, пробелы, запятые, дефисы).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!IsValidPhone(phone))
                {
                    MessageBox.Show("Номер телефона должен быть в формате +7XXXXXXXXXX.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(password) && !IsValidPassword(password))
                {
                    MessageBox.Show("Пароль должен содержать от 8 до 20 символов (буквы, цифры и специальные символы).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ClientTableAdapter clientAdapter = new ClientTableAdapter();
                var clientData = clientAdapter.GetData().FirstOrDefault(c => c.id == AuthService.CurrentClientId);

                if (clientData != null)
                {
                    clientData.last_name = lastName;
                    clientData.first_name = firstName;
                    clientData.middle_name = middleName;
                    clientData.passport = passport;
                    clientData.driver_license = driverLicense;
                    clientData.phone = phone;
                    clientData.address = address;

                    // Проверка: если пароль пуст, не обновлять его в базе данных
                    if (!string.IsNullOrEmpty(password))
                    {
                        clientData.password = password; // При необходимости можно добавить хэширование пароля
                    }

                    clientAdapter.Update(clientData);
                    MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка: Данные пользователя не найдены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
