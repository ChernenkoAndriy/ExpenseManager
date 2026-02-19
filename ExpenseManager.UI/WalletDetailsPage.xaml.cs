using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ExpenseManager.Data;
using ExpenseManager.Models;

namespace ExpenseManager.UI
{
    public partial class WalletDetailsPage : Page
    {
        private readonly IExpenseService _expenseService;
        private readonly Wallet _wallet;

        public WalletDetailsPage(IExpenseService expenseService, Wallet wallet)
        {
            InitializeComponent();
            _expenseService = expenseService;
            _wallet = wallet;

            DisplayWalletInfo();
        }

        private void DisplayWalletInfo()
        {
            WalletNameText.Text = _wallet.Name;

            var transactions = _expenseService.GetTransactionsByWalletId(_wallet.Id).ToList();
            TransactionsDataGrid.ItemsSource = transactions;

            decimal balance = transactions.Sum(t => t.Amount);
            WalletBalanceText.Text = $"Balance: {balance} {_wallet.Currency}";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void TransactionDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var selectedTransaction = button?.DataContext as ExpenseManager.Models.Transaction;

            if (selectedTransaction != null)
            {
                var detailsPage = new TransactionDetailsPage(selectedTransaction);

                this.NavigationService.Navigate(detailsPage);
            }
        }
    }
}