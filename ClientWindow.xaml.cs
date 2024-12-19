using System;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;
using System.Linq;
using System.Windows.Media;
using System.Data;


namespace Autobike
{
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
            LoadClients();
        }

        // Обработчик изменения выбора клиента в DataGrid
        private void ClientDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ClientDataGrid.SelectedItem != null)
            {
                var selectedClientRowView = ClientDataGrid.SelectedItem as System.Data.DataRowView;

                if (selectedClientRowView != null)
                {
                    try
                    {
                        // Извлекаем данные из DataRowView
                        int clientId = (int)selectedClientRowView["id"];
                        string lastName = selectedClientRowView["last_name"] as string;
                        string firstName = selectedClientRowView["first_name"] as string;

                        // Для демонстрации: выводим выбранного клиента в консоль
                        Console.WriteLine($"Выбран клиент с ID: {clientId}, Фамилия: {lastName}, Имя: {firstName}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при извлечении данных о выбранном клиенте: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось извлечь данные о выбранном клиенте.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Загрузка списка клиентов в DataGrid
        private void LoadClients()
        {
            try
            {
                // Используем адаптер для получения данных
                ClientTableAdapter clientAdapter = new ClientTableAdapter();
                var clients = clientAdapter.GetData();
                ClientDataGrid.ItemsSource = clients;  // Привязываем данные к DataGrid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке клиентов: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для добавления нового клиента
        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();  // Открываем окно добавления клиента
            LoadClients();  // Перезагружаем список клиентов после добавления
        }

        // Обработчик для редактирования клиента
        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientDataGrid.SelectedItem != null)
            {
                // Получаем выбранного клиента
                var selectedClient = ClientDataGrid.SelectedItem as System.Data.DataRowView;

                if (selectedClient != null)
                {
                    try
                    {
                        int clientId = (int)selectedClient["id"];

                        // Передаем ID клиента в окно редактирования
                        EditClientWindow editClientWindow = new EditClientWindow(clientId);
                        editClientWindow.ShowDialog();
                        LoadClients(); // Перезагружаем список клиентов после редактирования
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при редактировании клиента: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для удаления клиента
        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientDataGrid.SelectedItem != null)
            {
                var selectedClient = ClientDataGrid.SelectedItem as System.Data.DataRowView;

                if (selectedClient != null)
                {
                    try
                    {
                        int clientId = (int)selectedClient["id"];

                        ClientTableAdapter clientAdapter = new ClientTableAdapter();
                        clientAdapter.DeleteQuery(clientId);  // Удаляем клиента по ID
                        MessageBox.Show("Клиент удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadClients(); // Перезагружаем список клиентов после удаления
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении клиента: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LicenseSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LicenseSearchBox.Text == "Введите ВУ")
            {
                LicenseSearchBox.Text = string.Empty;
                LicenseSearchBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void LicenseSearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LicenseSearchBox.Text))
            {
                LicenseSearchBox.Text = "Введите ВУ";
                LicenseSearchBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            LicenseSearchBox.Text = "Введите ВУ";
            LicenseSearchBox.Foreground = new SolidColorBrush(Colors.Gray);
            LoadClients(); // Заново загружаем всех клиентов
        }


        private void SearchByLicenseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string licenseNumber = LicenseSearchBox.Text.Trim(); // Получаем введенное значение

                if (!string.IsNullOrEmpty(licenseNumber) && licenseNumber != "Введите ВУ")
                {
                    ClientTableAdapter clientAdapter = new ClientTableAdapter();
                    var clients = clientAdapter.GetData(); // Получаем всех клиентов
                    var filteredClients = clients.AsEnumerable()
                        .Where(client => client.Field<string>("driver_license") == licenseNumber);

                    if (filteredClients.Any())
                    {
                        ClientDataGrid.ItemsSource = clients.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Клиенты с указанным ВУ не найдены.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Введите номер водительского удостоверения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении поиска: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
