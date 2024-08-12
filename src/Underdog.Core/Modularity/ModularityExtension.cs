using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Underdog.Core.Modularity;

namespace Underdog.Core.Extensions
{
    public static class ModularityExtension
    {

        public static void AddModularity(HostBuilderContext context, IServiceCollection services) 
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

        //public static IServiceCollection AddModularity(this IHostBuilder builder)
        //{
        //    builder.Services.Configure<ModuleOptions>(builder.Configuration.GetSection(nameof(ModuleOptions)));
        //    var moduleOptions = builder.Configuration.GetSection(nameof(ModuleOptions)).Get<ModuleOptions>();
        //    if (moduleOptions != null && moduleOptions.Modules != null)
        //    {
        //        foreach (var option in moduleOptions.Modules)
        //        {
        //            Type? moduleType = Type.GetType(option.Type);
        //            if (moduleType == null) continue;
        //            builder.Services.AddSingleton(moduleType);//注册各模块

        //            IModule? module = builder.Services
        //                .BuildServiceProvider()
        //                .GetService(moduleType) as IModule;
        //            module?.ConfigService(builder.Services);
        //        }
        //    }
        //    return builder.Services;
        //}
        public static IHost UseModularity(this IHost host)
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

        public static IHost UseModularity(this IHost host,IConfiguration configuration)
        {
            var moduleOptions = configuration?.GetSection(nameof(ModuleOptions))
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
    }
}
