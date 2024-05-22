using Microsoft.Extensions.DependencyInjection;
using Underdog.Core.Ioc;
using Underdog.Core.Mvvm;

namespace Underdog.Wpf.Mvvm
{
    public static class MvvmExtension
    {
        public static IServiceCollection AddMvvm(this IServiceCollection services)
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory((view, type) =>
            {
                return ContainerLocator.Container.GetService(type);
            });
            return services;
        }
    }
}
