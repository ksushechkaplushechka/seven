using System;
using System.Text.RegularExpressions;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditTariffWindow : Window
    {
        private DataRowView selectedRow; // Выбранная строка для редактирования
        TariffTableAdapter tariffAdapter = new TariffTableAdapter();

        public EditTariffWindow(DataRowView row)
        {
            InitializeComponent();
            selectedRow = row;

            // Загружаем данные из выбранной строки в поля
            TariffNameTextBox.Text = selectedRow["name"].ToString();
            TariffPriceTextBox.Text = selectedRow["price"].ToString();
        }

        // Проверка на правильность ввода имени тарифа
        private bool IsValidName(string name)
        {
            string namePattern = @"^[А-Яа-я0-9\s\-]{2,50}$"; // Регулярное выражение для имени тарифа
            return Regex.IsMatch(name, namePattern);
        }

        // Проверка на правильность ввода цены тарифа
        private bool IsValidPrice(string price)
        {
            string pricePattern = @"^[0-9]+$"; // Регулярное выражение для цены (целое число)
            return Regex.IsMatch(price, pricePattern);
        }

        // Метод для сохранения изменений
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tariffName = TariffNameTextBox.Text; // Новое название тарифа
                string tariffPriceText = TariffPriceTextBox.Text; // Новая цена тарифа

                // Проверка имени тарифа
                if (!IsValidName(tariffName))
                {
                    MessageBox.Show("Название тарифа должно содержать от 2 до 50 символов, включая буквы, цифры, пробелы и дефисы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка цены тарифа
                if (!IsValidPrice(tariffPriceText))
                {
                    MessageBox.Show("Цена тарифа должна быть целым числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                decimal tariffPrice = decimal.Parse(tariffPriceText); // Преобразуем строку в decimal

                // Получаем ID тарифа для обновления
                int tariffId = (int)selectedRow["id"];

                // Обновляем тариф в базе данных
                tariffAdapter.UpdateQuery(tariffName, tariffPrice, tariffId);

                MessageBox.Show("Тариф успешно обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Закрываем окно после успешного обновления
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные данные в поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении тарифа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
