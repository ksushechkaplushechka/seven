using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class EmployeeWindow : Window
    {
        private EmployeeTableAdapter employeeAdapter = new EmployeeTableAdapter();
        private DepartmentTableAdapter departmentAdapter = new DepartmentTableAdapter();

        public EmployeeWindow()
        {
            InitializeComponent();
            LoadDepartments();
            LoadEmployeeData();
        }

        private void LoadDepartments()
        {
            var departments = departmentAdapter.GetData(); // Получаем список отделов
            DepartmentFilter.ItemsSource = departments.DefaultView;
            DepartmentFilter.DisplayMemberPath = "name";
            DepartmentFilter.SelectedValuePath = "id";

            // Добавляем "Все отделы"
            var allDepartments = departments.NewRow();
            allDepartments["id"] = -1;
            allDepartments["name"] = "Все отделы";
            departments.Rows.InsertAt(allDepartments, 0);

            DepartmentFilter.SelectedIndex = 0; // Выбираем "Все отделы" по умолчанию
        }


        // Загружаем данные сотрудников в DataGrid
        private void LoadEmployeeData()
        {
            // Получаем все данные сотрудников
            var employees = employeeAdapter.GetData();

            // Получаем выбранный ID отдела
            var selectedDepartmentId = (int)DepartmentFilter.SelectedValue;

            // Фильтруем сотрудников в зависимости от выбранного отдела
            if (selectedDepartmentId != -1)  // Если выбран конкретный отдел
            {
                var filteredEmployees = employees.Select($"department_id = {selectedDepartmentId}");
                EmployeeDataGrid.ItemsSource = filteredEmployees.CopyToDataTable().DefaultView;  // Преобразуем в DataView
            }
            else  // Если выбран "Все отделы"
            {
                EmployeeDataGrid.ItemsSource = employees.DefaultView;
            }
        }

        // Обработчик события изменения выбора в ComboBox
        private void DepartmentFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadEmployeeData(); // Обновляем данные сотрудников при изменении выбранного отдела
        }


        // Открытие окна для добавления нового сотрудника
        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEmployeeWindow();
            addWindow.ShowDialog();
            LoadEmployeeData();  // Обновляем список после добавления
        }

        // Открытие окна для редактирования выбранного сотрудника
        private void EditEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is DataRowView selectedRow)
            {
                var editWindow = new EditEmployeeWindow(selectedRow);
                editWindow.ShowDialog();
                LoadEmployeeData();  // Обновляем список после редактирования
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Удаление выбранного сотрудника
        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int id = (int)selectedRow["id"];
                var result = MessageBox.Show("Вы уверены, что хотите удалить выбранного сотрудника?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        employeeAdapter.DeleteQuery(id);
                        LoadEmployeeData();  // Обновляем список после удаления
                        MessageBox.Show("Сотрудник успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
