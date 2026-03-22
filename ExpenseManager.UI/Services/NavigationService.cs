using System;
using ExpenseManager.UI.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.UI.Services
{
    public class NavigationService : BaseViewModel, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private BaseViewModel? _currentViewModel;

        public BaseViewModel? CurrentViewModel
        {
            get => _currentViewModel;
            private set => SetProperty(ref _currentViewModel, value);
        }

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
            CurrentViewModel = viewModel;
        }

        public void NavigateTo<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            if (viewModel is IParameterReceiver receiver)
            {
                receiver.ReceiveParameter(parameter);
            }

            CurrentViewModel = viewModel;
        }
    }

    public interface IParameterReceiver
    {
        void ReceiveParameter(object parameter);
    }
}