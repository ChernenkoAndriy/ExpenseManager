using ExpenseManager.Models;

namespace ExpenseManager.ViewModels
{
    public class TransactionViewModel
    {
        private readonly Transaction _transaction;

        public TransactionViewModel(Transaction transaction)
        {
            _transaction = transaction;
        }
        public decimal Amount => _transaction.Amount;
        public string Category => _transaction.Category.ToString();
        public string Description => _transaction.Description;
        public string Date => _transaction.DateTime.ToString("dd.MM.yyyy HH:mm");

        public bool IsExpense => _transaction.Amount < 0;

        public override string ToString()
        {
            string type = IsExpense ? "[Expense]" : "[Income]";
            return $"{Date} | {type} {Category}: {Amount} ({Description})";
        }
    }
}