using System.Windows;

namespace Autobike
{
    public partial class UserMain : Window
    {
        public UserMain()
        {
            InitializeComponent();
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow();
            bookingWindow.Show();
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow();
            accountWindow.Show();
        }

        // Close button functionality
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        private void PersonalAccountButton_Click(object sender, RoutedEventArgs e)
        {
            UserProfileWindow userProfileWindow = new UserProfileWindow();
            userProfileWindow.Show();
        }
    }
}
