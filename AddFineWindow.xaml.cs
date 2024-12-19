using System;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;
using System.Data;

namespace Autobike
{
    public partial class AddFineWindow : Window
    {
        private int? accountId;
        FineTableAdapter fineAdapter = new FineTableAdapter();
        AccountTableAdapter accountAdapter = new AccountTableAdapter();

        public AddFineWindow(int? accountId = null)
        {
            InitializeComponent();
            this.accountId = accountId;

            LoadAccountComboBox(); // Загрузим данные в ComboBox

            // Если передан accountId, установим ComboBox на этот счет
            if (accountId.HasValue)
            {
                AccountComboBox.SelectedValue = accountId.Value;
                AccountComboBox.IsEnabled = false; // Заблокируем изменение, если счет уже выбран
            }
        }

        // Метод для загрузки данных в ComboBox
        private void LoadAccountComboBox()
        {
            var accountData = accountAdapter.GetData();
            AccountComboBox.ItemsSource = accountData;
            AccountComboBox.DisplayMemberPath = "id"; // Укажите поле для отображения номера счета
            AccountComboBox.SelectedValuePath = "id"; // Поле для значения — id счета
        }

        // Обработчик для изменения выбора счета в ComboBox
        private void AccountComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (AccountComboBox.SelectedValue != null)
            {
                accountId = (int)AccountComboBox.SelectedValue; // Устанавливаем выбранный accountId
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, что счет выбран
                if (accountId == null)
                {
                    MessageBox.Show("Выберите номер счета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка ввода суммы штрафа с учетом только целых чисел
                if (!int.TryParse(FineAmountTextBox.Text, out int fineAmount) || fineAmount <= 0)
                {
                    MessageBox.Show("Введите корректное целое положительное число для суммы штрафа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка причины штрафа
                string reason = ReasonTextBox.Text.Trim();
                if (string.IsNullOrEmpty(reason))
                {
                    MessageBox.Show("Введите причину штрафа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Добавляем новый штраф в базу данных
                fineAdapter.Insert(reason, fineAmount, accountId);

                MessageBox.Show("Штраф успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении штрафа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
