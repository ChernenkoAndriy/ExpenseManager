using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
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
            set
            {
                _wallets = value;
                OnPropertyChanged(); 
            }
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

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                IsBusy = true;

                var data = await _walletService.GetAllWalletsAsync();

                Wallets = new ObservableCollection<WalletListDto>(data);
            }
            finally
            {
                IsBusy = false;
            }
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