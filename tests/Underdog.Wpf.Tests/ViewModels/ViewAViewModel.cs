using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Underdog.Core.Mvvm;
using Underdog.Core.Navigation.Regions;
using Underdog.Wpf.Navigation.Regions;

namespace Underdog.Wpf.Tests.ViewModels
{
    public partial class ViewAViewModel : ViewModelBase, INavigationAware, IRegionMemberLifetime
    {
        [ObservableProperty]
        private string message;

        public ViewAViewModel()
        {
            Message = "Click Me A";

            ClickCommand = new(() =>
            {
                Message = $"A Click{_counter++}";
            });
        }

        public RelayCommand ClickCommand { get; }

        public bool KeepAlive => false;

        private int _counter = 1;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
