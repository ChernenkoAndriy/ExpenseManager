using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ExpenseManager.Data;
using ExpenseManager.ViewModels;

namespace ExpenseManager.UI
{
    public partial class AddTransactionPage : Page
    {
        private readonly IExpenseService _expenseService;
        private readonly TransactionViewModel _transactionVm;

        public AddTransactionPage(IExpenseService expenseService, int walletId)
        {
            InitializeComponent();
            _expenseService = expenseService;

            _transactionVm = new TransactionViewModel
            {
                WalletId = walletId,
                Category = "Other"
            };

            var context = new AddTransactionWrapper(_transactionVm, _expenseService.GetCategories());
            this.DataContext = context;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _expenseService.AddTransaction(_transactionVm);
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

        private class AddTransactionWrapper
        {
            public TransactionViewModel Transaction { get; }
            public IEnumerable<string> AvailableCategories { get; }

            public decimal Amount
            {
                get => Transaction.Amount;
                set => Transaction.Amount = value;
            }

            public string Category
            {
                get => Transaction.Category;
                set => Transaction.Category = value;
            }

            public string Description
            {
                get => Transaction.Description;
                set => Transaction.Description = value;
            }

            public AddTransactionWrapper(TransactionViewModel transaction, IEnumerable<string> categories)
            {
                Transaction = transaction;
                AvailableCategories = categories;
            }
        }
    }
}