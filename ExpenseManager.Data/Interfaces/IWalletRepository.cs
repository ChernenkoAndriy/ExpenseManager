using ExpenseManager.Domain;

namespace ExpenseManager.Data.Interfaces
{
    public interface IWalletRepository
    {
        IEnumerable<Wallet> GetAll();
        Wallet? GetById(int id);
        void Add(Wallet wallet);
        void Update(Wallet wallet);
        void Delete(int id);
    }
}