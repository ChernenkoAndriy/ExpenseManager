using ExpenseManager.Models.Enums;

namespace ExpenseManager.Models
{
    public record Wallet(
        int Id,
        string Name,
        Currency Currency
    );
}