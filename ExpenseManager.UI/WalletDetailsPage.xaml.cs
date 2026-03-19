using ExpenseManager.Data;
using ExpenseManager.Models;
using ExpenseManager.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager.UI
{
    public partial class WalletDetailsPage : Page
    {
        private readonly IExpenseService _expenseService;
        private readonly WalletViewModel _walletVm;

        public WalletDetailsPage(IExpenseService expenseService, WalletViewModel walletVm)
        {
            InitializeComponent();
            _expenseService = expenseService;
            _walletVm = walletVm;
            DisplayWalletInfo();
        }

        private void DisplayWalletInfo()
        {
            WalletNameText.Text = _walletVm.Name;

            var transactions = _expenseService.GetTransactionsByWalletId(_walletVm.Id).ToList();
            TransactionsDataGrid.ItemsSource = transactions;

            decimal balance = transactions.Sum(t => t.Amount);
            WalletBalanceText.Text = $"Balance: {_walletVm.TotalBalance} {_walletVm.Currency}";
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

            var selectedTransactionVm = button?.DataContext as ExpenseManager.ViewModels.TransactionViewModel;

            if (selectedTransactionVm != null)
            {
                var detailsPage = new TransactionDetailsPage(selectedTransactionVm);
                this.NavigationService?.Navigate(detailsPage);
            }
        }
    }
}