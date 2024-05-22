using Microsoft.Extensions.DependencyInjection;

namespace Underdog.Core.Modularity
{
    public interface IModule
    {
        void ConfigService(IServiceCollection services);
        void Config(IServiceProvider services);
    }
}
