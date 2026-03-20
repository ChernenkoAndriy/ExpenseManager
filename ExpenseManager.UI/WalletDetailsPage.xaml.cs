using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ExpenseManager.Data;
using ExpenseManager.ViewModels;
using System.Collections.ObjectModel;

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
            this.Loaded += (s, e) => RefreshData();
        }

        private void RefreshData()
        {
            var transactions = _expenseService.GetTransactionsByWalletId(_walletVm.Id).ToList();
            _walletVm.Transactions = new ObservableCollection<TransactionViewModel>(transactions);

            var updatedWallet = _expenseService.GetWalletById(_walletVm.Id);
            if (updatedWallet != null)
            {
                _walletVm.Name = updatedWallet.Name;
                _walletVm.Currency = updatedWallet.Currency;
            }

            TransactionsDataGrid.ItemsSource = _walletVm.Transactions;
            WalletNameText.Text = _walletVm.Name;
            WalletBalanceText.Text = $"Баланс: {_walletVm.TotalBalance:N2} {_walletVm.Currency}";

            if (_walletVm.TotalBalance >= 0)
                WalletBalanceText.Foreground = System.Windows.Media.Brushes.Green;
            else
                WalletBalanceText.Foreground = System.Windows.Media.Brushes.Red;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddTransactionPage(_expenseService, _walletVm.Id));
        }

        private void TransactionDetails_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is TransactionViewModel selectedTransaction)
            {
                NavigationService?.Navigate(new TransactionDetailsPage(selectedTransaction));
            }
        }

        private void DeleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is TransactionViewModel selectedTransaction)
            {
                var result = MessageBox.Show("Ви впевнені, що хочете видалити цю транзакцію?",
                    "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _expenseService.DeleteTransaction(selectedTransaction.Id);
                    RefreshData();
                }
            }
        }
    }
}