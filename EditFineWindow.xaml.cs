using System;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditFineWindow : Window
    {
        private DataRowView fineRow;
        FineTableAdapter fineAdapter = new FineTableAdapter();
        AccountTableAdapter accountAdapter = new AccountTableAdapter();

        public EditFineWindow(DataRowView selectedRow)
        {
            InitializeComponent();
            fineRow = selectedRow;
            LoadAccountData();

            // Заполняем поля данными из выбранного штрафа
            FineNumberTextBox.Text = fineRow["fine_number"]?.ToString();
            FineAmountTextBox.Text = fineRow["amount"]?.ToString();
            AccountIdComboBox.SelectedValue = fineRow["account_id"]; // Устанавливаем account_id
        }

        // Метод для загрузки данных счетов в ComboBox
        private void LoadAccountData()
        {
            try
            {
                var accountData = accountAdapter.GetData();
                AccountIdComboBox.ItemsSource = accountData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных счетов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для сохранения изменений
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка ввода суммы штрафа на целое положительное число
                if (!int.TryParse(FineAmountTextBox.Text, out int fineAmount) || fineAmount <= 0)
                {
                    MessageBox.Show("Введите корректное целое положительное число для суммы штрафа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Получаем новое значение штрафа и проверяем наличие выбранного счета
                string newFineNumber = FineNumberTextBox.Text.Trim();
                if (string.IsNullOrEmpty(newFineNumber))
                {
                    MessageBox.Show("Введите номер штрафа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int? accountId = AccountIdComboBox.SelectedValue as int?;
                if (accountId == null)
                {
                    MessageBox.Show("Выберите номер счета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Получаем оригинальный номер штрафа из текущей строки
                string originalFineNumber = fineRow["fine_number"].ToString();

                // Обновляем штраф в базе данных
                fineAdapter.UpdateQuery(newFineNumber, fineAmount, accountId, originalFineNumber);

                MessageBox.Show("Штраф успешно обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении штрафа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
