using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Underdog.Core.Dialogs;
using Underdog.Core.Extensions;
using Underdog.Core.Mvvm;
using Underdog.Wpf.Dialogs;
using Underdog.Wpf.Ioc;
using Underdog.Wpf.Extensions;
using Underdog.Wpf.Navigation.Regions;
using Underdog.Wpf.Mvvm;
using Underdog.Wpf.Tests.Extensions;
using Underdog.Wpf.Tests.Extensions.ServiceExtensions;
using Underdog.Wpf.Tests.ViewModels;
using Underdog.Wpf.Tests.Views;
using Underdog.Wpf.Tests.Views.Dialogs;
using Underdog.Wpf.Tests.Common;
using Underdog.Wpf.Tests;


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
            Environment.SetEnvironmentVariable("environment", "Development");
            // Environment.SetEnvironmentVariable("environment", "Production");
            #endregion

            // create builder
            var builder = Host.CreateDefaultBuilder(args)
                .UseContentRoot(AppContext.BaseDirectory)
                .UseEnvironment(Environment.GetEnvironmentVariable("environment") ?? "Developement")
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigurationCommonService)
                // .ConfigureServices(ModularityExtension.AddModularityForConfig)
                .ConfigureServices(ConfigurationClientService);



            AppHost = builder.Build();

            // 加载App.xaml资源
            var app = AppHost.Services.GetRequiredService<App>();
            app.InitializeComponent();

            AppHost.UseRegion<MainWindow>();
            AppHost.UseMainRegion();
            AppHost.UseModularity();

            Task.Run(async () =>
            {
                try
                {
                    await AppHost.RunAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during IOC container registration: {ex.Message}");
                    Environment.Exit(1);
                }
            });

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow!.ShowDialog();
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        /// <param name="hostingContext"></param>
        /// <param name="config"></param>
        private static void ConfigureAppConfiguration(HostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            config.Sources.Clear();
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (hostingContext.HostingEnvironment.IsDevelopment())
            {
                config.AddJsonFile($"appsettings.{Environments.Development}.json", optional: true, reloadOnChange: true);
            }

            config.AddEnvironmentVariables();

            // 这里需要手动构建 IConfigurationBuilder 并配置到HostingContext

            var configurationRoot = config.Build();
            hostingContext.Configuration.ConfigureApplication(configurationRoot);
        }

        /// <summary>
        /// 配置通用服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="services"></param>
        private static void ConfigurationCommonService(HostBuilderContext context, IServiceCollection services)
        {
            context.ConfigureApplication(services);
            services.AddSingleton(new AppSettings(context.Configuration));
        }

        /// <summary>
        /// 配置客户端服务
        /// view、viewmodel、region等
        /// </summary>
        /// <param name="context"></param>
        /// <param name="services"></param>
        private static void ConfigurationClientService(HostBuilderContext context, IServiceCollection services)
        {
            // 当前程序集
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            services.AddSingleton<App>();
            services.AddScoped<MainWindow>();
            services.AddScoped<MainWindowViewModel>();
            services.AddRegion();
            services.AddDialog();
            services.AddMvvm();
            services.AddModules();
            services.AddRegionViewScanner(currentAssembly);
            services.AddViewsAndViewModels(currentAssembly);
            services.AddViewAndViewModel();
            services.AddDialogVMMapping();
            services.AddHostedService<MainHostedService>(); // 这个放到最后注册
        }
    }
}
