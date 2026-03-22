using ExpenseManager.Domain.Enums;

namespace ExpenseManager.Domain
{
    public record Transaction(
        int Id,
        int WalletId,
        decimal Amount,
        TransactionCategory Category,
        string Description,
        DateTime DateTime
    );
}