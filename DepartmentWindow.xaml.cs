using System;
using System.Data;
using System.Windows;
using Autobike.AutocarsDataSetTableAdapters;

namespace Autobike
{
    public partial class DepartmentWindow : Window
    {
        DepartmentTableAdapter departmentAdapter = new DepartmentTableAdapter();
        AutocarsDataSet autocarsDataSet = new AutocarsDataSet();

        public DepartmentWindow()
        {
            InitializeComponent();
            LoadDepartmentData();
        }

        // Метод для загрузки данных об отделах из базы данных
        private void LoadDepartmentData()
        {
            departmentAdapter.Fill(autocarsDataSet.Department);
            DepartmentDataGrid.ItemsSource = autocarsDataSet.Department.DefaultView;
        }

        // Обработчик для добавления нового отдела
        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            AddDepartmentWindow addDepartmentWindow = new AddDepartmentWindow();
            addDepartmentWindow.ShowDialog();
            LoadDepartmentData();
        }

        // Обработчик для редактирования выбранного отдела
        private void EditDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)DepartmentDataGrid.SelectedItem;
                EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(selectedRow);
                editDepartmentWindow.ShowDialog();
                LoadDepartmentData();
            }
            else
            {
                MessageBox.Show("Выберите отдел для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработчик для удаления выбранного отдела
        private void DeleteDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)DepartmentDataGrid.SelectedItem;
                int id = (int)selectedRow["id"];

                if (MessageBox.Show($"Вы уверены, что хотите удалить отдел с ID {id}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    departmentAdapter.DeleteQuery(id);
                    LoadDepartmentData();
                }
            }
            else
            {
                MessageBox.Show("Выберите отдел для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
