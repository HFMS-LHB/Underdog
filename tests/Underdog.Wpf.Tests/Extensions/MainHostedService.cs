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
        public MainHostedService(IHostApplicationLifetime hostApplicationLifetime)
        {
            // 注册一些启动时要做的操作
            hostApplicationLifetime.ApplicationStarted.Register(() =>
            {

            });

            // 注册一些停止时要做的操作
            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {

            });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
