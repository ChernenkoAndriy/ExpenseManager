using ExpenseManager.Models;

namespace ExpenseManager.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;

        public bool IsExpense => Amount < 0;
        public string TransactionType => IsExpense ? "Expense" : "Income";
        public string FormattedDate => DateTime.ToString("dd.MM.yyyy HH:mm");

        public TransactionViewModel() { } 

        public TransactionViewModel(Transaction transaction)
        {
            Id = transaction.Id;
            WalletId = transaction.WalletId;
            Amount = transaction.Amount;
            Category = transaction.Category.ToString();
            Description = transaction.Description;
            DateTime = transaction.DateTime;
        }

        public override string ToString()
        {
            string type = IsExpense ? "[Expense]" : "[Income]";
            return $"{FormattedDate} | {type} {Category}: {Amount} ({Description})";
        }
    }
}