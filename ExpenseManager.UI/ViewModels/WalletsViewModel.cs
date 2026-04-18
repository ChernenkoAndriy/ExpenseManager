using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
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

        private string _selectedCurrency = "Усі"; // За замовчуванням без фільтра
        private bool _isSortByNameEnabled;
        private IEnumerable<string> _currencies = new List<string>();

        public ObservableCollection<WalletListDto> Wallets
        {
            get => _wallets;
            set => SetProperty(ref _wallets, value);
        }

        public IEnumerable<string> Currencies
        {
            get => _currencies;
            set => SetProperty(ref _currencies, value);
        }

        public string SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                if (SetProperty(ref _selectedCurrency, value))
                    LoadDataAsync();
            }
        }

        public bool IsSortByNameEnabled
        {
            get => _isSortByNameEnabled;
            set
            {
                if (SetProperty(ref _isSortByNameEnabled, value))
                    LoadDataAsync();
            }
        }

        public ICommand ViewDetailsCommand { get; }
        public ICommand AddWalletCommand { get; }
        public ICommand EditWalletCommand { get; }
        public ICommand DeleteWalletCommand { get; } // НОВО

        public WalletsViewModel(IWalletService walletService, INavigationService navigationService)
        {
            _walletService = walletService;
            _navigationService = navigationService;

            ViewDetailsCommand = new RelayCommand(OnViewDetails);
            AddWalletCommand = new RelayCommand(_ => _navigationService.NavigateTo<EditWalletViewModel>());
            EditWalletCommand = new RelayCommand(OnEditWallet);
            DeleteWalletCommand = new RelayCommand(OnDeleteWallet); // НОВО

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            // Отримуємо список валют для фільтра, додаючи варіант "Усі"
            var available = _walletService.GetAvailableCurrencies().ToList();
            available.Insert(0, "Усі");
            Currencies = available;

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                IsBusy = true;

                // Викликаємо метод сервісу з новими параметрами (будуть додані на етапі 3)
                string? filterCurrency = SelectedCurrency == "Усі" ? null : SelectedCurrency;
                var data = await _walletService.GetAllWalletsAsync(filterCurrency, IsSortByNameEnabled);

                Wallets = new ObservableCollection<WalletListDto>(data);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnDeleteWallet(object? parameter)
        {
            if (parameter is WalletListDto wallet)
            {
                var result = System.Windows.MessageBox.Show(
                    $"Ви впевнені, що хочете видалити гаманець '{wallet.Name}' та всі його транзакції?",
                    "Підтвердження", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Warning);

                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    await _walletService.DeleteWalletAsync(wallet.Id); // Метод буде додано на етапі 3
                    LoadDataAsync();
                }
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