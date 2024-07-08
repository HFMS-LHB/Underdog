using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Underdog.Wpf.Navigation.Regions
{
    public interface IRegionViewScanner
    {
        IReadOnlyDictionary<string, string> ViewDictionary { get; }

        void ConfigureAssemblies<TInterface>(Assembly assembly);

        /// <summary>
        /// Gets all the classes in the assembly
        /// Can be called multiple times
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblies"></param>
        void ConfigureAssemblies<TInterface>(List<Assembly> assemblies);

        /// <summary>
        /// Clear ViewDictionary
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the assembly qualified name of the corresponding view through the view name
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        string GetViewAssemblyQualifiedName(string viewName);

        /// <summary>
        /// Gets the assembly qualified name of the corresponding view through the viewmodel type
        /// </summary>
        /// <typeparam name="TViewModel">view model type</typeparam>
        /// <returns></returns>
        string GetViewAssemblyQualifiedName<TViewModel>();
    }
}
