using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Underdog.Core.Mvvm;
using Underdog.Core.Navigation.Regions;
using Underdog.Wpf.Navigation.Regions;

namespace Underdog.Wpf.Tests.ViewModels
{
    public partial class ViewCViewModel : ViewModelBase, INavigationAware, IRegionMemberLifetime
    {
        [ObservableProperty]
        private string message;

        public ViewCViewModel()
        {
            Message = "Click Me C";

            ClickCommand = new(() =>
            {
                Message = $"C Click{_counter++}";
            });
        }

        public RelayCommand ClickCommand { get; }

        /// <summary>
        /// 必须和IsNavigationTarget同时为True才会生效
        /// </summary>
        public bool KeepAlive => true;

        private int _counter = 1;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
