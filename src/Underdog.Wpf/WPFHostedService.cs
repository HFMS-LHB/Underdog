﻿using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Underdog.Wpf
{
    [Obsolete("没什么意义，而且如果使用IHostedService管理QuartNet，会和主程序产生线程冲突")]
    public class WPFHostedService<TApplication, TWindow> : IHostedService
        where TApplication : Application
        where TWindow : Window
    {
        private TApplication app;
        private TWindow window;
        private IHostApplicationLifetime applicationLifetime;
        private Task? _applicationTask;
        public WPFHostedService(TApplication app, TWindow window, IHostApplicationLifetime applicationLifetime)
        {
            this.app = app;
            this.window = window;
            this.applicationLifetime = applicationLifetime;
        }
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            app.Exit += App_Exit;//处理退出的时候，停止主机,.NET6+需要

            app.Run(window);

            return Task.CompletedTask;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            applicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
