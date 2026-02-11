using System;
using System.Text;
using ExpenseManager.Data;
using ExpenseManager.ViewModels;

namespace ExpenseManager.ConsoleApp
{
    class Program
    {
        private static readonly ExpenseService _service = new ExpenseService();

        static void Main(string[] args)
        {
            // Set encoding to UTF8 for proper currency and symbol rendering
            Console.OutputEncoding = Encoding.UTF8;
            
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("       EXPENSE MANAGER v1.0     ");
                Console.WriteLine("================================");
                
                var wallets = _service.GetWallets();

                Console.WriteLine("\nYOUR WALLETS:");
                for (int i = 0; i < wallets.Count; i++)
                {
                    // wallet.ToString() is called automatically here
                    Console.WriteLine($"{i + 1}. {wallets[i]}");
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
            // Service populates the ViewModel with filtered transactions
            _service.LoadTransactionsForWallet(wallet);

            Console.Clear();
            Console.WriteLine("================================================");
            Console.WriteLine($"   DETAILS: {wallet.Name.ToUpper()}");
            Console.WriteLine("================================================");
            Console.WriteLine($"Currency: {wallet.Currency}");
            Console.WriteLine($"Current Balance: {wallet.TotalBalance} {wallet.Currency}");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("TRANSACTION HISTORY:");

            if (wallet.Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found for this wallet.");
            }
            else
            {
                foreach (var transaction in wallet.Transactions)
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
