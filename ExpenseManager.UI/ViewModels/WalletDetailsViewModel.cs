using System;
using System.Windows.Input;
using System.Threading.Tasks;
using ExpenseManager.Services.DTOs;
using ExpenseManager.Services.Interfaces;
using ExpenseManager.UI.Commands;
using ExpenseManager.UI.Services;
using ExpenseManager.UI.ViewModels.Base;

namespace ExpenseManager.UI.ViewModels
{
    public class WalletDetailsViewModel : BaseViewModel, IParameterReceiver
    {
        private readonly IWalletService _walletService;
        private readonly ITransactionService _transactionService;
        private readonly INavigationService _navigationService;
        private WalletDetailsDto? _wallet;
        private string _selectedTypeFilter = "All"; // All, Income, Expense

        public WalletDetailsDto? Wallet
        {
            get => _wallet;
            set => SetProperty(ref _wallet, value);
        }

        public string SelectedTypeFilter
        {
            get => _selectedTypeFilter;
            set
            {
                if (SetProperty(ref _selectedTypeFilter, value) && Wallet != null)
                    LoadDataAsync(Wallet.Id);
            }
        }

        public ICommand BackCommand { get; }
        public ICommand AddTransactionCommand { get; }
        public ICommand DeleteTransactionCommand { get; }
        public ICommand ViewTransactionCommand { get; }
        public ICommand EditTransactionCommand { get; } // НОВО

        public WalletDetailsViewModel(
            IWalletService walletService,
            ITransactionService transactionService,
            INavigationService navigationService)
        {
            _walletService = walletService;
            _transactionService = transactionService;
            _navigationService = navigationService;

            BackCommand = new RelayCommand(_ => _navigationService.NavigateTo<WalletsViewModel>());
            AddTransactionCommand = new RelayCommand(OnAddTransaction);
            DeleteTransactionCommand = new RelayCommand(OnDeleteTransaction);
            ViewTransactionCommand = new RelayCommand(OnViewTransaction);
            EditTransactionCommand = new RelayCommand(OnEditTransaction); // НОВО
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is int walletId)
            {
                LoadDataAsync(walletId);
            }
        }

        private async void LoadDataAsync(int id)
        {
            try
            {
                IsBusy = true;
                // Викликаємо розширений метод сервісу з фільтром (буде додано на етапі 3)
                Wallet = await _walletService.GetWalletByIdAsync(id, SelectedTypeFilter);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnEditTransaction(object? parameter)
        {
            if (parameter is TransactionListDto transaction)
            {
                // Навігація до нової VM (яку створимо в пункті 3 цього етапу)
                _navigationService.NavigateTo<EditTransactionViewModel>(transaction.Id);
            }
        }

        private void OnAddTransaction(object? _)
        {
            if (Wallet != null)
            {
                _navigationService.NavigateTo<AddTransactionViewModel>(Wallet.Id);
            }
        }

        private async void OnDeleteTransaction(object? parameter)
        {
            if (parameter is TransactionListDto transaction && Wallet != null)
            {
                try
                {
                    IsBusy = true;
                    await _transactionService.DeleteTransactionAsync(transaction.Id);
                    LoadDataAsync(Wallet.Id);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        private void OnViewTransaction(object? parameter)
        {
            if (parameter is TransactionListDto transaction)
            {
                _navigationService.NavigateTo<TransactionDetailsViewModel>(transaction);
            }
        }
    }
}