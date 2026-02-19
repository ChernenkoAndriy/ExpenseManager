using System.Windows;
using ExpenseManager.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.UI
{
    public partial class MainWindow : Window
    {
        private readonly IExpenseService _expenseService;

        public MainWindow(IExpenseService expenseService)
        {
            InitializeComponent();
            _expenseService = expenseService;
            MainFrame.Navigate(new WalletsPage(_expenseService));
        }
    }
}