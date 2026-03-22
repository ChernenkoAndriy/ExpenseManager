using ExpenseManager.Domain.Enums;

namespace ExpenseManager.Domain
{
    public record Wallet(
        int Id,
        string Name,
        Currency Currency
    );
}