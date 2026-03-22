using ExpenseManager.Data.Interfaces;
using ExpenseManager.Domain;

namespace ExpenseManager.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public IEnumerable<Transaction> GetByWalletId(int walletId)
        {
            return Storage.Transactions.Where(t => t.WalletId == walletId);
        }

        public void Add(Transaction transaction)
        {
            Storage.AddTransaction(transaction);
        }

        public void Delete(int id)
        {
            Storage.DeleteTransaction(id);
        }

        public int GetNextId()
        {
            return Storage.Transactions.Any() ? Storage.Transactions.Max(t => t.Id) + 1 : 1;
        }
    }
}