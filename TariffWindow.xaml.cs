using System;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class TariffWindow : Window
    {
        TariffTableAdapter tariffAdapter = new TariffTableAdapter();
        AutocarsDataSet autocarsDataSet = new AutocarsDataSet();

        public TariffWindow()
        {
            InitializeComponent();
            LoadTariffData(); // Загружаем данные при открытии окна
        }

        // Метод для загрузки данных о тарифах из базы данных
        private void LoadTariffData()
        {
            tariffAdapter.Fill(autocarsDataSet.Tariff); // Заполняем набор данных
            TariffDataGrid.ItemsSource = autocarsDataSet.Tariff.DefaultView; // Привязываем данные к DataGrid
        }

        // Обработчик для обновления данных
        private void RefreshTariffButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTariffData(); // Обновляем данные из базы
        }

        // Обработчик для добавления нового тарифа
        private void AddTariffButton_Click(object sender, RoutedEventArgs e)
        {
            AddTariffWindow addTariffWindow = new AddTariffWindow();
            addTariffWindow.ShowDialog();

            // После закрытия окна добавления, обновляем таблицу
            LoadTariffData();
        }

        // Обработчик для редактирования выбранного тарифа
        private void EditTariffButton_Click(object sender, RoutedEventArgs e)
        {
            if (TariffDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)TariffDataGrid.SelectedItem;
                EditTariffWindow editTariffWindow = new EditTariffWindow(selectedRow);
                editTariffWindow.ShowDialog();

                // После закрытия окна редактирования, обновляем таблицу
                LoadTariffData();
            }
            else
            {
                MessageBox.Show("Выберите тариф для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработчик для удаления выбранного тарифа
        private void DeleteTariffButton_Click(object sender, RoutedEventArgs e)
        {
            if (TariffDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)TariffDataGrid.SelectedItem;
                int tariffId = (int)selectedRow["id"]; // Получаем ID выбранного тарифа

                // Подтверждение удаления
                if (MessageBox.Show($"Вы уверены, что хотите удалить тариф с ID {tariffId}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    tariffAdapter.DeleteQuery(tariffId); // Удаляем тариф из базы данных
                    LoadTariffData(); // Обновляем таблицу после удаления
                }
            }
            else
            {
                MessageBox.Show("Выберите тариф для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
