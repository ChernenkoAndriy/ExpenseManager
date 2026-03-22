using ExpenseManager.Domain;
using ExpenseManager.Domain.Enums;

namespace ExpenseManager.Data
{
    internal static class Storage
    {
        private static readonly List<Wallet> _wallets = new()
        {
            new Wallet(1, "Основна картка", Currency.UAH),
            new Wallet(2, "Готівка", Currency.UAH),
            new Wallet(3, "Travel Fund", Currency.USD)
        };

        private static readonly List<Transaction> _transactions = new()
        {
            new Transaction(1, 1, 15000.00m, TransactionCategory.Salary, "Зарплата", DateTime.Now.AddDays(-5)),
            new Transaction(2, 1, -1200.50m, TransactionCategory.Food, "Продукти", DateTime.Now.AddDays(-3)),
            new Transaction(3, 2, -350.00m, TransactionCategory.Transport, "Пальне", DateTime.Now.AddDays(-1)),
            new Transaction(4, 2, 1000.00m, TransactionCategory.Other, "Подарунок", DateTime.Now.AddDays(-10))
        };

        public static List<Wallet> Wallets => _wallets;
        public static List<Transaction> Transactions => _transactions;

        public static void AddWallet(Wallet wallet) => _wallets.Add(wallet);

        public static void UpdateWallet(Wallet wallet)
        {
            var index = _wallets.FindIndex(w => w.Id == wallet.Id);
            if (index != -1) _wallets[index] = wallet;
        }

        public static void DeleteWallet(int id)
        {
            _wallets.RemoveAll(w => w.Id == id);
            _transactions.RemoveAll(t => t.WalletId == id);
        }

        public static void AddTransaction(Transaction transaction) => _transactions.Add(transaction);

        public static void DeleteTransaction(int id) => _transactions.RemoveAll(t => t.Id == id);
    }
}