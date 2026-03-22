using System.Collections.Generic;

namespace ExpenseManager.Services.DTOs
{
    public class WalletDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public decimal TotalBalance { get; set; }
        public List<TransactionListDto> Transactions { get; set; } = new();
    }
}