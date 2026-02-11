using System.Collections.Generic;
using ExpenseManager.Models;
using ExpenseManager.Models.Enums;

namespace ExpenseManager.Data
{
    internal static class Storage
    {
        public static List<Wallet> Wallets { get; } = new()
        {
            new Wallet(1, "Main Debit Card", Currency.UAH),
            new Wallet(2, "Cash Wallet", Currency.UAH),
            new Wallet(3, "Travel Fund", Currency.USD)
        };

        public static List<Transaction> Transactions { get; } = new()
        {
            new Transaction(1, 1, 25000.00m, TransactionCategory.Salary, "Monthly Salary"),
            new Transaction(2, 1, -1200.50m, TransactionCategory.Food, "Weekly Groceries"),
            new Transaction(3, 1, -350.00m, TransactionCategory.Transport, "Fuel Refill"),
            new Transaction(4, 1, -800.00m, TransactionCategory.Entertainment, "Cinema & Dinner"),
            new Transaction(5, 1, -1500.00m, TransactionCategory.Health, "Dental Checkup"),
            new Transaction(6, 1, -45.00m, TransactionCategory.Transport, "Public Bus Pass"),
            new Transaction(7, 1, -2100.00m, TransactionCategory.Other, "Electricity Bill"),
            new Transaction(8, 1, -600.00m, TransactionCategory.Food, "Office Lunch"),
            new Transaction(9, 1, -120.00m, TransactionCategory.Entertainment, "Netflix Subscription"),
            new Transaction(10, 1, -300.00m, TransactionCategory.Other, "Mobile Top-up"),
            new Transaction(11, 2, 1000.00m, TransactionCategory.Other, "Cash Gift"),
            new Transaction(12, 2, -150.00m, TransactionCategory.Food, "Coffee & Croissant")
        };
    }
}