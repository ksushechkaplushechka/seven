using System;
using System.Data;
using System.Linq;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditAccountWindow : Window
    {
        private int accountId;

        public EditAccountWindow(int accountId)
        {
            InitializeComponent();
            this.accountId = accountId;
            LoadComboBoxData(); // Загружаем данные для ComboBox
            Loaded += EditAccountWindow_Loaded; // Событие загрузки окна
        }

        private void EditAccountWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAccountData(); // После загрузки окна загружаем данные счета
        }

        private void LoadAccountData()
        {
            AccountTableAdapter accountAdapter = new AccountTableAdapter();
            var accountData = accountAdapter.GetData();
            var account = accountData.FirstOrDefault(a => a.id == accountId);

            if (account != null)
            {
                // Устанавливаем значения для ComboBox
                CarNumberComboBox.SelectedValue = account.car_number;
                ClientIdComboBox.SelectedValue = account.client_id;

                PriceTextBox.Text = account.price.ToString();
                RentalStartDatePicker.SelectedDate = account.rental_start_date;
                RentalEndDatePicker.SelectedDate = account.rental_end_date;
                ReturnDatePicker.SelectedDate = account.Isreturn_dateNull() ? (DateTime?)null : account.return_date;
            }
            else
            {
                MessageBox.Show("Счет не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Загрузка данных для ComboBox с клиентами
                ClientTableAdapter clientAdapter = new ClientTableAdapter();
                var clientData = clientAdapter.GetData();
                if (clientData.Rows.Count > 0)
                {
                    ClientIdComboBox.ItemsSource = clientData;
                    ClientIdComboBox.DisplayMemberPath = "last_name";
                    ClientIdComboBox.SelectedValuePath = "id";
                }
                else
                {
                    MessageBox.Show("Нет данных для клиентов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Загрузка данных для ComboBox с автомобилями
                CarTableAdapter carAdapter = new CarTableAdapter();
                var carData = carAdapter.GetData();
                if (carData.Rows.Count > 0)
                {
                    CarNumberComboBox.ItemsSource = carData;
                    CarNumberComboBox.DisplayMemberPath = "car_number";
                    CarNumberComboBox.SelectedValuePath = "car_number";
                }
                else
                {
                    MessageBox.Show("Нет данных для автомобилей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка выбора автомобиля
                if (CarNumberComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите автомобиль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка выбора клиента
                if (ClientIdComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка формата цены
                if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Введите корректную цену (положительное число).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка формата дат
                if (!RentalStartDatePicker.SelectedDate.HasValue || !RentalEndDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Выберите даты начала и окончания аренды.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (RentalStartDatePicker.SelectedDate.Value > RentalEndDatePicker.SelectedDate.Value)
                {
                    MessageBox.Show("Дата начала аренды не может быть позже даты окончания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка возвратной даты (опционально)
                DateTime? returnDate = ReturnDatePicker.SelectedDate;
                if (returnDate.HasValue && returnDate.Value < RentalStartDatePicker.SelectedDate.Value)
                {
                    MessageBox.Show("Дата возврата не может быть раньше даты начала аренды.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка формата автомобильного номера
                var selectedCar = CarNumberComboBox.SelectedItem as DataRowView;
                string carNumber = selectedCar?["car_number"].ToString();
                if (string.IsNullOrWhiteSpace(carNumber) || !System.Text.RegularExpressions.Regex.IsMatch(carNumber, @"^[А-Я]{1}[0-9]{3}[А-Я]{2}[0-9]{2,3}$"))
                {
                    MessageBox.Show("Номер автомобиля не соответствует формату: [А-Я]{1}[0-9]{3}[А-Я]{2}[0-9]{2,3}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка формата ID клиента
                var selectedClient = ClientIdComboBox.SelectedItem as DataRowView;
                if (selectedClient == null || !int.TryParse(selectedClient["id"].ToString(), out int clientId))
                {
                    MessageBox.Show("ID клиента не соответствует формату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime rentalStartDate = RentalStartDatePicker.SelectedDate.Value;
                DateTime rentalEndDate = RentalEndDatePicker.SelectedDate.Value;

                // Обновление данных в базе
                AccountTableAdapter accountAdapter = new AccountTableAdapter();
                accountAdapter.UpdateQuery(
                    carNumber,
                    clientId,
                    price,
                    rentalStartDate.ToString("yyyy-MM-dd"),
                    rentalEndDate.ToString("yyyy-MM-dd"),
                    returnDate?.ToString("yyyy-MM-dd"),
                    accountId
                );

                MessageBox.Show("Данные счета успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении счета: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
