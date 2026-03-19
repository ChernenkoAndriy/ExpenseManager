using ExpenseManager.Models;
using System.Collections.ObjectModel;

namespace ExpenseManager.ViewModels
{
    public class WalletViewModel
    {
        private int _id;
        private string _name = string.Empty;
        private string _currency = string.Empty;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Currency { get => _currency; set => _currency = value; }

        public ObservableCollection<TransactionViewModel> Transactions { get; set; } = new();

        public decimal TotalBalance => Transactions.Sum(t => t.Amount);

        public WalletViewModel() { } 

        public WalletViewModel(Wallet wallet)
        {
            Id = wallet.Id;
            Name = wallet.Name;
            Currency = wallet.Currency.ToString();
        }

        public override string ToString()
        {
            return $"{Name} | Balance: {TotalBalance} {Currency}";
        }
    }
}