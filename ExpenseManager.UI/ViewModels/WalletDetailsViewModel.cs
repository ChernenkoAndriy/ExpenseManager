using System.Windows.Input;
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

        public WalletDetailsDto? Wallet
        {
            get => _wallet;
            set => SetProperty(ref _wallet, value);
        }

        public ICommand BackCommand { get; }
        public ICommand AddTransactionCommand { get; }
        public ICommand DeleteTransactionCommand { get; }
        public ICommand ViewTransactionCommand { get; }

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
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is int walletId)
            {
                LoadData(walletId);
            }
        }

        private void LoadData(int id)
        {
            Wallet = _walletService.GetWalletById(id);
        }

        private void OnAddTransaction(object? _)
        {
            if (Wallet != null)
            {
                _navigationService.NavigateTo<AddTransactionViewModel>(Wallet.Id);
            }
        }

        private void OnDeleteTransaction(object? parameter)
        {
            if (parameter is TransactionListDto transaction && Wallet != null)
            {
                _transactionService.DeleteTransaction(transaction.Id);
                LoadData(Wallet.Id);
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