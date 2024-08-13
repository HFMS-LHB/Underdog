using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Underdog.Core.Dialogs;
using Underdog.Core.Extensions;
using Underdog.Core.Mvvm;
using Underdog.Wpf.Dialogs;
using Underdog.Wpf.Ioc;
using Underdog.Wpf.Extensions;
using Underdog.Wpf.Navigation.Regions;
using Underdog.Wpf.Mvvm;
using Underdog.Wpf.Tests.Extensions;
using Underdog.Wpf.Tests.Extensions.ServiceExtensions;
using Underdog.Wpf.Tests.ViewModels;
using Underdog.Wpf.Tests.Views;
using Underdog.Wpf.Tests.Views.Dialogs;
using Underdog.Wpf.Tests.Common;
using Underdog.Wpf.Tests;
using Underdog.Wpf.Common;


namespace Underdog.Wpf.Tests
{
    public class Program
    {
        public static IHost? AppHost { get; private set; }

        [System.STAThreadAttribute()]
        public static void Main(string[] args)
        {
            var helper = new HostBuilderHelper(args, false);
            var builder = helper.CreateHostBuilder();

            AppHost = builder.Build();
            AppHost.InitializeComponent();
            AppHost.UseRegion<MainWindow>();
            AppHost.UseMainRegion();
            AppHost.UseModularity();
            AppHost.RunApplication();
        }
    }
}
