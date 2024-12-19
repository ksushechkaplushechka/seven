using System;
using System.Text.RegularExpressions;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class AddTariffWindow : Window
    {
        TariffTableAdapter tariffAdapter = new TariffTableAdapter();

        public AddTariffWindow()
        {
            InitializeComponent();
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

        // Метод для сохранения нового тарифа
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tariffName = TariffNameTextBox.Text; // Название тарифа
                string tariffPriceText = TariffPriceTextBox.Text; // Цена тарифа

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

                // Добавляем новый тариф в базу данных
                tariffAdapter.Insert(tariffName, tariffPrice);

                MessageBox.Show("Тариф успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Закрываем окно после успешного добавления
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные данные в поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении тарифа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
