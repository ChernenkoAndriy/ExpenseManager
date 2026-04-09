using ExpenseManager.Data.Interfaces;
using ExpenseManager.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Data.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ExpenseDbContext _dbContext;

        public WalletRepository(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Wallet>> GetAllAsync()
        {
            return await _dbContext.Wallets.AsNoTracking().ToListAsync();
        }

        public async Task<Wallet?> GetByIdAsync(int id)
        {
            return await _dbContext.Wallets.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task AddAsync(Wallet wallet)
        {
            await _dbContext.Wallets.AddAsync(wallet);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            _dbContext.Wallets.Update(wallet);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(w => w.Id == id);
            if (wallet != null)
            {
                _dbContext.Wallets.Remove(wallet);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}