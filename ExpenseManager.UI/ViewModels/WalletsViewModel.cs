using System.Collections.ObjectModel;
using System.Windows.Input;
using ExpenseManager.Services.DTOs;
using ExpenseManager.Services.Interfaces;
using ExpenseManager.UI.Commands;
using ExpenseManager.UI.Services;
using ExpenseManager.UI.ViewModels.Base;

namespace ExpenseManager.UI.ViewModels
{
    public class WalletsViewModel : BaseViewModel
    {
        private readonly IWalletService _walletService;
        private readonly INavigationService _navigationService;
        private ObservableCollection<WalletListDto> _wallets = new();

        public ObservableCollection<WalletListDto> Wallets
        {
            get => _wallets;
            set => SetProperty(ref _wallets, value);
        }

        public ICommand ViewDetailsCommand { get; }
        public ICommand AddWalletCommand { get; }
        public ICommand EditWalletCommand { get; }

        public WalletsViewModel(IWalletService walletService, INavigationService navigationService)
        {
            _walletService = walletService;
            _navigationService = navigationService;

            ViewDetailsCommand = new RelayCommand(OnViewDetails);
            AddWalletCommand = new RelayCommand(_ => _navigationService.NavigateTo<EditWalletViewModel>());
            EditWalletCommand = new RelayCommand(OnEditWallet);

            LoadData();
        }

        private void LoadData()
        {
            var data = _walletService.GetAllWallets();
            Wallets = new ObservableCollection<WalletListDto>(data);
        }

        private void OnViewDetails(object? parameter)
        {
            if (parameter is WalletListDto wallet)
            {
                _navigationService.NavigateTo<WalletDetailsViewModel>(wallet.Id);
            }
        }

        private void OnEditWallet(object? parameter)
        {
            if (parameter is WalletListDto wallet)
            {
                _navigationService.NavigateTo<EditWalletViewModel>(wallet.Id);
            }
        }
    }
}