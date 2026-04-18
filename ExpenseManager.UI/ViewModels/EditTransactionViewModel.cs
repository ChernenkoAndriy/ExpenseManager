using System;
using System.Collections.Generic;
using System.Windows.Input;
using ExpenseManager.Services.Interfaces;
using ExpenseManager.UI.Commands;
using ExpenseManager.UI.Services;
using ExpenseManager.UI.ViewModels.Base;

namespace ExpenseManager.UI.ViewModels
{
    public class EditTransactionViewModel : BaseViewModel, IParameterReceiver
    {
        private readonly ITransactionService _transactionService;
        private readonly INavigationService _navigationService;
        private int _transactionId;
        private int _walletId;
        private decimal _amount;
        private string _description = string.Empty;
        private string _selectedCategory = string.Empty;

        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public IEnumerable<string> Categories { get; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditTransactionViewModel(ITransactionService transactionService, INavigationService navigationService)
        {
            _transactionService = transactionService;
            _navigationService = navigationService;

            Categories = _transactionService.GetAvailableCategories();

            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(_ => _navigationService.NavigateTo<WalletDetailsViewModel>(_walletId));
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is int id)
            {
                _transactionId = id;
                LoadTransactionDataAsync(id);
            }
        }

        private async void LoadTransactionDataAsync(int id)
        {
            try
            {
                IsBusy = true;
                var transaction = await _transactionService.GetTransactionByIdAsync(id);
                if (transaction != null)
                {
                    _walletId = transaction.WalletId;
                    Amount = transaction.Amount;
                    Description = transaction.Description;
                    SelectedCategory = transaction.Category;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnSave(object? _)
        {
            if (Amount == 0)
            {
                System.Windows.MessageBox.Show("Сума не може бути нульовою.", "Помилка",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            try
            {
                IsBusy = true;
                await _transactionService.UpdateTransactionAsync(_transactionId, Amount, SelectedCategory, Description);

                _navigationService.NavigateTo<WalletDetailsViewModel>(_walletId);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Помилка",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}