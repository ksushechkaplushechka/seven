using System;
using System.Data;
using System.Linq;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class UserFinesWindow : Window
    {
        private int clientId;

        public UserFinesWindow(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            LoadFines();
        }

        private void LoadFines()
        {
            try
            {
                // Создаем адаптеры
                FineTableAdapter fineAdapter = new FineTableAdapter();
                AccountTableAdapter accountAdapter = new AccountTableAdapter();

                // Получаем список счетов текущего клиента
                var accounts = accountAdapter.GetData()
                                             .Where(account => account.Field<int>("client_id") == clientId)
                                             .Select(account => account.Field<int>("id"))
                                             .ToList();

                if (accounts.Count == 0)
                {
                    MessageBox.Show("У вас нет связанных штрафов.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Загружаем штрафы, связанные с этими счетами
                var fines = fineAdapter.GetData()
                                       .Where(fine => accounts.Contains(fine.Field<int>("account_id")))
                                       .Select(fine => new FineViewModel
                                       {
                                           FineNumber = fine.Field<string>("fine_number"),
                                           Amount = fine.Field<decimal>("amount"),
                                           AccountId = fine.Field<int>("account_id")
                                       })
                                       .ToList();

                if (fines.Count == 0)
                {
                    MessageBox.Show("Нет данных о штрафах для отображения.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Привязываем данные к DataGrid
                FinesDataGrid.ItemsSource = fines;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void PayFineButton_Click(object sender, RoutedEventArgs e)
        {
            if (FinesDataGrid.SelectedItem is FineViewModel selectedFine)
            {
                // Получаем данные о выбранном штрафе
                string fineNumber = selectedFine.FineNumber;
                decimal amount = selectedFine.Amount;
                int accountId = selectedFine.AccountId;

                try
                {
                    // Логика оплаты штрафа
                    MessageBox.Show($"Штраф {fineNumber} на сумму {amount:C} оплачен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Логика удаления штрафа после оплаты
                    FineTableAdapter fineAdapter = new FineTableAdapter();

                    // Передаем все параметры в DeleteQuery
                    fineAdapter.DeleteQuery(
                        fineNumber,   // FineNumber
                        null,         // Передайте `null`, если параметр IsNull_amount отсутствует или может быть NULL
                        amount,       // Amount
                        null,         // Параметры, которые не нужны, передаем как null
                        accountId     // AccountId
                    );

                    // Обновляем список штрафов
                    LoadFines();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при оплате штрафа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите штраф для оплаты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
