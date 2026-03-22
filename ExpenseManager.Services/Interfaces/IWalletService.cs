using ExpenseManager.Services.DTOs;

namespace ExpenseManager.Services.Interfaces
{
    public interface IWalletService
    {
        IEnumerable<WalletListDto> GetAllWallets();
        WalletDetailsDto? GetWalletById(int id);
        void SaveWallet(WalletSaveDto walletDto);
        IEnumerable<string> GetAvailableCurrencies();
    }
}