using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManager.UI.Services;
using ExpenseManager.UI.ViewModels;
using ExpenseManager.UI.Views;
using ExpenseManager.Services.Interfaces;
using ExpenseManager.Services.Implementations;
using ExpenseManager.Data.Interfaces;
using ExpenseManager.Data.Repositories;

namespace ExpenseManager.UI
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            this.DispatcherUnhandledException += (s, e) =>
            {
                MessageBox.Show($"Виникла помилка: {e.Exception.Message}\n\nДеталі: {e.Exception.InnerException?.Message}",
                                "Критична помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            };

            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<WalletsViewModel>();
            services.AddTransient<WalletDetailsViewModel>();
            services.AddTransient<EditWalletViewModel>();
            services.AddTransient<AddTransactionViewModel>();
            services.AddTransient<TransactionDetailsViewModel>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddSingleton<MainWindow>(sp => new MainWindow
            {
                DataContext = sp.GetRequiredService<MainViewModel>()
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var navService = _serviceProvider.GetRequiredService<INavigationService>();
            navService.NavigateTo<WalletsViewModel>();
        }
    }
}