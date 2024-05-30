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

namespace Underdog.Wpf.Tests.Extensions.ServiceExtensions
{
    public static class RegistViewExtensions
    {
        /// <summary>
        /// 注册区域
        /// </summary>
        /// <param name="host"></param>
        public static void UseMainRegion(this IHost host)
        {
            var regionManager = host.Services.GetService<IRegionManager>();
            regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
            regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewB));
        }

        /// <summary>
        /// 显式注册视图和视图模型
        /// </summary>
        /// <param name="host"></param>
        public static void UseExplicitMappingVM(this IHost host)
        {
            ViewModelLocationProvider.Register<NotificationDialog2, NotificationDialog2ViewModel>();
        }

        public static void AddRegionViewScanner(this IServiceCollection services)
        {
            services.AddSingleton<IRegionViewScanner, RegionViewScanner>((provider =>
            {
                var viewScanner = new RegionViewScanner();

                // 注册当前程序集，按需添加要注册的程序集
                //var assemblies = new List<Assembly>()
                //{
                //    Assembly.GetExecutingAssembly(),
                //};

                // 直接获取当前域的所有程序集，按需筛选
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                                        .Where(x => !string.IsNullOrEmpty(x.FullName) && x.FullName.StartsWith("Underdog"))
                                                        .ToList();
                viewScanner.ConfigureAssemblies<FrameworkElement>(assemblies);
                return viewScanner;
            }));
        }

        /// <summary>
        /// 注册视图和视图模型
        /// </summary>
        /// <param name="services"></param>
        public static void AddViewAndViewModel(this IServiceCollection services)
        {
            services.AddTransient<ViewA>();
            services.AddTransient<ViewB>();
            services.AddTransient<ViewAViewModel>();
            services.AddTransient<ViewBViewModel>();

            services.RegisterDialogWindow<MessageBoxC>(nameof(MessageBoxC));

            services.AddTransient<NotificationDialog1>();
            services.AddTransient<NotificationDialog2>();
            services.AddTransient<NotificationDialog1ViewModel>();
            services.AddTransient<NotificationDialog2ViewModel>();
            services.RegisterDialog<NotificationDialog1, NotificationDialog1ViewModel>();
            services.RegisterDialog<NotificationDialog2, NotificationDialog2ViewModel>();
        }
    }
}
