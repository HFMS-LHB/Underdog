﻿using Microsoft.Extensions.DependencyInjection;

using Underdog.Core.Navigation.Regions;

namespace Underdog.Core.Ioc
{
    public static class ContainerExtension
    {
        public static bool IsRegistered(this IServiceProvider serviceProvider, Type type)
        {
            return serviceProvider.GetService(type) == null ? false : true;
        }
        public static bool IsRegistered(this IServiceProvider serviceProvider, Type type, string name)
        {
            return serviceProvider.GetService(type) == null ? false : true;
        }
        public static Type GetRegistrationType(this IServiceProvider serviceProvider, Type type)
        {
            return serviceProvider.GetService(type).GetType();
        }
        public static Type GetRegistrationType(this IServiceProvider serviceProvider, string name)
        {
            var type = Type.GetType(name);
            return serviceProvider.GetService(type).GetType();
        }
        public static object? GetService(this IServiceProvider serviceProvider, string name)
        {
            var type = Type.GetType(name);
            return serviceProvider.GetService(type);
        }
    }
}
