using ExpenseManager.Models.Enums;

namespace ExpenseManager.Models
{
    public record Transaction(
        int Id,
        int WalletId,
        decimal Amount,
        TransactionCategory Category,
        string Description,
        DateTime DateTime
    )
    {
        public bool IsExpense => Amount < 0;
    }
}