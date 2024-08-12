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
    public partial class MainWindowViewModel:ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IRegionManager _regionManager;
        private readonly IRegionViewScanner _regionViewScanner;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        public MainWindowViewModel(IServiceProvider serviceProvider, IConfiguration configuration, IRegionViewScanner regionViewScanner, IDialogService dialogService,IRegionManager regionManager)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _dialogService = dialogService;
            _regionManager = regionManager;
            _regionViewScanner = regionViewScanner;

            ShowDialog1Command = new RelayCommand(() =>
            {
                var message = "This is a message that should be shown in the dialog.";
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
            });

            ShowDialog2Command = new RelayCommand(() =>
            {
                var message = "This is a message that should be shown in the dialog.";
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
            });

            ShowRegion1Command = new(() =>
            {
                _regionManager.RequestNavigate(RegionKey.Root, _regionViewScanner.GetViewAssemblyQualifiedName<ViewAViewModel>());
            });

            ShowRegion2Command = new(() =>
            {
                _regionManager.RequestNavigate(RegionKey.Root, _regionViewScanner.GetViewAssemblyQualifiedName<ViewBViewModel>());
            });
        }

        public RelayCommand ShowDialog1Command { get; }
        public RelayCommand ShowDialog2Command { get; }

        public RelayCommand ShowRegion1Command { get; }
        public RelayCommand ShowRegion2Command { get; }

        [ObservableProperty]
        private string title = "Welcome to Underdog WPF Tests";

        [RelayCommand]
        private void ShowRegion3() 
        {
            _regionManager.RequestNavigate(RegionKey.Root, _regionViewScanner.GetViewAssemblyQualifiedName<Underdog.Wpf.Tests.ModuleA.ViewModels.ViewAViewModel>());
        }

        [RelayCommand]
        private void ShowRegion4()
        {
            _regionManager.RequestNavigate(RegionKey.Root, _regionViewScanner.GetViewAssemblyQualifiedName<Underdog.Wpf.Tests.ModuleB.ViewModels.ViewAViewModel>());
        }
    }
}
