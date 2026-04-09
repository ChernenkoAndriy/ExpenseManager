using ExpenseManager.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Services.Interfaces
{
    public interface IWalletService
    {
        Task<IEnumerable<WalletListDto>> GetAllWalletsAsync();
        Task<WalletDetailsDto?> GetWalletByIdAsync(int id);
        Task SaveWalletAsync(WalletSaveDto walletDto);
        IEnumerable<string> GetAvailableCurrencies(); 
    }
}