using ExpenseManager.Models;
using ExpenseManager.ViewModels;

namespace ExpenseManager.Data
{
    public class ExpenseService
    {
        public List<WalletViewModel> GetWallets()
        {
            return Storage.Wallets.Select(w => new WalletViewModel(w)).ToList();
        }
        
        public void LoadTransactionsForWallet(WalletViewModel walletVm)
        {
            var transactions = Storage.Transactions
                .Where(t => t.WalletId == Storage.Wallets.First(w => w.Name == walletVm.Name).Id)
                .Select(t => new TransactionViewModel(t))
                .ToList();

            walletVm.Transactions = transactions;
        }
    }
}