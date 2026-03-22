namespace ExpenseManager.Services.DTOs
{
    public class TransactionListDto
    {
        public int Id { get; set; }
        public int WalletId { get; set; } 
        public decimal Amount { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FormattedDate { get; set; } = string.Empty;
        public bool IsExpense { get; set; }
    }
}