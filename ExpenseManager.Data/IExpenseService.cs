using System.Collections.Generic;
using ExpenseManager.Models;

namespace ExpenseManager.Data
{
    public interface IExpenseService
    {
        IEnumerable<Wallet> GetAllWallets();
        IEnumerable<Transaction> GetTransactionsByWalletId(int walletId);
        Transaction GetTransactionById(int transactionId);
    }
}