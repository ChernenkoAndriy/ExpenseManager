namespace ExpenseManager.Services.DTOs
{
    public class TransactionDetailsDto
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FormattedDate { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
    }
}