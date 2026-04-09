using ExpenseManager.Data.Interfaces;
using ExpenseManager.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManager.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ExpenseDbContext _dbContext;

        public TransactionRepository(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transaction>> GetByWalletIdAsync(int walletId)
        {
            return await _dbContext.Transactions
                .Where(t => t.WalletId == walletId)
                .ToListAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            if (transaction != null)
            {
                _dbContext.Transactions.Remove(transaction);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}