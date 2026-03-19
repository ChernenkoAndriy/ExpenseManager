using ExpenseManager.Models.Enums;

namespace ExpenseManager.Models
{
    public class Wallet
    {
        public Wallet(int id, string name, Currency currency)
        {
            Id = id;
            Name = name;
            Currency = currency;
        }
        public int Id { get; init; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
    }
}