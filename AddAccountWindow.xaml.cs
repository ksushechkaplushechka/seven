using System;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class AddAccountWindow : Window
    {
        public AddAccountWindow()
        {
            InitializeComponent();
            LoadClientData();  // Загружаем данные для ComboBox с клиентами
            LoadCarData();     // Загружаем данные для ComboBox с автомобилями
        }

        // Загрузка данных для ComboBox с клиентами
        private void LoadClientData()
        {
            try
            {
                ClientTableAdapter clientAdapter = new ClientTableAdapter();
                var clientData = clientAdapter.GetData();

                if (clientData != null && clientData.Rows.Count > 0)
                {
                    ClientIdComboBox.ItemsSource = clientData;  // Загрузка данных в ComboBox
                    ClientIdComboBox.DisplayMemberPath = "last_name";  // Указываем поле для отображения
                    ClientIdComboBox.SelectedValuePath = "client_id";  // Указываем поле для значения
                }
                else
                {
                    MessageBox.Show("Нет данных для клиентов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных клиентов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Загрузка данных для ComboBox с автомобилями
        private void LoadCarData()
        {
            try
            {
                CarTableAdapter carAdapter = new CarTableAdapter();
                var carData = carAdapter.GetData();

                if (carData != null && carData.Rows.Count > 0)
                {
                    CarNumberComboBox.ItemsSource = carData;  // Загрузка данных в ComboBox
                    CarNumberComboBox.DisplayMemberPath = "car_number";  // Указываем поле для отображения
                    CarNumberComboBox.SelectedValuePath = "car_id";  // Указываем поле для значения
                }
                else
                {
                    MessageBox.Show("Нет данных для автомобилей", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных автомобилей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка выбора автомобиля
                if (CarNumberComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите автомобиль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка выбора клиента
                if (ClientIdComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Получение и валидация номера автомобиля
                var selectedCar = (DataRowView)CarNumberComboBox.SelectedItem;
                string carNumber = selectedCar["car_number"].ToString();
                if (!System.Text.RegularExpressions.Regex.IsMatch(carNumber, @"^[А-Я]{1}[0-9]{3}[А-Я]{2}[0-9]{2,3}$"))
                {
                    MessageBox.Show("Неверный формат номера автомобиля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Получение и валидация клиента
                var selectedClient = (DataRowView)ClientIdComboBox.SelectedItem;
                if (!int.TryParse(selectedClient["id"].ToString(), out int clientId) || clientId <= 0)
                {
                    MessageBox.Show("Неверный идентификатор клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Валидация цены
                if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Цена должна быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка выбора дат
                if (!RentalStartDatePicker.SelectedDate.HasValue || !RentalEndDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Пожалуйста, выберите даты начала и окончания аренды.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DateTime rentalStartDate = RentalStartDatePicker.SelectedDate.Value;
                DateTime rentalEndDate = RentalEndDatePicker.SelectedDate.Value;

                if (rentalStartDate > rentalEndDate)
                {
                    MessageBox.Show("Дата начала аренды не может быть позже даты окончания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Форматирование дат
                string rentalStartDateStr = rentalStartDate.ToString("yyyy-MM-dd");
                string rentalEndDateStr = rentalEndDate.ToString("yyyy-MM-dd");

                // Добавление записи в базу
                AccountTableAdapter accountAdapter = new AccountTableAdapter();
                accountAdapter.InsertQuery(carNumber, clientId, price, rentalStartDateStr, rentalEndDateStr, null);

                MessageBox.Show("Счет добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении счета: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
