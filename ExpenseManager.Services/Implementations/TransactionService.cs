using ExpenseManager.Data.Interfaces;
using ExpenseManager.Domain;
using ExpenseManager.Domain.Enums;
using ExpenseManager.Services.DTOs;
using ExpenseManager.Services.Interfaces;

namespace ExpenseManager.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionListDto?> GetTransactionByIdAsync(int id)
        {
            var t = await _transactionRepository.GetByIdAsync(id);
            if (t == null) return null;

            return new TransactionListDto
            {
                Id = t.Id,
                WalletId = t.WalletId,
                Amount = t.Amount,
                Category = t.Category.ToString(),
                Description = t.Description,
                FormattedDate = t.DateTime.ToString("dd.MM.yyyy HH:mm"),
                IsExpense = t.Amount < 0
            };
        }

        public async Task UpdateTransactionAsync(int id, decimal amount, string category, string description)
        {
            if (amount == 0) throw new ArgumentException("Сума не може бути нульовою.");

            var existing = await _transactionRepository.GetByIdAsync(id);
            if (existing != null)
            {
                var updated = existing with
                {
                    Amount = amount,
                    Category = Enum.Parse<TransactionCategory>(category),
                    Description = description
                };
                await _transactionRepository.UpdateAsync(updated);
            }
        }

        public async Task AddTransactionAsync(int walletId, decimal amount, string category, string description)
        {
            if (amount == 0) throw new ArgumentException("Сума не може бути нульовою.");

            var transaction = new Transaction(
                0, walletId, amount,
                Enum.Parse<TransactionCategory>(category),
                description, DateTime.Now);

            await _transactionRepository.AddAsync(transaction);
        }

        public async Task DeleteTransactionAsync(int id) => await _transactionRepository.DeleteAsync(id);

        public IEnumerable<string> GetAvailableCategories() => Enum.GetNames(typeof(TransactionCategory));
    }
}