using Underdog.Wpf.Tests.ViewModels;
using Underdog.Wpf.Tests.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Windows.Threading;

using Underdog.Core.Navigation.Regions;
using Underdog.Wpf.Tests.Views.Dialogs;
using Underdog.Core.Mvvm;

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
        /// 注册视图和视图模型
        /// </summary>
        /// <param name="services"></param>
        public static void AddViewAndViewModel(this IServiceCollection services)
        {
            services.AddTransient<NotificationDialog1>();
            services.AddTransient<NotificationDialog2>();

            services.AddTransient<NotificationDialog1ViewModel>();
            services.AddTransient<NotificationDialog2ViewModel>();

            // ViewModelLocationProvider.Register<NotificationDialog2, NotificationDialog2ViewModel>();
        }
    }
}
