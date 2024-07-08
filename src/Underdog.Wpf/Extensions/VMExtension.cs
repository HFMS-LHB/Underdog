using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Underdog.Wpf.Navigation.Regions;

namespace Underdog.Wpf.Extensions
{
    public static class VMExtension
    {
        /// <summary>
        /// 注册视图扫描器
        /// 每次调用都会重置
        /// 需要使用IRegionViewScanner传入程序集获取
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly">默认传入的程序集，null：创建空的视图字典</param>
        public static void AddRegionViewScanner(this IServiceCollection services, Assembly? assembly = null)
        {
            services.AddSingleton<IRegionViewScanner, RegionViewScanner>((provider =>
            {
                var viewScanner = new RegionViewScanner();
                if (assembly != null) 
                {
                    viewScanner.ConfigureAssemblies<FrameworkElement>(assembly);
                }
                return viewScanner;
            }));
        }

        /// <summary>
        /// 批量注册视图和视图模型
        /// 默认的命名空间后缀
        /// "Views", "ViewModels", "Views.Dialogs", "ViewModels.Dialogs"
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="suffixs"></param>
        public static void AddViewsAndViewModels(this IServiceCollection services, Assembly assembly, string[]? suffixs = null)
        {
            // 指定要查找的命名空间后缀
            suffixs ??= ["Views", "ViewModels", "Views.Dialogs", "ViewModels.Dialogs"];

            // 获取所有指定命名空间下的所有类
            List<Type> classes = [];

            foreach (var suffix in suffixs)
            {
                classes.AddRange(GetClassesInNamespace(assembly, suffix));
            }

            foreach (var type in classes)
            {
                if (!services.Any(x => x.ServiceType == type))
                {
                    services.AddTransient(type);
                }
            }
        }

        static List<Type> GetClassesInNamespace(Assembly assembly, string suffix)
        {
            return assembly.GetTypes()
                           .Where(t => t is { IsClass: true, IsAbstract: false, Namespace: not null }
                                       && t.Namespace.EndsWith(suffix))
                           .ToList();
        }
    }
}
