using ExpenseManager.UI.ViewModels.Base;
using ExpenseManager.UI.Services;

namespace ExpenseManager.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public INavigationService Navigation => _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}