using System;
using System.Linq;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditClientWindow : Window
    {
        private int clientId;  // ID клиента

        // Конструктор для инициализации и загрузки данных
        public EditClientWindow(int id)
        {
            InitializeComponent();
            clientId = id;
            LoadClientData();  // Загружаем данные клиента
        }

        // Загрузка данных клиента по ID
        private void LoadClientData()
        {
            try
            {
                // Используем адаптер для получения данных клиента
                ClientTableAdapter clientAdapter = new ClientTableAdapter();
                var client = clientAdapter.GetData().FirstOrDefault(c => c.id == clientId);

                if (client != null)
                {
                    // Заполняем поля
                    LastNameTextBox.Text = client.last_name;
                    FirstNameTextBox.Text = client.first_name;
                    MiddleNameTextBox.Text = client.middle_name;
                    PassportTextBox.Text = client.passport;
                    DriverLicenseTextBox.Text = client.driver_license;
                    AddressTextBox.Text = client.address;
                    PhoneTextBox.Text = client.phone;
                }
                else
                {
                    MessageBox.Show("Клиент не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных клиента: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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

                // Обновляем данные клиента
                ClientTableAdapter clientAdapter = new ClientTableAdapter();
                clientAdapter.UpdateQuery(
                    LastNameTextBox.Text,
                    FirstNameTextBox.Text,
                    MiddleNameTextBox.Text,
                    PassportTextBox.Text,
                    DriverLicenseTextBox.Text,
                    AddressTextBox.Text,
                    PhoneTextBox.Text,
                    clientAdapter.GetData().FirstOrDefault(c => c.id == clientId).password,
                    clientId
                );

                MessageBox.Show("Данные клиента обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Закрываем окно после сохранения
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных клиента: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик нажатия кнопки "Отмена"
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();  // Просто закрыть окно без сохранения
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
