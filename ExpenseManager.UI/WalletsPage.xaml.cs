using System.Windows;
using System.Windows.Controls;
using ExpenseManager.Data;

namespace ExpenseManager.UI
{
    public partial class WalletsPage : Page
    {
        private readonly IExpenseService _expenseService;

        public WalletsPage(IExpenseService expenseService)
        {
            InitializeComponent();
            _expenseService = expenseService;

            LoadWallets();
        }

        private void LoadWallets()
        {
            WalletsListBox.ItemsSource = _expenseService.GetAllWallets();
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var selectedWallet = button?.DataContext as ExpenseManager.Models.Wallet;

            if (selectedWallet != null)
            {
                var detailsPage = new WalletDetailsPage(_expenseService, selectedWallet);

                if (this.NavigationService != null)
                {
                    this.NavigationService.Navigate(detailsPage);
                }
                else
                {
                    MessageBox.Show("Navigation error: Frame not found.");
                }
            }
        }
    }
}