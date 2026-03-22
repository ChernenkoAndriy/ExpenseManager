using ExpenseManager.Data.Interfaces;
using ExpenseManager.Domain;

namespace ExpenseManager.Data.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        public IEnumerable<Wallet> GetAll()
        {
            return Storage.Wallets;
        }

        public Wallet? GetById(int id)
        {
            return Storage.Wallets.FirstOrDefault(w => w.Id == id);
        }

        public void Add(Wallet wallet)
        {
            Storage.AddWallet(wallet);
        }

        public void Update(Wallet wallet)
        {
            Storage.UpdateWallet(wallet);
        }

        public void Delete(int id)
        {
            Storage.DeleteWallet(id);
        }
    }
}