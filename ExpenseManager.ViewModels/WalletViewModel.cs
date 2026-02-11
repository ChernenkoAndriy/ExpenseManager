using ExpenseManager.Models;

namespace ExpenseManager.ViewModels
{
    public class WalletViewModel
    {
        private readonly Wallet _wallet;
        
        public List<TransactionViewModel> Transactions { get; set; } = new();

        public WalletViewModel(Wallet wallet)
        {
            _wallet = wallet;
        }

        public string Name => _wallet.Name;
        public string Currency => _wallet.Currency.ToString();

        public decimal TotalBalance => Transactions.Sum(t => t.Amount);

        public override string ToString()
        {
            return $"{Name} | Balance: {TotalBalance} {Currency}";
        }
    }
}