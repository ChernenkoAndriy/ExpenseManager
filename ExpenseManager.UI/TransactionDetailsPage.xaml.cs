using System.Windows;
using System.Windows.Controls;
using ExpenseManager.Models;

namespace ExpenseManager.UI
{
    public partial class TransactionDetailsPage : Page
    {
        public TransactionDetailsPage(Transaction transaction)
        {
            InitializeComponent();
            DisplayDetails(transaction);
        }

        private void DisplayDetails(Transaction t)
        {
            AmountText.Text = t.Amount.ToString("N2");
            CategoryText.Text = t.Category.ToString();
            DateText.Text = t.DateTime.ToString("f"); 
            DescriptionText.Text = string.IsNullOrWhiteSpace(t.Description)
                ? "No description provided"
                : t.Description;

            AmountText.Foreground = t.Amount < 0
                ? System.Windows.Media.Brushes.Red
                : System.Windows.Media.Brushes.Green;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}