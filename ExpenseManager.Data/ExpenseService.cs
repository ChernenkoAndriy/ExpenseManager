using System;
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

        public WalletViewModel? GetWalletById(int id)
        {
            var wallet = Storage.Wallets.FirstOrDefault(w => w.Id == id);
            return wallet != null ? new WalletViewModel(wallet) : null;
        }

        public IEnumerable<TransactionViewModel> GetTransactionsByWalletId(int walletId)
        {
            return Storage.Transactions
                .Where(t => t.WalletId == walletId)
                .OrderByDescending(t => t.DateTime)
                .Select(t => new TransactionViewModel(t));
        }

        public void AddTransaction(TransactionViewModel vm)
        {
            if (vm.Amount == 0) throw new ArgumentException("—ума не може бути нульовою.");

            var category = Enum.Parse<TransactionCategory>(vm.Category);

            int newId = Storage.Transactions.Any() ? Storage.Transactions.Max(t => t.Id) + 1 : 1;

            var transaction = new Transaction(
                newId,
                vm.WalletId,
                vm.Amount,
                category,
                vm.Description,
                DateTime.Now
            );

            Storage.AddTransaction(transaction);
        }

        public void DeleteTransaction(int transactionId)
        {
            Storage.DeleteTransaction(transactionId);
        }

        public void AddWallet(WalletViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Name)) throw new ArgumentException("Ќазва гаманц€ не може бути порожньою.");

            var currency = Enum.Parse<Currency>(vm.Currency);
            int newId = Storage.Wallets.Any() ? Storage.Wallets.Max(w => w.Id) + 1 : 1;

            var wallet = new Wallet(newId, vm.Name, currency);
            Storage.AddWallet(wallet);
        }

        public void UpdateWallet(WalletViewModel vm)
        {
            var currency = Enum.Parse<Currency>(vm.Currency);
            var updatedWallet = new Wallet(vm.Id, vm.Name, currency);
            Storage.UpdateWallet(updatedWallet);
        }

        public void DeleteWallet(int walletId)
        {
            Storage.DeleteWallet(walletId);
        }

        public IEnumerable<string> GetCategories()
        {
            return Enum.GetNames(typeof(TransactionCategory));
        }

        public IEnumerable<string> GetCurrencies()
        {
            return Enum.GetNames(typeof(Currency));
        }
    }
}