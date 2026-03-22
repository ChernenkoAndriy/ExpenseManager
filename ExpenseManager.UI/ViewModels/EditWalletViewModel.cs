using System.Windows.Input;
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
                var wallet = _walletService.GetWalletById(id);
                if (wallet != null)
                {
                    Name = wallet.Name;
                    SelectedCurrency = wallet.Currency;
                }
            }
        }

        private void OnSave(object? _)
        {
            var walletDto = new WalletSaveDto
            {
                Id = _editingWalletId,
                Name = Name,
                Currency = SelectedCurrency
            };

            _walletService.SaveWallet(walletDto);
            _navigationService.NavigateTo<WalletsViewModel>();
        }
    }
}