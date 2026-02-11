using ExpenseManager.Models.Enums;

namespace ExpenseManager.Models
{
    public class Transaction
    {
        public int Id { get; init; }
        public int WalletId { get; init; } 
        public decimal Amount { get; set; }
        public TransactionCategory Category { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public Transaction(int id, int walletId, decimal amount, TransactionCategory category, string description)
        {
            Id = id;
            WalletId = walletId;
            Amount = amount;
            Category = category;
            Description = description;
            DateTime = DateTime.Now;
        }
    }
}