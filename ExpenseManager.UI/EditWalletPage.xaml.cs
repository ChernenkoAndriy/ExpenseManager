using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ExpenseManager.Data;
using ExpenseManager.ViewModels;

namespace ExpenseManager.UI
{
    public partial class EditWalletPage : Page
    {
        private readonly IExpenseService _expenseService;
        private readonly WalletViewModel _walletVm;
        private readonly bool _isNew;

        public EditWalletPage(IExpenseService expenseService, WalletViewModel? walletVm = null)
        {
            InitializeComponent();
            _expenseService = expenseService;

            if (walletVm == null)
            {
                _walletVm = new WalletViewModel { Currency = "UAH" };
                _isNew = true;
                PageTitle.SourceUpdated += (s, e) => { }; // Dummy for title change
                PageTitle.Text = "Новий гаманець";
            }
            else
            {
                _walletVm = walletVm;
                _isNew = false;
            }

            var context = new EditWalletWrapper(_walletVm, _expenseService.GetCurrencies());
            this.DataContext = context;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isNew)
                {
                    _expenseService.AddWallet(_walletVm);
                }
                else
                {
                    _expenseService.UpdateWallet(_walletVm);
                }

                NavigationService?.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private class EditWalletWrapper
        {
            public WalletViewModel Wallet { get; }
            public IEnumerable<string> AvailableCurrencies { get; }
            public string Name { get => Wallet.Name; set => Wallet.Name = value; }
            public string Currency { get => Wallet.Currency; set => Wallet.Currency = value; }

            public EditWalletWrapper(WalletViewModel wallet, IEnumerable<string> currencies)
            {
                Wallet = wallet;
                AvailableCurrencies = currencies;
            }
        }
    }
}