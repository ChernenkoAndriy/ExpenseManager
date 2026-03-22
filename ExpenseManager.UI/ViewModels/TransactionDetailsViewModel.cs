using System.Windows.Input;
using ExpenseManager.Services.DTOs;
using ExpenseManager.UI.Commands;
using ExpenseManager.UI.Services;
using ExpenseManager.UI.ViewModels.Base;

namespace ExpenseManager.UI.ViewModels
{
    public class TransactionDetailsViewModel : BaseViewModel, IParameterReceiver
    {
        private readonly INavigationService _navigationService;
        private TransactionListDto? _transaction;

        public TransactionListDto? Transaction
        {
            get => _transaction;
            set => SetProperty(ref _transaction, value);
        }

        public ICommand BackCommand { get; }

        public TransactionDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            BackCommand = new RelayCommand(OnBack);
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is TransactionListDto dto)
                Transaction = dto;
        }

        private void OnBack(object? _)
        {
            if (Transaction != null)
                _navigationService.NavigateTo<WalletDetailsViewModel>(Transaction.WalletId);
        }
    }
}