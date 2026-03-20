using System;
using System.Text;
using System.Linq;
using ExpenseManager.Data;
using ExpenseManager.ViewModels;

namespace ExpenseManager.ConsoleApp
{
    class Program
    {
        private static readonly IExpenseService _service = new ExpenseService();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("       EXPENSE MANAGER v1.1     ");
                Console.WriteLine("================================");

                var wallets = _service.GetAllWallets().ToList();

                Console.WriteLine("\nВАШІ ГАМАНЦІ:");
                if (!wallets.Any())
                {
                    Console.WriteLine("Гаманців не знайдено.");
                }
                else
                {
                    for (int i = 0; i < wallets.Count; i++)
                    {
                        var w = wallets[i];
                        // Оновлюємо транзакції для коректного розрахунку балансу в консолі
                        w.Transactions = new System.Collections.ObjectModel.ObservableCollection<TransactionViewModel>(
                            _service.GetTransactionsByWalletId(w.Id));
                        Console.WriteLine($"{i + 1}. {w}");
                    }
                }

                Console.WriteLine("\n0. Вихід");
                Console.WriteLine("--------------------------------");
                Console.Write("Виберіть номер гаманця для деталей: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        isRunning = false;
                    }
                    else if (choice > 0 && choice <= wallets.Count)
                    {
                        ShowWalletDetails(wallets[choice - 1]);
                    }
                }
            }

            Console.WriteLine("\nДякуємо за використання. До побачення!");
        }

        static void ShowWalletDetails(WalletViewModel wallet)
        {
            var transactions = _service.GetTransactionsByWalletId(wallet.Id).ToList();

            Console.Clear();
            Console.WriteLine("================================================");
            Console.WriteLine($"   ДЕТАЛІ: {wallet.Name.ToUpper()}");
            Console.WriteLine("================================================");
            Console.WriteLine($"Валюта: {wallet.Currency}");

            decimal currentBalance = transactions.Sum(t => t.Amount);
            Console.WriteLine($"Поточний баланс: {currentBalance:N2} {wallet.Currency}");

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("ІСТОРІЯ ТРАНЗАКЦІЙ:");

            if (!transactions.Any())
            {
                Console.WriteLine("Транзакцій не знайдено.");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
            }

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення...");
            Console.ReadKey();
        }
    }
}