using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Models;

namespace ExpenseManager.Data
{
    public class ExpenseService : IExpenseService
    {
        public IEnumerable<Wallet> GetAllWallets()
        {
            return Storage.Wallets;
        }

        public IEnumerable<Transaction> GetTransactionsByWalletId(int walletId)
        {
            return Storage.Transactions.Where(t => t.WalletId == walletId);
        }

        public Transaction GetTransactionById(int transactionId)
        {
            return Storage.Transactions.FirstOrDefault(t => t.Id == transactionId);
        }
    }
}