# Underdog.Core

基于Prism的wpf框架

# 鸣谢

**根据这个修改来的**

- [StupidBear](https://github.com/AelousDing/StupidBear)


# 开发信息

- NET8 + VS2022
- WPF

# 支持功能

- 移植了Prism框架中的模块化、mvvm、region、dialog
- 支持NET8及以上版本

# 快速使用

**安装包**

```nuget
    dotnet add package Underdog.Core --version 1.0.3
    dotnet add package Underdog.Wpf --version 1.0.3
```


**1.创建`Program.cs`文件**

```Program.cs

    public class Program
    {
        public static IHost? AppHost { get; private set; }
        public static IServiceProvider ServiceProvider => AppHost!.Services;

        [System.STAThreadAttribute()]
        public static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                // 注册模块
                .ConfigureServices(ModularityExtension.AddModularity)
                .ConfigureServices((hosting,context)=>
                {
                    services.AddSingleton<App>();
                    services.AddScoped<MainWindow>();
                    services.AddScoped<MainWindowViewModel>();
                    services.AddHostedService<WPFHostedService<App, MainWindow>>(); // 配置启动窗口
                    services.AddRegion(); // 添加区域
                    // 添加视图扫描器，由于region的name需要传完整视图文件所在程序集的完全限定名,提供视图扫描器获取名称
                    services.AddRegionViewScanner();
                    services.AddDialog(); // 添加Dialog
                    services.AddMvvm(); // 添加MVVM
                });

            AppHost = builder.Build();

            AppHost.UseRegion<MainWindow>();
            // 注册区域
            // var regionManager = AppHost.Services.GetService<IRegionManager>();
            // regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
            // regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewB));

            // 启用模块化
            AppHost.UseModularity();

            AppHost.Run();
        }
    }
```

**2.修改.csproj文件,指定启动对象为Program**

```csproj
  <PropertyGroup>
	  <StartupObject>YourNamespace.Program</StartupObject>
  </PropertyGroup>
```

**3.修改App.xaml**

*删除 StartupUri="MainWindow.xaml" 属性*

**4.启动程序**

# MVVM

# Dialog

# Region

# Modularity