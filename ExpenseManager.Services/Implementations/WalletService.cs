using ExpenseManager.Data.Interfaces;
using ExpenseManager.Domain;
using ExpenseManager.Domain.Enums;
using ExpenseManager.Services.DTOs;
using ExpenseManager.Services.Interfaces;

namespace ExpenseManager.Services.Implementations
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WalletService(IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public IEnumerable<WalletListDto> GetAllWallets()
        {
            var wallets = _walletRepository.GetAll();
            return wallets.Select(w =>
            {
                var transactions = _transactionRepository.GetByWalletId(w.Id);
                return new WalletListDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Currency = w.Currency.ToString(),
                    TotalBalance = transactions.Sum(t => t.Amount)
                };
            });
        }

        public WalletDetailsDto? GetWalletById(int id)
        {
            var wallet = _walletRepository.GetById(id);
            if (wallet == null) return null;

            var transactions = _transactionRepository.GetByWalletId(id);
            var totalBalance = transactions.Sum(t => t.Amount);

            return new WalletDetailsDto
            {
                Id = wallet.Id,
                Name = wallet.Name,
                Currency = wallet.Currency.ToString(),
                TotalBalance = totalBalance,
                Transactions = transactions.OrderByDescending(t => t.DateTime).Select(t => new TransactionListDto
                {
                    Id = t.Id,
                    WalletId = t.WalletId,
                    Amount = t.Amount,
                    Category = t.Category.ToString(),
                    FormattedDate = t.DateTime.ToString("dd.MM.yyyy HH:mm"),
                    IsExpense = t.Amount < 0,
                    Description = t.Description
                }).ToList()
            };
        }

        public void SaveWallet(WalletSaveDto walletDto)
        {
            if (walletDto.Id.HasValue)
            {
                var existingWallet = _walletRepository.GetById(walletDto.Id.Value);
                if (existingWallet != null)
                {
                    var updatedWallet = existingWallet with
                    {
                        Name = walletDto.Name,
                        Currency = Enum.Parse<Currency>(walletDto.Currency)
                    };
                    _walletRepository.Update(updatedWallet);
                }
            }
            else
            {
                var allWallets = _walletRepository.GetAll();
                int newId = allWallets.Any() ? allWallets.Max(w => w.Id) + 1 : 1;

                var newWallet = new Wallet(
                    newId,
                    walletDto.Name,
                    Enum.Parse<Currency>(walletDto.Currency)
                );
                _walletRepository.Add(newWallet);
            }
        }

        public IEnumerable<string> GetAvailableCurrencies()
        {
            return Enum.GetNames(typeof(Currency));
        }
    }
}