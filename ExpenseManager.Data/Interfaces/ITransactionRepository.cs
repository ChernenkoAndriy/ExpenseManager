using ExpenseManager.Domain;

namespace ExpenseManager.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetByWalletIdAsync(int walletId);
        Task AddAsync(Transaction transaction);
        Task DeleteAsync(int id);
    }
}