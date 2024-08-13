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
using Underdog.Wpf.Common;

namespace Underdog.Wpf.Tests
{
    public class HostBuilderHelper
    {
        private readonly string[] _args;
        private readonly bool _isProduction;

        public HostBuilderHelper(string[] args, bool isProduction = true)
        {
            _args = args;
            _isProduction = isProduction;
            SetEnvironment();
        }

        /// <summary>
        /// 初始化环境
        /// </summary>
        public IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder(_args)
                .UseContentRoot(AppContext.BaseDirectory)
                .UseEnvironment(Environment.GetEnvironmentVariable("environment") ?? "Developement")
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigurationCommonService)
                // .ConfigureServices(ModularityExtension.AddModularityForConfig)
                .ConfigureServices(ConfigurationClientService);

            return builder;
        }

        /// <summary>
        /// 初始化环境
        /// </summary>
        private void SetEnvironment()
        {
            if (_isProduction)
            {
                Environment.SetEnvironmentVariable("environment", Environments.Production);
            }
            else
            {
                Environment.SetEnvironmentVariable("environment", Environments.Development);
            }
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
