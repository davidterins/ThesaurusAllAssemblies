using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WPFClient.ViewModels
{
  public class HelloWorldDialogViewModel : BindableBase, IDialogAware
  {
    public string Title => "Hello world";

    public event Action<IDialogResult> RequestClose;

    public ICommand CloseCommand => new DelegateCommand(()
      => RequestClose.Invoke(new DialogResult(ButtonResult.OK)));

    public bool CanCloseDialog()
    {
      return true;
    }

    public void OnDialogClosed()
    {
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
    }
  }
}
