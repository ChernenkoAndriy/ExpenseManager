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
                Console.WriteLine("       EXPENSE MANAGER v1.0     ");
                Console.WriteLine("================================");

                var wallets = _service.GetAllWallets().ToList();

                Console.WriteLine("\nYOUR WALLETS:");
                if (!wallets.Any())
                {
                    Console.WriteLine("No wallets found.");
                }
                else
                {
                    for (int i = 0; i < wallets.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {wallets[i]}");
                    }
                }

                Console.WriteLine("\n0. Exit");
                Console.WriteLine("--------------------------------");
                Console.Write("Select a wallet number for details: ");

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

            Console.WriteLine("\nThank you for using Expense Manager. Goodbye!");
        }

        static void ShowWalletDetails(WalletViewModel wallet)
        {
            var transactions = _service.GetTransactionsByWalletId(wallet.Id).ToList();

            Console.Clear();
            Console.WriteLine("================================================");
            Console.WriteLine($"   DETAILS: {wallet.Name.ToUpper()}");
            Console.WriteLine("================================================");
            Console.WriteLine($"Currency: {wallet.Currency}");

            decimal currentBalance = transactions.Sum(t => t.Amount);
            Console.WriteLine($"Current Balance: {currentBalance} {wallet.Currency}");

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("TRANSACTION HISTORY:");

            if (!transactions.Any())
            {
                Console.WriteLine("No transactions found for this wallet.");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
            }

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("\nPress any key to return to the list...");
            Console.ReadKey();
        }
    }
}