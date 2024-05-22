#if WPF

// NOTE: This is for Legacy support for WPF apps only
using Underdog.Core.Navigation.Regions;

namespace Underdog.Wpf.Navigation.Regions
{
    /// <summary>
    /// Provides a way for objects involved in navigation to be notified of navigation activities.
    /// </summary>
    /// <remarks>
    /// Provides compatibility for Legacy Underdog.Wpf.Wpf apps.
    /// </remarks>
    public interface INavigationAware : IRegionAware
    {
    }
}
#endif
