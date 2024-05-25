using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Underdog.Core.Dialogs;

namespace Underdog.Wpf.Dialogs
{
    public static class DialogExtension
    {
        public static IServiceCollection AddDialog(this IServiceCollection services)
        {
            services.BuildServiceProvider();
            services.AddScoped<Dialog>();
            services.AddTransient<IDialogService, DialogService>();
            services.AddTransient<IDialogWindow, DialogWindow>();
            
            return services;
        }

    }
}
