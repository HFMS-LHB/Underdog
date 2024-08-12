using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Underdog.Core.Modularity;

namespace Underdog.Core.Extensions
{
    /// <summary>
    /// 模块化扩展，使用配置文件，或者指定类型注册，二选一
    /// </summary>
    public static class ModularityExtension
    {
        #region 配置文件
        public static void AddModularityForConfig(HostBuilderContext context, IServiceCollection services)
        {
            services.Configure<ModuleOptions>(context.Configuration.GetSection(nameof(ModuleOptions)));
            var moduleOptions = context.Configuration.GetSection(nameof(ModuleOptions)).Get<ModuleOptions>();
            if (moduleOptions != null && moduleOptions.Modules != null)
            {
                foreach (var option in moduleOptions.Modules)
                {
                    Type? moduleType = Type.GetType(option.Type);
                    if (moduleType == null) continue;
                    services.AddSingleton(moduleType);//注册各模块

                    IModule? module = services
                        .BuildServiceProvider()
                        .GetService(moduleType) as IModule;
                    module?.ConfigService(services);
                }
            }
        }

        public static IHost UseModularityForConfig(this IHost host)
        {
            var moduleOptions = host.Services
                .GetService<IConfiguration>()
                ?.GetSection(nameof(ModuleOptions))
                .Get<ModuleOptions>();
            if (moduleOptions != null && moduleOptions.Modules != null)
            {
                foreach (var option in moduleOptions.Modules)
                {
                    Type? moduleType = Type.GetType(option.Type);
                    if (moduleType == null) continue;
                    IModule? module = host.Services.GetService(moduleType) as IModule;
                    module?.Config(host.Services);
                }
            }
            return host;
        }
        #endregion

        #region 指定类型
        public static void AddModule<TImplementation>(this IServiceCollection services) where TImplementation : class, IModule, new()
        {
            var module = new TImplementation();
            module.ConfigService(services);
            services.AddSingleton<IModule>(module);
        }

        public static IHost UseModularity(this IHost host)
        {
            var services = host.Services.GetServices<IModule>();
            if (services != null)
            {
                foreach (var module in services)
                {
                    module?.Config(host.Services);
                }
            }
            return host;
        }
        #endregion
    }
}
