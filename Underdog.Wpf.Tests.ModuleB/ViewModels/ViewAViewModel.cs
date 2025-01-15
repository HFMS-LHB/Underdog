﻿using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underdog.Wpf.Tests.ModuleB.ViewModels
{
    public partial class ViewAViewModel:ObservableObject
    {
        private readonly IConfiguration _configuration;
        public ViewAViewModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [ObservableProperty]
        private string title = "ModuleB.ViewA";
    }
}
