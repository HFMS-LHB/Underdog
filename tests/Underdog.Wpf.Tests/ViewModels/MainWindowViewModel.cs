using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Underdog.Core.Dialogs;
using Underdog.Core.Mvvm;
using Underdog.Wpf.Dialogs;

namespace Underdog.Wpf.Tests.ViewModels
{
    public partial class MainWindowViewModel:ViewModelBase
    {
        private IDialogService _dialogService;
        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            ShowDialog1Command = new RelayCommand(() =>
            {
                var message = "This is a message that should be shown in the dialog.";
                //using the dialog service as-is
                _dialogService.ShowDialog("NotificationDialog1", new DialogParameters($"message={message}"), r =>
                {
                    if (r.Result == ButtonResult.None)
                        Title = "Result is None";
                    else if (r.Result == ButtonResult.OK)
                        Title = "Result is OK";
                    else if (r.Result == ButtonResult.Cancel)
                        Title = "Result is Cancel";
                    else
                        Title = "I Don't know what you did!?";
                });
            });

            ShowDialog2Command = new RelayCommand(() =>
            {
                var message = "This is a message that should be shown in the dialog.";
                //using the dialog service as-is
                _dialogService.ShowDialog("NotificationDialog2", new DialogParameters($"message={message}"), r =>
                {
                    if (r.Result == ButtonResult.None)
                        Title = "Result is None";
                    else if (r.Result == ButtonResult.OK)
                        Title = "Result is OK";
                    else if (r.Result == ButtonResult.Cancel)
                        Title = "Result is Cancel";
                    else
                        Title = "I Don't know what you did!?";
                });
            });
        }

        public RelayCommand ShowDialog1Command { get; }
        public RelayCommand ShowDialog2Command { get; }

        [ObservableProperty]
        private string title;
    }
}
