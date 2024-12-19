using Autobike.AutocarsDataSetTableAdapters;
using System.Data;
using System.Windows;

namespace Autobike
{
    public partial class ClientAccountWindow : Window
    {
        private AccountTableAdapter accountAdapter = new AccountTableAdapter();
        private AutocarsDataSet autocarsDataSet = new AutocarsDataSet();

        public ClientAccountWindow(int clientId)
        {
            InitializeComponent();
            LoadClientAccountData(clientId);
        }

        private void LoadClientAccountData(int clientId)
        {
            accountAdapter.Fill(autocarsDataSet.Account);
            DataView clientBookings = new DataView(autocarsDataSet.Account)
            {
                RowFilter = $"Client = {clientId}"
            };
            ClientAccountDataGrid.ItemsSource = clientBookings;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
