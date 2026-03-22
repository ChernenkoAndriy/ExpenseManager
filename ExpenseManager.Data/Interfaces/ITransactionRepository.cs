using ExpenseManager.Domain;

namespace ExpenseManager.Data.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetByWalletId(int walletId);
        void Add(Transaction transaction);
        void Delete(int id);
        int GetNextId();
    }
}