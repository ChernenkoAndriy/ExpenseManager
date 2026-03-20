using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExpenseManager.Models;

namespace ExpenseManager.ViewModels
{
    public class TransactionViewModel : INotifyPropertyChanged
    {
        private int _id;
        private int _walletId;
        private decimal _amount;
        private string _category = string.Empty;
        private string _description = string.Empty;
        private DateTime _dateTime = DateTime.Now;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TransactionViewModel() { }

        public TransactionViewModel(Transaction transaction)
        {
            _id = transaction.Id;
            _walletId = transaction.WalletId;
            _amount = transaction.Amount;
            _category = transaction.Category.ToString();
            _description = transaction.Description;
            _dateTime = transaction.DateTime;
        }

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public int WalletId
        {
            get => _walletId;
            set => SetField(ref _walletId, value);
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                if (SetField(ref _amount, value))
                {
                    OnPropertyChanged(nameof(IsExpense));
                    OnPropertyChanged(nameof(TransactionType));
                }
            }
        }

        public string Category
        {
            get => _category;
            set => SetField(ref _category, value);
        }

        public string Description
        {
            get => _description;
            set => SetField(ref _description, value);
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                if (SetField(ref _dateTime, value))
                {
                    OnPropertyChanged(nameof(FormattedDate));
                }
            }
        }

        public bool IsExpense => Amount < 0;
        public string TransactionType => IsExpense ? "Expense" : "Income";
        public string FormattedDate => DateTime.ToString("dd.MM.yyyy HH:mm");

        public override string ToString()
        {
            string type = IsExpense ? "[Expense]" : "[Income]";
            return $"{FormattedDate} | {type} {Category}: {Amount} ({Description})";
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