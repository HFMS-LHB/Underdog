namespace Underdog.Core.Navigation.Regions
{
    /// <summary>
    /// When implemented, allows an instance placed in a <see cref="IRegion"/>
    /// that uses a RegionMemberLifetimeBehavior to indicate
    /// it should be removed when it transitions from an activated to deactivated state.
    /// </summary>
    public interface IRegionMemberLifetime
    {
        /// <summary>
        /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
        /// </summary>
        bool KeepAlive { get; }
    }
}
