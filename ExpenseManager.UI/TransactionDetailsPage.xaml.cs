using System.Windows.Controls;
using ExpenseManager.ViewModels;

namespace ExpenseManager.UI
{
    public partial class TransactionDetailsPage : Page
    {
        public TransactionDetailsPage(TransactionViewModel transaction)
        {
            InitializeComponent();

            this.DataContext = transaction;

            AmountText.Foreground = transaction.IsExpense
                ? System.Windows.Media.Brushes.Red
                : System.Windows.Media.Brushes.Green;
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}