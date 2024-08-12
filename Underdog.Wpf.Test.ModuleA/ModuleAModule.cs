using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using Underdog.Core.Modularity;
using Underdog.Core.Navigation.Regions;
using Underdog.Wpf.Extensions;
using Underdog.Wpf.Tests.ModuleA.Views;

namespace Underdog.Wpf.Tests.ModuleA
{
    public class ModuleAModule : IModule
    {
        public void Config(IServiceProvider services)
        {
            var regionManager = services.GetService<IRegionManager>();
            regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
        }

        public void ConfigService(IServiceCollection services)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            services.AddRegionViewScanner(currentAssembly);
            services.AddViewsAndViewModels(currentAssembly);
        }
    }
}
