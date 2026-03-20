using System.Collections.Generic;
using ExpenseManager.ViewModels;

namespace ExpenseManager.Data
{
    public interface IExpenseService
    {
        IEnumerable<WalletViewModel> GetAllWallets();
        WalletViewModel? GetWalletById(int id);
        IEnumerable<TransactionViewModel> GetTransactionsByWalletId(int walletId);
        void AddTransaction(TransactionViewModel transactionVm);
        void DeleteTransaction(int transactionId);
        void AddWallet(WalletViewModel walletVm);
        void UpdateWallet(WalletViewModel walletVm);
        void DeleteWallet(int walletId);
        IEnumerable<string> GetCategories();
        IEnumerable<string> GetCurrencies();
    }
}