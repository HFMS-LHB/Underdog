using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underdog.Wpf.Tests.Extensions
{
    public class MainHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        public MainHostedService(IHostApplicationLifetime hostApplicationLifetime)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // 注册一些启动时要做的操作
            _hostApplicationLifetime.ApplicationStarted.Register(() => 
            {
            
            });

            // 注册一些停止时要做的操作
            _hostApplicationLifetime.ApplicationStopped.Register(() =>
            {

            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
