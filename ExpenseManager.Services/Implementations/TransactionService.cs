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

       

        public void AddTransaction(int walletId, decimal amount, string category, string description)
        {
            if (amount == 0) throw new ArgumentException("Сума не може бути нульовою.");

            var categoryEnum = Enum.Parse<TransactionCategory>(category);
            int newId = _transactionRepository.GetNextId();

            var transaction = new Transaction(
                newId,
                walletId,
                amount,
                categoryEnum,
                description,
                DateTime.Now
            );

            _transactionRepository.Add(transaction);
        }

        public void DeleteTransaction(int id)
        {
            _transactionRepository.Delete(id);
        }

        public IEnumerable<string> GetAvailableCategories()
        {
            return Enum.GetNames(typeof(TransactionCategory));
        }
    }
}