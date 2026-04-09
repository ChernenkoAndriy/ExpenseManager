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
                LoadDataAsync(walletId);
            }
        }

        private async void LoadDataAsync(int id)
        {
            try
            {
                IsBusy = true; 
                Wallet = await _walletService.GetWalletByIdAsync(id);
            }
            finally
            {
                IsBusy = false;
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

                    Wallet = await _walletService.GetWalletByIdAsync(Wallet.Id);
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