using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underdog.Wpf.Tests.ModuleA.ViewModels
{
    public partial class ViewAViewModel:ObservableObject
    {
        [ObservableProperty]
        private string title = "ModuleA.ViewA";
    }
}
