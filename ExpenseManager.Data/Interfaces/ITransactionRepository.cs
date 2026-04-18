using ExpenseManager.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetByWalletIdAsync(int walletId);

        Task<Transaction?> GetByIdAsync(int id);

        Task AddAsync(Transaction transaction);

        Task UpdateAsync(Transaction transaction);

        Task DeleteAsync(int id);
    }
}