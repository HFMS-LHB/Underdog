using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Underdog.Core.Dialogs;
using Underdog.Core.Extensions;
using Underdog.Core.Mvvm;
using Underdog.Wpf.Dialogs;
using Underdog.Wpf.Ioc;
using Underdog.Wpf.Mvvm;
using Underdog.Wpf.Navigation.Regions;
using Underdog.Wpf.Tests.Extensions.ServiceExtensions;
using Underdog.Wpf.Tests.ViewModels;
using Underdog.Wpf.Tests.Views;
using Underdog.Wpf.Tests.Views.Dialogs;


namespace Underdog.Wpf.Tests
{
    public class Program
    {
        public static IHost? AppHost { get; private set; }
        public static IServiceProvider ServiceProvider => AppHost!.Services;

        [System.STAThreadAttribute()]
        public static void Main(string[] args)
        {
            #region set environment
#if DEBUG
            Environment.SetEnvironmentVariable("environment", "Development");
#else
            Environment.SetEnvironmentVariable("environment", "Production");
#endif
            #endregion

            // create builder
            var builder = Host.CreateDefaultBuilder(args)
                .UseContentRoot(AppContext.BaseDirectory)
                .UseEnvironment(Environment.GetEnvironmentVariable("environment") ?? "Developement")
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ModularityExtension.AddModularity)
                .ConfigureServices(ConfigurationClientService)
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    //builder.RegisterDialog<NotificationDialog1, NotificationDialog1ViewModel>();

                    //builder.RegisterDialog<NotificationDialog2, NotificationDialog2ViewModel>();
                });


            AppHost = builder.Build();
            // 加载App.xaml资源
            var app = AppHost.Services.GetRequiredService<App>();
            // app.InitializeComponent();
            AppHost.UseRegion<MainWindow>();
            AppHost.UseMainRegion();
            AppHost.UseModularity();

            AppHost.Run();
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        /// <param name="hostingContext"></param>
        /// <param name="config"></param>
        private static void ConfigureAppConfiguration(HostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            config.Sources.Clear();
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            if (hostingContext.HostingEnvironment.IsDevelopment())
            {
                config.AddJsonFile($"appsettings.{Environments.Development}.json", optional: true, reloadOnChange: false);
            }

            config.AddEnvironmentVariables();
        }



        /// <summary>
        /// 配置客户端服务
        /// view、viewmodel、region等
        /// </summary>
        /// <param name="context"></param>
        /// <param name="services"></param>
        private static void ConfigurationClientService(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<App>();
            services.AddScoped<MainWindow>();
            services.AddScoped<MainWindowViewModel>();
            services.AddHostedService<WPFHostedService<App, MainWindow>>();
            services.AddViewAndViewModel();
            services.RegisterDialog<NotificationDialog1, NotificationDialog1ViewModel>();
            services.RegisterDialog<NotificationDialog2, NotificationDialog2ViewModel>();
            services.AddRegion();
            services.AddDialog();
            services.AddMvvm();
        }
    }
}
