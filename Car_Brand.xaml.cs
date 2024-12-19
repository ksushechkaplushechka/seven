using System;
using System.Data;
using System.Linq;
using System.Windows;
using ClosedXML.Excel;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class CarBrandWindow : Window
    {
        Car_BrandTableAdapter carBrandAdapter = new Car_BrandTableAdapter();
        AutocarsDataSet autocarsDataSet = new AutocarsDataSet();

        public CarBrandWindow()
        {
            InitializeComponent();
            LoadCarBrandData(); // Загружаем данные при открытии окна
        }

        // Метод для загрузки данных о марках автомобилей из базы данных
        private void LoadCarBrandData()
        {
            carBrandAdapter.Fill(autocarsDataSet.Car_Brand); // Заполняем набор данных
            CarBrandDataGrid.ItemsSource = autocarsDataSet.Car_Brand.DefaultView; // Привязываем данные к DataGrid
        }

        // Обработчик для импорта данных из Excel
        private void ImportFromExcelButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем диалог выбора файла
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Открываем выбранный Excel файл
                    using (var workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        // Выбираем первый рабочий лист
                        var worksheet = workbook.Worksheet(1);

                        // Перебираем строки начиная с 2-й (чтобы пропустить заголовки)
                        foreach (var row in worksheet.RowsUsed().Skip(1))
                        {
                            string name = row.Cell(1).GetValue<string>(); // Имя марки автомобиля
                            string carClass = row.Cell(2).GetValue<string>(); // Класс автомобиля
                            int seats = row.Cell(3).GetValue<int>(); // Количество мест
                            bool airConditioning = row.Cell(4).GetValue<bool>(); // Наличие кондиционера
                            double engineCapacity;

                            // Преобразуем значение в engineCapacity только если это корректное число
                            if (double.TryParse(row.Cell(5).GetValue<string>(), out engineCapacity))
                            {
                                // Добавляем запись в таблицу Car_Brand с преобразованием в decimal?
                                carBrandAdapter.Insert(name, carClass, seats, airConditioning, (decimal?)engineCapacity);
                            }
                            else
                            {
                                // Если значение не является числом, можно обработать это как ошибку или задать значение по умолчанию
                                MessageBox.Show($"Неверное значение объема двигателя в строке {row.RowNumber()}.", "Ошибка импорта", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                        MessageBox.Show("Данные успешно импортированы!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    // После импорта данных, обновляем таблицу
                    LoadCarBrandData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при импорте данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Обработчик для обновления данных
        private void RefreshCarBrandButton_Click(object sender, RoutedEventArgs e)
        {
            LoadCarBrandData(); // Обновляем данные из базы
        }

        // Обработчик для добавления новой марки автомобиля
        private void AddCarBrandButton_Click(object sender, RoutedEventArgs e)
        {
            AddCarBrandWindow addCarBrandWindow = new AddCarBrandWindow();
            addCarBrandWindow.ShowDialog();

            // После закрытия окна добавления, обновляем таблицу
            LoadCarBrandData();
        }

        // Обработчик для редактирования выбранной марки автомобиля
        private void EditCarBrandButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarBrandDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)CarBrandDataGrid.SelectedItem;
                EditCarBrandWindow editCarBrandWindow = new EditCarBrandWindow(selectedRow);
                editCarBrandWindow.ShowDialog();

                // После закрытия окна редактирования, обновляем таблицу
                LoadCarBrandData();
            }
            else
            {
                MessageBox.Show("Выберите марку автомобиля для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработчик для удаления выбранной марки автомобиля
        private void DeleteCarBrandButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarBrandDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)CarBrandDataGrid.SelectedItem;
                int carBrandId = (int)selectedRow["id"];

                // Подтверждение удаления
                if (MessageBox.Show($"Вы уверены, что хотите удалить марку с ID {carBrandId}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    carBrandAdapter.DeleteQuery(carBrandId);
                    LoadCarBrandData(); // Обновляем таблицу после удаления
                }
            }
            else
            {
                MessageBox.Show("Выберите марку для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
