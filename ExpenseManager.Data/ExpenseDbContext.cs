using Microsoft.EntityFrameworkCore;
using ExpenseManager.Domain;
using ExpenseManager.Domain.Enums;
using System;

namespace ExpenseManager.Data
{
    public class ExpenseDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;

        public ExpenseDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=expenses.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasKey(w => w.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);

            modelBuilder.Entity<Transaction>()
                .HasOne<Wallet>()
                .WithMany()
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Wallet>().HasData(
                new Wallet(1, "Основна картка", Currency.UAH),
                new Wallet(2, "Готівка", Currency.UAH),
                new Wallet(3, "Travel Fund", Currency.USD)
            );

            var now = new DateTime(2023, 10, 1, 12, 0, 0);

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction(1, 1, 15000.00m, TransactionCategory.Salary, "Зарплата", now.AddDays(-5)),
                new Transaction(2, 1, -1200.50m, TransactionCategory.Food, "Продукти", now.AddDays(-3)),
                new Transaction(3, 2, -350.00m, TransactionCategory.Transport, "Пальне", now.AddDays(-1)),
                new Transaction(4, 2, 1000.00m, TransactionCategory.Other, "Подарунок", now.AddDays(-10))
            );
        }
    }
}