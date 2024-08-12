using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Underdog.Wpf.Navigation.Regions
{
    public class RegionViewScannerHostedService : IHostedService
    {
        private readonly IRegionViewScanner _regionViewScanner;
        private readonly Assembly? _assembly;

        public RegionViewScannerHostedService(IRegionViewScanner regionViewScanner, Assembly? assembly)
        {
            _regionViewScanner = regionViewScanner;
            _assembly = assembly;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_assembly != null)
            {
                _regionViewScanner.ConfigureAssemblies<FrameworkElement>(_assembly);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) 
        {
            _regionViewScanner?.Clear();
            return Task.CompletedTask;
        }
    }
}
