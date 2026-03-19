using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Models;
using ExpenseManager.Models.Enums;
using ExpenseManager.ViewModels;

namespace ExpenseManager.Data
{
    public class ExpenseService : IExpenseService
    {
        public IEnumerable<WalletViewModel> GetAllWallets()
        {
            return Storage.Wallets.Select(w => new WalletViewModel(w));
        }

        public IEnumerable<TransactionViewModel> GetTransactionsByWalletId(int walletId)
        {
            return Storage.Transactions
                .Where(t => t.WalletId == walletId)
                .Select(t => new TransactionViewModel(t));
        }

        public void AddTransaction(TransactionViewModel vm)
        {
            var category = Enum.Parse<TransactionCategory>(vm.Category);
            var transaction = new Transaction(vm.Id, vm.WalletId, vm.Amount, category, vm.Description);
            Storage.AddTransaction(transaction);
        }

        public void UpdateWallet(WalletViewModel vm)
        {
            var wallet = Storage.Wallets.FirstOrDefault(w => w.Id == vm.Id);
            if (wallet != null)
            {
                wallet.Name = vm.Name;
                wallet.Currency = Enum.Parse<Currency>(vm.Currency);
            }
        }
    }
}