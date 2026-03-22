using ExpenseManager.Services.DTOs;

namespace ExpenseManager.Services.Interfaces
{
    public interface ITransactionService
    {
        void AddTransaction(int walletId, decimal amount, string category, string description);
        void DeleteTransaction(int id);
        IEnumerable<string> GetAvailableCategories();
    }
}