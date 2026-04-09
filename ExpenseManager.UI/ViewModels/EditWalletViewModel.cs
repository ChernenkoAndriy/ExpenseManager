using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks; // ДОДАНО: для роботи з асинхронністю
using ExpenseManager.Services.Interfaces;
using ExpenseManager.UI.Commands;
using ExpenseManager.UI.Services;
using ExpenseManager.UI.ViewModels.Base;
using ExpenseManager.Services.DTOs;

namespace ExpenseManager.UI.ViewModels
{
    public class EditWalletViewModel : BaseViewModel, IParameterReceiver
    {
        private readonly IWalletService _walletService;
        private readonly INavigationService _navigationService;
        private string _name = string.Empty;
        private string _selectedCurrency = "UAH";
        private int? _editingWalletId;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string SelectedCurrency
        {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value);
        }

        public IEnumerable<string> Currencies { get; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditWalletViewModel(IWalletService walletService, INavigationService navigationService)
        {
            _walletService = walletService;
            _navigationService = navigationService;

            Currencies = _walletService.GetAvailableCurrencies();

            SaveCommand = new RelayCommand(OnSave, _ => !string.IsNullOrWhiteSpace(Name));
            CancelCommand = new RelayCommand(_ => _navigationService.NavigateTo<WalletsViewModel>());
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is int id)
            {
                _editingWalletId = id;
                LoadWalletDataAsync(id);
            }
        }

        private async void LoadWalletDataAsync(int id)
        {
            try
            {
                IsBusy = true;
                var wallet = await _walletService.GetWalletByIdAsync(id);
                if (wallet != null)
                {
                    Name = wallet.Name;
                    SelectedCurrency = wallet.Currency;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnSave(object? _)
        {
            var walletDto = new WalletSaveDto
            {
                Id = _editingWalletId,
                Name = Name,
                Currency = SelectedCurrency
            };

            try
            {
                IsBusy = true; 

                await _walletService.SaveWalletAsync(walletDto);

                _navigationService.NavigateTo<WalletsViewModel>();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}