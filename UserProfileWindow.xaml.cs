using Autobike.AutocarsDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Autobike
{
    public partial class UserProfileWindow : Window
    {
        public UserProfileWindow()
        {
            InitializeComponent();
            LoadUserData(); // Загрузка данных пользователя
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
                    PasswordTextBox.Password = clientData.password; // Отображаем хэш
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Открыть окно для редактирования данных пользователя
            EditUserProfileWindow editWindow = new EditUserProfileWindow();
            editWindow.ShowDialog();
            LoadUserData(); // Обновляем данные после редактирования
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthService.CurrentClientId == null)
            {
                MessageBox.Show("Ошибка: Пользователь не авторизован.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить аккаунт?", "Удаление аккаунта", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    ClientTableAdapter clientAdapter = new ClientTableAdapter();
                    clientAdapter.DeleteQuery(AuthService.CurrentClientId.Value);
                    MessageBox.Show("Аккаунт успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Логаут после удаления аккаунта
                    AuthService.Logout();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении аккаунта: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
