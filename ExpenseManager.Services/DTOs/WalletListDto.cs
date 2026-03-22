namespace ExpenseManager.Services.DTOs
{
    public class WalletListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public decimal TotalBalance { get; set; }
    }
}