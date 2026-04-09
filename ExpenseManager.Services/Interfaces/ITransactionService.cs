using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Services.Interfaces
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(int walletId, decimal amount, string category, string description);
        Task DeleteTransactionAsync(int id);
        IEnumerable<string> GetAvailableCategories();
    }
}