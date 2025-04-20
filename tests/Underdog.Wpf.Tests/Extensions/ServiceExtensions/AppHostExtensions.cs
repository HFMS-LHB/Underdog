using Underdog.Wpf.Tests.ViewModels;
using Underdog.Wpf.Tests.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Windows.Threading;

using Underdog.Core.Navigation.Regions;
using Underdog.Wpf.Tests.Views.Dialogs;
using Underdog.Core.Mvvm;
using Underdog.Wpf.Ioc;
using System.Reflection;
using System.Windows;
using Underdog.Wpf.Navigation.Regions;
using Underdog.Core.Modularity;
using Underdog.Core.Extensions;

namespace Underdog.Wpf.Tests.Extensions.ServiceExtensions
{
    public static class AppHostExtensions
    {
        /// <summary>
        /// 初始化App.xaml资源
        /// </summary>
        /// <param name="host"></param>
        public static void InitializeComponent(this IHost host) 
        {
            var app = host.Services.GetRequiredService<App>();
            app.InitializeComponent();
        }

        public static void RunApplication<HostWindow>(this IHost host) where HostWindow : Window, new()
        {
            Task.Run(async () =>
            {
                try
                {
                    await host.RunAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during IOC container registration: {ex.Message}");
                    Environment.Exit(1);
                }
            });

            var app = host.Services.GetRequiredService<App>();
            app.MainWindow = host.Services.GetRequiredService<HostWindow>();
            app.Run();
        }

        /// <summary>
        /// 注册区域
        /// </summary>
        /// <param name="host"></param>
        public static void UseMainRegion(this IHost host)
        {
            var regionManager = host.Services.GetService<IRegionManager>();
            regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
            regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewB));
            regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewC));
        }

        /// <summary>
        /// 显式注册视图和视图模型
        /// </summary>
        /// <param name="host"></param>
        public static void UseExplicitMappingVM(this IHost host)
        {
            ViewModelLocationProvider.Register<NotificationDialog2, NotificationDialog2ViewModel>();
        }

        /// <summary>
        /// 手动注册视图和视图模型
        /// </summary>
        /// <param name="services"></param>
        public static void AddViewAndViewModel(this IServiceCollection services)
        {
            //services.AddTransient<ViewA>();
            //services.AddTransient<ViewB>();
            //services.AddTransient<ViewAViewModel>();
            //services.AddTransient<ViewBViewModel>();

            //services.AddTransient<NotificationDialog1>();
            //services.AddTransient<NotificationDialog2>();
            //services.AddTransient<NotificationDialog1ViewModel>();
            //services.AddTransient<NotificationDialog2ViewModel>();
        }

        public static void AddDialogVMMapping(this IServiceCollection services)
        {
            services.RegisterDialogWindow<MessageBoxC>(nameof(MessageBoxC));
            services.RegisterDialog<NotificationDialog1, NotificationDialog1ViewModel>();
            services.RegisterDialog<NotificationDialog2, NotificationDialog2ViewModel>();
        }

        /// <summary>
        /// 注册模块
        /// </summary>
        /// <param name="services"></param>
        public static void AddModules(this IServiceCollection services)
        {
            services.AddModule<ModuleA.ModuleAModule>();
            services.AddModule<ModuleB.ModuleBModule>();
        }
    }
}
