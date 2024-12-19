using System;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using ClosedXML.Excel;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class AccountWindow : Window
    {
        public AccountWindow()
        {
            InitializeComponent();
            LoadAccounts();

            // Скрываем кнопки для обычных пользователей
            if (!AuthService.IsAdmin)
            {
                AddAccountButton.Visibility = Visibility.Collapsed;
                EditAccountButton.Visibility = Visibility.Collapsed;
                DeleteAccountButton.Visibility = Visibility.Collapsed;
                ExportButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadAccounts()
        {
            try
            {
                // Адаптеры для таблиц
                AccountTableAdapter accountAdapter = new AccountTableAdapter();
                CarTableAdapter carAdapter = new CarTableAdapter();
                TariffTableAdapter tariffAdapter = new TariffTableAdapter();
                ClientTableAdapter clientAdapter = new ClientTableAdapter(); // Адаптер для таблицы клиентов

                // Получаем данные из всех таблиц
                var accounts = accountAdapter.GetData();
                var cars = carAdapter.GetData();
                var tariffs = tariffAdapter.GetData();
                var clients = clientAdapter.GetData(); // Данные о клиентах

                // Проверяем наличие данных
                if (accounts.Rows.Count == 0)
                {
                    MessageBox.Show("Таблица аккаунтов пуста.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Соединяем таблицы для получения нужной информации
                var accountsWithPrice = from account in accounts.AsEnumerable()
                                        join car in cars.AsEnumerable() on account.Field<string>("car_number") equals car.Field<string>("car_number") into carJoin
                                        from car in carJoin.DefaultIfEmpty()
                                        join tariff in tariffs.AsEnumerable() on car?.Field<int?>("tariff_id") equals tariff.Field<int?>("id") into tariffJoin
                                        from tariff in tariffJoin.DefaultIfEmpty()
                                        join client in clients.AsEnumerable() on account.Field<int?>("client_id") equals client.Field<int?>("id") into clientJoin
                                        from client in clientJoin.DefaultIfEmpty()
                                        where AuthService.IsAdmin || account.Field<int?>("client_id") == AuthService.CurrentClientId
                                        select new
                                        {
                                            AccountId = account.Field<int>("id"),
                                            CarNumber = account.Field<string>("car_number"),
                                            ClientMiddleName = client == null ? "Не найдено" : client.Field<string>("middle_name"),
                                            TariffPrice = tariff == null ? 0m : tariff.Field<decimal?>("price") ?? 0m,
                                            RentalStartDate = account.IsNull("rental_start_date") ? (DateTime?)null : account.Field<DateTime?>("rental_start_date"),
                                            RentalEndDate = account.IsNull("rental_end_date") ? (DateTime?)null : account.Field<DateTime?>("rental_end_date"),
                                            ReturnDate = account.IsNull("return_date") ? (DateTime?)null : account.Field<DateTime?>("return_date")
                                        };

                // Добавляем расчет стоимости аренды
                var accountsWithCalculatedPrice = accountsWithPrice.Select(account =>
                {
                    decimal totalPrice = 0;

                    if (account.RentalStartDate.HasValue && account.RentalEndDate.HasValue)
                    {
                        // Рассчитываем количество дней аренды
                        int rentalDays = (account.RentalEndDate.Value - account.RentalStartDate.Value).Days + 1;
                        totalPrice = rentalDays * account.TariffPrice;
                    }

                    return new
                    {
                        AccountId = account.AccountId,
                        CarNumber = account.CarNumber,
                        ClientMiddleName = account.ClientMiddleName,  // Выводим middle_name
                        Price = totalPrice.ToString("C"),  // Форматируем цену как денежную сумму
                        RentalStartDate = account.RentalStartDate,
                        RentalEndDate = account.RentalEndDate,
                        ReturnDate = account.ReturnDate
                    };
                }).ToList();

                // Проверяем, есть ли данные для отображения
                if (accountsWithCalculatedPrice.Count == 0)
                {
                    MessageBox.Show("Нет данных для отображения.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Привязываем данные к DataGrid
                AccountDataGrid.ItemsSource = accountsWithCalculatedPrice;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных о счетах: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для кнопки "Добавить"
        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AddAccountWindow addAccountWindow = new AddAccountWindow();
            addAccountWindow.ShowDialog();
            LoadAccounts();
        }

        // Обработчик для кнопки "Редактировать"
        private void EditAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountDataGrid.SelectedItem != null)
            {
                var selectedAccount = AccountDataGrid.SelectedItem as dynamic;
                int accountId = selectedAccount.AccountId;

                EditAccountWindow editAccountWindow = new EditAccountWindow(accountId);
                editAccountWindow.ShowDialog();
                LoadAccounts();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите счет для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountDataGrid.SelectedItem != null)
            {
                // Попробуйте извлечь выбранную строку из DataGrid
                var selectedAccount = AccountDataGrid.SelectedItem as dynamic;

                if (selectedAccount != null)
                {
                    try
                    {
                        int accountId = selectedAccount.AccountId;

                        // Создаем адаптер для работы с таблицей аккаунтов
                        AccountTableAdapter accountAdapter = new AccountTableAdapter();

                        // Выполняем удаление с использованием AccountId
                        accountAdapter.DeleteQuery(accountId);

                        MessageBox.Show("Счет удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadAccounts(); // Перезагружаем данные в DataGrid
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении счета: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите правильный элемент для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите счет для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для кнопки "Экспорт в Excel"
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем новый Excel файл
                using (var workbook = new XLWorkbook())
                {
                    // Добавляем рабочий лист
                    var worksheet = workbook.AddWorksheet("Accounts");

                    // Заголовки столбцов
                    worksheet.Cell(1, 1).Value = "Account ID";
                    worksheet.Cell(1, 2).Value = "Car Number";
                    worksheet.Cell(1, 3).Value = "Client Middle Name";
                    worksheet.Cell(1, 4).Value = "Price";
                    worksheet.Cell(1, 5).Value = "Rental Start Date";
                    worksheet.Cell(1, 6).Value = "Rental End Date";
                    worksheet.Cell(1, 7).Value = "Return Date";

                    // Заполняем данными из DataGrid
                    int row = 2;
                    foreach (var account in AccountDataGrid.ItemsSource)
                    {
                        var accountData = account as dynamic;

                        worksheet.Cell(row, 1).Value = accountData.AccountId;
                        worksheet.Cell(row, 2).Value = accountData.CarNumber;
                        worksheet.Cell(row, 3).Value = accountData.ClientMiddleName;
                        worksheet.Cell(row, 4).Value = accountData.Price;
                        worksheet.Cell(row, 5).Value = accountData.RentalStartDate?.ToString("yyyy-MM-dd") ?? string.Empty;
                        worksheet.Cell(row, 6).Value = accountData.RentalEndDate?.ToString("yyyy-MM-dd") ?? string.Empty;
                        worksheet.Cell(row, 7).Value = accountData.ReturnDate?.ToString("yyyy-MM-dd") ?? string.Empty;

                        row++;
                    }

                    // Сохраняем файл Excel
                    var saveDialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog
                    {
                        IsFolderPicker = false,
                        DefaultFileName = "Accounts.xlsx",
                        DefaultExtension = ".xlsx",
                    };

                    if (saveDialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
                    {
                        workbook.SaveAs(saveDialog.FileName);
                        MessageBox.Show("Экспорт выполнен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при экспорте в Excel: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для кнопки "Закрыть"
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewFinesButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountDataGrid.SelectedItem != null)
            {
                var selectedAccount = AccountDataGrid.SelectedItem as dynamic; // Получаем выбранный элемент

                if (selectedAccount != null && selectedAccount.AccountId != null)
                {
                    int accountId = selectedAccount.AccountId; // Получаем AccountId
                    UserFinesWindow editAccountWindow = new UserFinesWindow(accountId);
                    editAccountWindow.ShowDialog();
                    LoadAccounts();
                }
                else
                {
                    MessageBox.Show("Выберите правильный элемент для просмотра штрафов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите счет.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
