# Underdog.Core

����Prism��wpf���

# ��л

**��������޸�����**

- [StupidBear](https://github.com/AelousDing/StupidBear)


# ������Ϣ

- NET8 + VS2022
- WPF

# ֧�ֹ���

- ��ֲ��Prism����е�ģ�黯��mvvm��region��dialog
- ֧��NET8�����ϰ汾

# ����ʹ��

**��װ��**

```nuget
    dotnet add package Underdog.Core --version 1.0.3
    dotnet add package Underdog.Wpf --version 1.0.3
```


**1.����`Program.cs`�ļ�**

```Program.cs

    public class Program
    {
        public static IHost? AppHost { get; private set; }
        public static IServiceProvider ServiceProvider => AppHost!.Services;

        [System.STAThreadAttribute()]
        public static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                // ע��ģ��
                .ConfigureServices(ModularityExtension.AddModularity)
                .ConfigureServices((hosting,context)=>
                {
                    services.AddSingleton<App>();
                    services.AddScoped<MainWindow>();
                    services.AddScoped<MainWindowViewModel>();
                    services.AddHostedService<WPFHostedService<App, MainWindow>>(); // ������������
                    services.AddRegion(); // �������
                    // �����ͼɨ����������region��name��Ҫ��������ͼ�ļ����ڳ��򼯵���ȫ�޶���,�ṩ��ͼɨ������ȡ����
                    services.AddRegionViewScanner();
                    services.AddDialog(); // ���Dialog
                    services.AddMvvm(); // ���MVVM
                });

            AppHost = builder.Build();

            AppHost.UseRegion<MainWindow>();
            // ע������
            // var regionManager = AppHost.Services.GetService<IRegionManager>();
            // regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
            // regionManager?.RegisterViewWithRegion("ContentRegion", typeof(ViewB));

            // ����ģ�黯
            AppHost.UseModularity();

            AppHost.Run();
        }
    }
```

**2.�޸�.csproj�ļ�,ָ����������ΪProgram**

```csproj
  <PropertyGroup>
	  <StartupObject>YourNamespace.Program</StartupObject>
  </PropertyGroup>
```

**3.�޸�App.xaml**

*ɾ�� StartupUri="MainWindow.xaml" ����*

**4.��������**

# MVVM

# Dialog

# Region

# Modularity