using System;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class FineWindow : Window
    {
        FineTableAdapter fineAdapter = new FineTableAdapter();
        AutocarsDataSet autocarsDataSet = new AutocarsDataSet();

        public FineWindow()
        {
            InitializeComponent();
            LoadFineData(); // Загружаем данные при открытии окна
        }

        // Метод для загрузки данных о штрафах из базы данных
        private void LoadFineData()
        {
            fineAdapter.Fill(autocarsDataSet.Fine); // Заполняем набор данных
            FineDataGrid.ItemsSource = autocarsDataSet.Fine.DefaultView; // Привязываем данные к DataGrid
        }

        // Обработчик для обновления данных
        private void RefreshFineButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFineData(); // Обновляем данные из базы
        }

        // Обработчик для добавления нового штрафа
        private void AddFineButton_Click(object sender, RoutedEventArgs e)
        {
            int? selectedAccountId = GetSelectedAccountId(); // Получаем ID аккаунта
            AddFineWindow addFineWindow = new AddFineWindow(selectedAccountId);
            addFineWindow.ShowDialog();

            // После закрытия окна добавления, обновляем таблицу
            LoadFineData();
        }

        // Обработчик для редактирования выбранного штрафа
        private void EditFineButton_Click(object sender, RoutedEventArgs e)
        {
            if (FineDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)FineDataGrid.SelectedItem;
                EditFineWindow editFineWindow = new EditFineWindow(selectedRow);
                editFineWindow.ShowDialog();

                // После закрытия окна редактирования, обновляем таблицу
                LoadFineData();
            }
            else
            {
                MessageBox.Show("Выберите штраф для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработчик для удаления выбранного штрафа
        // Обработчик для удаления выбранного штрафа
        private void DeleteFineButton_Click(object sender, RoutedEventArgs e)
        {
            if (FineDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)FineDataGrid.SelectedItem;
                string fineNumber = selectedRow["fine_number"].ToString(); // Предполагается, что это строка
                decimal? amount = selectedRow["amount"] as decimal?;
                int? accountId = selectedRow["account_id"] as int?;

                // Определение, являются ли amount и account_id null
                int isNullAmount = amount == null ? 1 : 0;
                int isNullAccountId = accountId == null ? 1 : 0;

                // Подтверждение удаления
                if (MessageBox.Show($"Вы уверены, что хотите оплатить штраф с номером {fineNumber}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    fineAdapter.DeleteQuery(
                        fineNumber, // Передаем номер штрафа
                        isNullAmount, // Передаем значение для IsNull_amount
                        amount, // Передаем amount
                        isNullAccountId, // Передаем значение для IsNull_account_id
                        accountId // Передаем account_id
                    );

                    LoadFineData(); // Обновляем таблицу после удаления
                }
            }
            else
            {
                MessageBox.Show("Выберите штраф для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        // Выбор строки в DataGrid
        private void FineDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Логика для выбора, если необходимо
        }

        private int? GetSelectedAccountId()
        {
            if (FineDataGrid.SelectedItem is DataRowView selectedRow)
            {
                return (int?)selectedRow["account_id"]; // Замените на имя вашего поля, если нужно
            }
            return null; // Если ничего не выбрано
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
