using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Models;
using ExpenseManager.Models.Enums;

namespace ExpenseManager.Data
{
    internal static class Storage
    {
        private static readonly List<Wallet> _wallets = new()
        {
            new Wallet(1, "Main Debit Card", Currency.UAH),
            new Wallet(2, "Cash Wallet", Currency.UAH),
            new Wallet(3, "Travel Fund", Currency.USD)
        };

        private static readonly List<Transaction> _transactions = new()
        {
            new Transaction(1, 1, 25000.00m, TransactionCategory.Salary, "Monthly Salary", DateTime.Now.AddDays(-30)),
            new Transaction(2, 1, -1200.50m, TransactionCategory.Food, "Weekly Groceries", DateTime.Now.AddDays(-25)),
            new Transaction(3, 1, -350.00m, TransactionCategory.Transport, "Fuel Refill", DateTime.Now.AddDays(-20)),
            new Transaction(11, 2, 1000.00m, TransactionCategory.Other, "Cash Gift", DateTime.Now.AddDays(-15)),
            new Transaction(12, 2, -150.00m, TransactionCategory.Food, "Coffee & Croissant", DateTime.Now.AddMinutes(-30))
        };

        public static IReadOnlyCollection<Wallet> Wallets => _wallets.AsReadOnly();
        public static IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        public static void AddWallet(Wallet wallet) => _wallets.Add(wallet);

        public static void UpdateWallet(Wallet updatedWallet)
        {
            var index = _wallets.FindIndex(w => w.Id == updatedWallet.Id);
            if (index != -1) _wallets[index] = updatedWallet;
        }

        public static void DeleteWallet(int walletId)
        {
            _wallets.RemoveAll(w => w.Id == walletId);
            _transactions.RemoveAll(t => t.WalletId == walletId);
        }

        public static void AddTransaction(Transaction transaction) => _transactions.Add(transaction);

        public static void DeleteTransaction(int transactionId)
        {
            _transactions.RemoveAll(t => t.Id == transactionId);
        }
    }
}