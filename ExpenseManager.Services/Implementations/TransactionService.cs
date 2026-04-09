using ExpenseManager.Data.Interfaces;
using ExpenseManager.Domain;
using ExpenseManager.Domain.Enums;
using ExpenseManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task AddTransactionAsync(int walletId, decimal amount, string category, string description)
        {
            if (amount == 0) throw new ArgumentException("Сума не може бути нульовою.");

            var categoryEnum = Enum.Parse<TransactionCategory>(category);
            var transaction = new Transaction(
                0,
                walletId,
                amount,
                categoryEnum,
                description,
                DateTime.Now
            );

            await _transactionRepository.AddAsync(transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            await _transactionRepository.DeleteAsync(id);
        }

        public IEnumerable<string> GetAvailableCategories()
        {
            return Enum.GetNames(typeof(TransactionCategory));
        }
    }
}