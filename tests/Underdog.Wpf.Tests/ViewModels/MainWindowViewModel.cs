using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Underdog.Core.Dialogs;
using Underdog.Core.Mvvm;
using Underdog.Core.Navigation.Regions;
using Underdog.Wpf.Navigation.Regions;
using Underdog.Wpf.Tests.Common;
using Underdog.Wpf.Tests.Views;

namespace Underdog.Wpf.Tests.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IRegionManager _regionManager;
        private readonly IRegionViewScanner _regionViewScanner;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        public MainWindowViewModel(IServiceProvider serviceProvider, IConfiguration configuration, IRegionViewScanner regionViewScanner, IDialogService dialogService, IRegionManager regionManager)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _dialogService = dialogService;
            _regionManager = regionManager;
            _regionViewScanner = regionViewScanner;
        }

        [ObservableProperty]
        private string title = "Welcome to Underdog WPF Tests";

        [RelayCommand]
        private void ShowDialog1()
        {
            var message = "这是一个自定义window窗体的弹窗，通过参数传递窗体名称";
            //using the dialog service as-is
            _dialogService.ShowDialog("NotificationDialog1", new DialogParameters($"message={message}"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Title = "Result is None";
                else if (r.Result == ButtonResult.OK)
                    Title = "Result is OK";
                else if (r.Result == ButtonResult.Cancel)
                    Title = "Result is Cancel";
                else
                    Title = "I Don't know what you did!?";
            }, "MessageBoxC");
        }

        [RelayCommand]
        private void ShowDialog2() 
        {
            var message = "这是一个自定义window窗体的弹窗，通过DialogParameters传递窗体名称";
            //using the dialog service as-is
            _dialogService.ShowDialog("NotificationDialog2", new DialogParameters($"windowName=MessageBoxC&message={message}"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Title = "Result is None";
                else if (r.Result == ButtonResult.OK)
                    Title = "Result is OK";
                else if (r.Result == ButtonResult.Cancel)
                    Title = "Result is Cancel";
                else
                    Title = "I Don't know what you did!?";
            });
        }

        [RelayCommand]
        private void ShowRegion1()
        {
            _regionManager.RequestNavigate(RegionKey.Root1, _regionViewScanner.GetViewAssemblyQualifiedName<ViewAViewModel>());

        }

        [RelayCommand]
        private void ShowRegion2()
        {
            _regionManager.RequestNavigate(RegionKey.Root2, _regionViewScanner.GetViewAssemblyQualifiedName<ViewBViewModel>());
        }

        [RelayCommand]
        private void ShowRegion3() 
        {
            _regionManager.RequestNavigate(RegionKey.Root3, _regionViewScanner.GetViewAssemblyQualifiedName<Underdog.Wpf.Tests.ModuleA.ViewModels.ViewAViewModel>());
        }

        [RelayCommand]
        private void ShowRegion4()
        {
            _regionManager.RequestNavigate(RegionKey.Root4, _regionViewScanner.GetViewAssemblyQualifiedName<Underdog.Wpf.Tests.ModuleB.ViewModels.ViewAViewModel>());
        }

        [RelayCommand]
        private void ShowRegion5()
        {
            //_regionManager.RequestNavigate(RegionKey.Root3, _regionViewScanner.GetViewAssemblyQualifiedName<ViewAViewModel>());
            //_regionManager.RequestNavigate(RegionKey.Root4, _regionViewScanner.GetViewAssemblyQualifiedName<ViewBViewModel>());

            _regionManager.Regions[RegionKey.Root3].NavigationService.RequestNavigate(_regionViewScanner.GetViewAssemblyQualifiedName<ViewAViewModel>());
            _regionManager.Regions[RegionKey.Root4].NavigationService.RequestNavigate(_regionViewScanner.GetViewAssemblyQualifiedName<ViewBViewModel>());
        }

        [RelayCommand]
        private void ShowRegion6()
        {
            //_regionManager.RequestNavigate(RegionKey.Root3, _regionViewScanner.GetViewAssemblyQualifiedName<Underdog.Wpf.Tests.ModuleA.ViewModels.ViewAViewModel>());
            //_regionManager.RequestNavigate(RegionKey.Root4, _regionViewScanner.GetViewAssemblyQualifiedName<Underdog.Wpf.Tests.ModuleB.ViewModels.ViewAViewModel>());

            _regionManager.Regions[RegionKey.Root3].NavigationService.RequestNavigate(_regionViewScanner.GetViewAssemblyQualifiedName<Underdog.Wpf.Tests.ModuleA.ViewModels.ViewAViewModel>());
            _regionManager.Regions[RegionKey.Root4].NavigationService.RequestNavigate(_regionViewScanner.GetViewAssemblyQualifiedName<Underdog.Wpf.Tests.ModuleB.ViewModels.ViewAViewModel>());
        }
    }
}
