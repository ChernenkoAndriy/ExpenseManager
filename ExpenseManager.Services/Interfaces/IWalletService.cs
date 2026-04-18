using ExpenseManager.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Services.Interfaces
{
    public interface IWalletService
    {
        Task<IEnumerable<WalletListDto>> GetAllWalletsAsync(string? currencyFilter = null, bool sortByName = false);

        Task<WalletDetailsDto?> GetWalletByIdAsync(int id, string? transactionTypeFilter = "All");

        Task SaveWalletAsync(WalletSaveDto walletDto);

        Task DeleteWalletAsync(int id);

        IEnumerable<string> GetAvailableCurrencies();
    }
}