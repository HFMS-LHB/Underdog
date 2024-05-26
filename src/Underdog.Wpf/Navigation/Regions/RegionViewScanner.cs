using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Underdog.Wpf.Navigation.Regions
{
    public class RegionViewScanner : IRegionViewScanner
    {
        private readonly Dictionary<string, string> _viewDictionary = [];

        public IReadOnlyDictionary<string, string> ViewDictionary => _viewDictionary;

        public void ConfigureAssemblies(List<Assembly> assemblies)
        {
            _viewDictionary.Clear();
            ScanAssembliesForInterface<FrameworkElement>(assemblies);
        }

        public void ConfigureAssemblies<TInterface>(List<Assembly> assemblies)
        {
            _viewDictionary.Clear();
            ScanAssembliesForInterface<TInterface>(assemblies);
        }

        private void ScanAssembliesForInterface<TInterface>(List<Assembly> assemblies)
        {
            var interfaceType = typeof(TInterface);

            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (interfaceType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                        {
                            _viewDictionary[type.Name] = type?.AssemblyQualifiedName?.Replace(" ", "") ?? "";
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    Console.WriteLine($"Could not load some types from assembly {assembly.FullName}");
                }
            }
        }

        /// <summary>
        /// Gets the assembly qualified name of the corresponding view through the view name
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public string GetViewAssemblyQualifiedName(string viewName)
        {
            return ViewDictionary.ContainsKey(viewName) ? ViewDictionary[viewName] : string.Empty;
        }

        /// <summary>
        /// Gets the assembly qualified name of the corresponding view through the viewmodel type
        /// </summary>
        /// <typeparam name="TViewModel">view model type</typeparam>
        /// <returns></returns>
        public string GetViewAssemblyQualifiedName<TViewModel>()
        {
            var name = typeof(TViewModel).Name;
            if (string.IsNullOrEmpty(name) || !name.EndsWith("ViewModel"))
            {
                return string.Empty;
            }

            var viewName = name[..^"ViewModel".Length];

            return ViewDictionary.ContainsKey(viewName) ? ViewDictionary[viewName] : string.Empty;
        }
    }
}
