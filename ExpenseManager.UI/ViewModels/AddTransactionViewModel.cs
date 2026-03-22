using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ExpenseManager.Services.Interfaces;
using ExpenseManager.UI.Commands;
using ExpenseManager.UI.Services;
using ExpenseManager.UI.ViewModels.Base;

namespace ExpenseManager.UI.ViewModels
{
    public class AddTransactionViewModel : BaseViewModel, IParameterReceiver
    {
        private readonly ITransactionService _transactionService;
        private readonly INavigationService _navigationService;
        private int _walletId;
        private decimal _amount;
        private string _description = string.Empty;
        private string _selectedCategory;

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

        public AddTransactionViewModel(ITransactionService transactionService, INavigationService navigationService)
        {
            _transactionService = transactionService;
            _navigationService = navigationService;
            Categories = _transactionService.GetAvailableCategories();

            // Встановлюємо першу категорію за замовчуванням, щоб уникнути помилок збереження
            _selectedCategory = Categories.FirstOrDefault() ?? "Other";

            // Прибираємо умову з RelayCommand, щоб кнопка була активна завжди
            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(_ => _navigationService.NavigateTo<WalletDetailsViewModel>(_walletId));
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is int id)
                _walletId = id;
        }

        private void OnSave(object? _)
        {
            if (Amount == 0)
            {
                System.Windows.MessageBox.Show("Сума не може бути нульовою.", "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            try
            {
                _transactionService.AddTransaction(_walletId, Amount, SelectedCategory, Description);
                _navigationService.NavigateTo<WalletDetailsViewModel>(_walletId);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }
}