using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Underdog.Core.Dialogs;
using Underdog.Core.Mvvm;

namespace Underdog.Wpf.Ioc
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extensions.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Registers an object to be used as a dialog in the IDialogService.
        /// </summary>
        /// <typeparam name="TView">The Type of object to register as the dialog</typeparam>
        /// <param name="services"></param>
        /// <param name="name">The unique name to register with the dialog.</param>
        public static void RegisterDialog<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView>(this IServiceCollection services, string name = null)
        {
            services.RegisterForNavigation<TView>(name);
        }

        /// <summary>
        /// Registers an object to be used as a dialog in the IDialogService.
        /// </summary>
        /// <typeparam name="TView">The Type of object to register as the dialog</typeparam>
        /// <typeparam name="TViewModel">The ViewModel to use as the DataContext for the dialog</typeparam>
        /// <param name="services"></param>
        /// <param name="name">The unique name to register with the dialog.</param>
        public static void RegisterDialog<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TViewModel>(this IServiceCollection services, string name = null) where TViewModel : IDialogAware
        {
            services.RegisterForNavigation<TView, TViewModel>(name);
        }

        /// <summary>
        /// Registers an object that implements IDialogWindow to be used to host all dialogs in the IDialogService.
        /// </summary>
        /// <typeparam name="TWindow">The Type of the Window class that will be used to host dialogs in the IDialogService</typeparam>
        /// <param name="services"></param>
        public static void RegisterDialogWindow<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TWindow>(this IServiceCollection services) where TWindow : class, Dialogs.IDialogWindow
        {
            services.AddTransient<Dialogs.IDialogWindow, TWindow>();
        }

        /// <summary>
        /// Registers an object that implements IDialogWindow to be used to host all dialogs in the IDialogService.
        /// </summary>
        /// <typeparam name="TWindow">The Type of the Window class that will be used to host dialogs in the IDialogService</typeparam>
        /// <param name="services"></param>
        /// <param name="name">The name of the dialog window</param>
        public static void RegisterDialogWindow<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TWindow>(this IServiceCollection services, string name) where TWindow : class, Dialogs.IDialogWindow
        {
            services.AddKeyedTransient<Dialogs.IDialogWindow, TWindow>(name);
        }

        /// <summary>
        /// Registers an object for navigation
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type">The type of object to register</param>
        /// <param name="name">The unique name to register with the object.</param>
        public static void RegisterForNavigation(this IServiceCollection services, Type type, string name)
        {
            services.AddKeyedTransient(typeof(object), name,type);
        }

        /// <summary>
        /// Registers an object for navigation.
        /// </summary>
        /// <typeparam name="T">The Type of the object to register as the view</typeparam>
        /// <param name="services"></param>
        /// <param name="name">The unique name to register with the object.</param>
        public static void RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(this IServiceCollection services, string name = null)
        {
            Type type = typeof(T);
            string viewName = string.IsNullOrWhiteSpace(name) ? type.Name : name;
            services.RegisterForNavigation(type, viewName);
        }

        /// <summary>
        /// Registers an object for navigation with the ViewModel type to be used as the DataContext.
        /// </summary>
        /// <typeparam name="TView">The Type of object to register as the view</typeparam>
        /// <typeparam name="TViewModel">The ViewModel to use as the DataContext for the view</typeparam>
        /// <param name="services"></param>
        /// <param name="name">The unique name to register with the view</param>
        public static void RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TViewModel>(this IServiceCollection services, string name = null)
        {
            services.RegisterForNavigationWithViewModel<TViewModel>(typeof(TView), name);
        }

        private static void RegisterForNavigationWithViewModel<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TViewModel>(this IServiceCollection services, Type viewType, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = viewType.Name;

            ViewModelLocationProvider.Register(viewType.ToString(), typeof(TViewModel));
            services.RegisterForNavigation(viewType, name);
        }
    }
}
