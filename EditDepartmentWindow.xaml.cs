using System;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EditDepartmentWindow : Window
    {
        private DataRowView departmentRow;
        DepartmentTableAdapter departmentAdapter = new DepartmentTableAdapter();

        public EditDepartmentWindow(DataRowView selectedRow)
        {
            InitializeComponent();
            departmentRow = selectedRow;

            // Заполняем поля данными выбранного отдела
            DepartmentIdTextBlock.Text = departmentRow["id"].ToString(); // Отображаем ID отдела (только для чтения)
            DepartmentNameTextBox.Text = departmentRow["name"].ToString(); // Делаем имя отдела редактируемым
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем новый ввод для названия отдела
                string newName = DepartmentNameTextBox.Text;
                int departmentId = (int)departmentRow["id"]; // ID остается неизменным

                // Проверка на соответствие ограничениям
                if (!System.Text.RegularExpressions.Regex.IsMatch(newName, @"^[А-Яа-я0-9\s\-]{2,50}$"))
                {
                    MessageBox.Show("Введите корректное название отдела (2-50 символов, кириллица, цифры, пробелы или дефисы).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Обновляем данные отдела в базе данных
                departmentAdapter.UpdateQuery(newName, departmentId);

                MessageBox.Show("Отдел успешно обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении отдела: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
