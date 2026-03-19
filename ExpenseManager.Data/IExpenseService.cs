using System.Collections.Generic;
using ExpenseManager.Models;
using ExpenseManager.ViewModels;

namespace ExpenseManager.Data
{
    public interface IExpenseService
    {
        IEnumerable<WalletViewModel> GetAllWallets();
        IEnumerable<TransactionViewModel> GetTransactionsByWalletId(int walletId);
        void AddTransaction(TransactionViewModel transactionVm);
        void UpdateWallet(WalletViewModel walletVm);
    }
}