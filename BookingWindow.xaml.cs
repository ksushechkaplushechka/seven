using System;
using System.Data;
using System.Windows;
using System.Linq;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class BookingWindow : Window
    {
        private CarTableAdapter carAdapter = new CarTableAdapter();
        private Car_BrandTableAdapter brandAdapter = new Car_BrandTableAdapter();
        private TariffTableAdapter tariffAdapter = new TariffTableAdapter();
        private AccountTableAdapter accountAdapter = new AccountTableAdapter();
        private AutocarsDataSet autocarsDataSet = new AutocarsDataSet();

        public BookingWindow()
        {
            InitializeComponent();
            LoadAvailableCars();
        }

        private void LoadAvailableCars()
        {
            carAdapter.Fill(autocarsDataSet.Car);
            brandAdapter.Fill(autocarsDataSet.Car_Brand);
            tariffAdapter.Fill(autocarsDataSet.Tariff);

            // Создаем DataView с фильтром для доступных машин
            DataView availableCars = new DataView(autocarsDataSet.Car)
            {
                RowFilter = "is_booked = false AND is_in_worked = true"
            };

            // Преобразуем строки в список объектов с нужными полями
            var availableCarsList = availableCars.Cast<DataRowView>().Select(row =>
            {
                string carNumber = row["car_number"].ToString();
                int brandId = (int)row["brand_id"];
                int tariffId = (int)row["tariff_id"];
                int yearOfManufacture = (int)row["year_of_manufacture"];

                // Находим соответствующие бренд и тариф
                var brandRow = autocarsDataSet.Car_Brand.FirstOrDefault(b => b.id == brandId);
                var tariffRow = autocarsDataSet.Tariff.FirstOrDefault(t => t.id == tariffId);

                return new
                {
                    CarNumber = carNumber,
                    BrandName = brandRow?.Field<string>("name") ?? "Неизвестен",  // Название марки
                    Class = brandRow?.Field<string>("class") ?? "Не указан",      // Класс автомобиля
                    Seats = brandRow?.Field<int?>("seats") ?? 0,                  // Количество мест
                    AirConditioning = brandRow?.Field<bool?>("air_conditioning") == true ? "Да" : "Нет", // Кондиционер
                    EngineCapacity = brandRow?.Field<decimal?>("engine_capacity") ?? 0m, // Объем двигателя
                    YearOfManufacture = yearOfManufacture,
                    TariffPrice = tariffRow?.Field<decimal?>("price") ?? 0m       // Цена тарифа
                };

            }).ToList();

            // Устанавливаем источник данных для DataGrid
            AvailableCarsDataGrid.ItemsSource = availableCarsList;
        }

        private void BookCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableCarsDataGrid.SelectedItem != null && StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue)
            {
                var selectedCar = (dynamic)AvailableCarsDataGrid.SelectedItem; // Получаем выбранный автомобиль как динамический объект
                string carNumber = selectedCar.CarNumber;  // Номер машины
                DateTime rentalDate = StartDatePicker.SelectedDate.Value;  // Дата начала аренды
                DateTime endDate = EndDatePicker.SelectedDate.Value;  // Дата окончания аренды

                // Проверяем, что дата окончания аренды позже даты начала
                if (endDate < rentalDate)
                {
                    MessageBox.Show("Дата окончания должна быть позже даты начала!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Находим тариф для выбранного автомобиля
                var carRow = autocarsDataSet.Car.FirstOrDefault(c => c.car_number == carNumber);
                if (carRow == null || carRow.IsNull("tariff_id"))
                {
                    MessageBox.Show("Тариф для выбранного автомобиля не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int tariffId = carRow.tariff_id;
                var tariffRow = autocarsDataSet.Tariff.FirstOrDefault(t => t.id == tariffId);
                if (tariffRow == null || tariffRow.IsNull("price"))
                {
                    MessageBox.Show("Цена для тарифа не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                decimal dailyPrice = tariffRow.price;  // Цена за день аренды

                // Вычисляем количество дней аренды
                int rentalDays = (endDate - rentalDate).Days + 1;

                // Итоговая стоимость за весь период аренды
                decimal totalPrice = dailyPrice * rentalDays;

                // Получаем ID текущего клиента (предположим, что оно доступно через AuthService)
                int? clientId = AuthService.CurrentClientId;
                DateTime? returnDate = null;  // Если дата возврата неизвестна на момент бронирования

                // Добавляем запись о бронировании в таблицу Account
                accountAdapter.Insert(carNumber, clientId, totalPrice, rentalDate, endDate, returnDate);

                // Обновляем статус is_booked на true для забронированной машины
                carAdapter.UpdateIsBooked(true, carNumber);

                MessageBox.Show($"Машина успешно забронирована на {rentalDays} дней. Итоговая стоимость: {totalPrice:C}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Открываем окно для оплаты
                PaymentWindow paymentWindow = new PaymentWindow();
                paymentWindow.ShowDialog();

                // Закрываем текущее окно после бронирования
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите машину и укажите даты аренды.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
