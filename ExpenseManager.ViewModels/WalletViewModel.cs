using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExpenseManager.Models;

namespace ExpenseManager.ViewModels
{
    public class WalletViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name = string.Empty;
        private string _currency = string.Empty;
        private ObservableCollection<TransactionViewModel> _transactions = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public WalletViewModel() { }

        public WalletViewModel(Wallet wallet)
        {
            _id = wallet.Id;
            _name = wallet.Name;
            _currency = wallet.Currency.ToString();
        }

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public string Currency
        {
            get => _currency;
            set => SetField(ref _currency, value);
        }

        public ObservableCollection<TransactionViewModel> Transactions
        {
            get => _transactions;
            set
            {
                if (SetField(ref _transactions, value))
                {
                    OnPropertyChanged(nameof(TotalBalance));
                }
            }
        }

        public decimal TotalBalance => Transactions.Sum(t => t.Amount);

        public override string ToString()
        {
            return $"{Name} | Баланс: {TotalBalance:N2} {Currency}";
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}