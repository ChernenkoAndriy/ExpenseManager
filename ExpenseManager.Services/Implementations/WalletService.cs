using ExpenseManager.Data.Interfaces;
using ExpenseManager.Domain;
using ExpenseManager.Domain.Enums;
using ExpenseManager.Services.DTOs;
using ExpenseManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<WalletListDto>> GetAllWalletsAsync(string? currencyFilter = null, bool sortByName = false)
        {
            var wallets = await _walletRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(currencyFilter))
            {
                wallets = wallets.Where(w => w.Currency.ToString() == currencyFilter);
            }

            if (sortByName)
            {
                wallets = wallets.OrderBy(w => w.Name);
            }

            var result = new List<WalletListDto>();
            foreach (var w in wallets)
            {
                var transactions = await _transactionRepository.GetByWalletIdAsync(w.Id);
                result.Add(new WalletListDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Currency = w.Currency.ToString(),
                    TotalBalance = transactions.Sum(t => t.Amount)
                });
            }
            return result;
        }

        public async Task<WalletDetailsDto?> GetWalletByIdAsync(int id, string? transactionTypeFilter = "All")
        {
            var wallet = await _walletRepository.GetByIdAsync(id);
            if (wallet == null) return null;

            var transactions = await _transactionRepository.GetByWalletIdAsync(id);

            var filteredTransactions = transactionTypeFilter switch
            {
                "Income" => transactions.Where(t => t.Amount > 0),
                "Expense" => transactions.Where(t => t.Amount < 0),
                _ => transactions
            };

            return new WalletDetailsDto
            {
                Id = wallet.Id,
                Name = wallet.Name,
                Currency = wallet.Currency.ToString(),
                TotalBalance = transactions.Sum(t => t.Amount), 
                Transactions = filteredTransactions.OrderByDescending(t => t.DateTime).Select(t => new TransactionListDto
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

        public async Task DeleteWalletAsync(int id)
        {
            await _walletRepository.DeleteAsync(id); 
        }

        public async Task SaveWalletAsync(WalletSaveDto walletDto)
        {
            if (walletDto.Id.HasValue)
            {
                var existingWallet = await _walletRepository.GetByIdAsync(walletDto.Id.Value);
                if (existingWallet != null)
                {
                    var updatedWallet = existingWallet with
                    {
                        Name = walletDto.Name,
                        Currency = Enum.Parse<Currency>(walletDto.Currency)
                    };
                    await _walletRepository.UpdateAsync(updatedWallet);
                }
            }
            else
            {
                var newWallet = new Wallet(0, walletDto.Name, Enum.Parse<Currency>(walletDto.Currency));
                await _walletRepository.AddAsync(newWallet);
            }
        }

        public IEnumerable<string> GetAvailableCurrencies() => Enum.GetNames(typeof(Currency));
    }
}