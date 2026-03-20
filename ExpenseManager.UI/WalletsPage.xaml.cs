using System.Windows;
using System.Windows.Controls;
using ExpenseManager.Data;
using ExpenseManager.ViewModels;

namespace ExpenseManager.UI
{
    public partial class WalletsPage : Page
    {
        private readonly IExpenseService _expenseService;

        public WalletsPage(IExpenseService expenseService)
        {
            InitializeComponent();
            _expenseService = expenseService;
            this.Loaded += (s, e) => LoadData();
        }

        private void LoadData()
        {
            WalletsListBox.ItemsSource = _expenseService.GetAllWallets();
        }

        private void AddWalletButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new EditWalletPage(_expenseService));
        }

        private void EditWalletButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is WalletViewModel selectedWallet)
            {
                NavigationService?.Navigate(new EditWalletPage(_expenseService, selectedWallet));
            }
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is WalletViewModel selectedWallet)
            {
                NavigationService?.Navigate(new WalletDetailsPage(_expenseService, selectedWallet));
            }
        }
    }
}