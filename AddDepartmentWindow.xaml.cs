using System;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class AddDepartmentWindow : Window
    {
        DepartmentTableAdapter departmentAdapter = new DepartmentTableAdapter();

        public AddDepartmentWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем введенное название отдела
                string name = DepartmentNameTextBox.Text;

                // Проверка на соответствие ограничениям
                if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[А-Яа-я0-9\s\-]{2,50}$"))
                {
                    MessageBox.Show("Введите корректное название отдела (2-50 символов, кириллица, цифры, пробелы или дефисы).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Добавляем новый отдел в базу данных (id генерируется автоматически)
                departmentAdapter.Insert(name);

                MessageBox.Show("Отдел успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении отдела: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
