using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;
using LiveCharts;
using LiveCharts.Wpf;

namespace Autobike
{
    public partial class BookingChartWindow : Window
    {
        public BookingChartWindow()
        {
            InitializeComponent();
            LoadBookingData();
        }

        private void LoadBookingData()
        {
            try
            {
                // Инициализация адаптера для работы с базой данных
                AccountTableAdapter accountAdapter = new AccountTableAdapter();
                var accounts = accountAdapter.GetData();

                if (accounts == null || accounts.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для отображения графика!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Создание словаря для бронирований по месяцам
                var bookingsByMonth = new Dictionary<string, int>();

                foreach (DataRow account in accounts.Rows)
                {
                    DateTime? rentalStartDate = account.IsNull("rental_start_date")
                        ? (DateTime?)null
                        : account.Field<DateTime>("rental_start_date");

                    if (rentalStartDate.HasValue)
                    {
                        string monthKey = rentalStartDate.Value.ToString("MMMM yyyy", CultureInfo.InvariantCulture);

                        if (bookingsByMonth.ContainsKey(monthKey))
                        {
                            bookingsByMonth[monthKey]++;
                        }
                        else
                        {
                            bookingsByMonth[monthKey] = 1;
                        }
                    }
                }

                if (bookingsByMonth.Count == 0)
                {
                    MessageBox.Show("Нет данных для отображения на графике.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Создание серии графика
                var series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Bookings",
                        Values = new ChartValues<int>(bookingsByMonth.Values)
                    }
                };

                // Проверяем, что BookingChart инициализирован
                if (BookingChart == null)
                {
                    MessageBox.Show("График не инициализирован!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                BookingChart.Series = series;

                // Настройка осей
                BookingChart.AxisX[0].Labels = bookingsByMonth.Keys.ToList();
                BookingChart.AxisY[0].LabelFormatter = value => value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных для графика: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
