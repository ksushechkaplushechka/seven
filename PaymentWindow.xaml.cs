using System.Windows;

namespace Autobike
{
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Оплата прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Открываем окно AccountWindow после успешной оплаты
            AccountWindow accountWindow = new AccountWindow();
            accountWindow.Show();

            this.Close();
        }
    }
}
