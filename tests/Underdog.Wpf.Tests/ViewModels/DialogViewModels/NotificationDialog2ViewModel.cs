
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;

using Underdog.Core.Dialogs;
using Underdog.Core.Mvvm;

namespace Underdog.Wpf.Tests.ViewModels
{
    public partial class NotificationDialog2ViewModel : ViewModelBase, IDialogAware
    {

        public NotificationDialog2ViewModel()
        {
            CloseDialogCommand = new RelayCommand<string>(CloseDialog);
        }

        public RelayCommand<string> CloseDialogCommand { get; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _title = "Notification2";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DialogCloseListener RequestClose { get; }

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
                result = ButtonResult.OK;
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose.Invoke(dialogResult);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {

        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }
    }
}
