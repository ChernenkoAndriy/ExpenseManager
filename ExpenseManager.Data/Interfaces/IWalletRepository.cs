using ExpenseManager.Domain;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace ExpenseManager.Data.Interfaces
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetAllAsync();
        Task<Wallet?> GetByIdAsync(int id);
        Task AddAsync(Wallet wallet);
        Task UpdateAsync(Wallet wallet);
        Task DeleteAsync(int id);
    }
}