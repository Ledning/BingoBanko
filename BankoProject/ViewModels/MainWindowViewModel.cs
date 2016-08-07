using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using Orchestra.Views;
using System.ComponentModel.Composition;
using System.ComponentModel;
using System.Windows;
using BankoProject.Models;
using Caliburn.Micro;
using BankoProject.Tools;
using BankoProject.Tools.Events;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BankoProject.ViewModels
{
  [Export(typeof(IShell))]
  class MainWindowViewModel : Conductor<IMainViewItem>.Collection.OneActive, IShell, IHandle<ChangeViewEvent>
  {
    private IWindowManager _windowManager;
    private IEventAggregator _eventAggregator;
    private BingoEvent _bingoEvent;
    private DialogCoordinator _dialogCoordinator;





    [ImportingConstructor]
    public MainWindowViewModel(IWindowManager windowManager, IEventAggregator events, BingoEvent bingoEvent, IDialogCoordinator dialogCoordinator)
    {
      _windowManager = windowManager;
      _eventAggregator = events;
      _bingoEvent = bingoEvent;
      events.Subscribe(this);
      //ActivateItem(new WelcomeViewModel(_windowManager, _eventAggregator, _bingoEvent));
      ActivateItem(new ControlPanelViewModel());
      DisplayName = "Bingo Kontrol";

      //See below for example of how to publish an event on a thread. Below event will publish a change of views event, which will change the current view to the one corresponding to the message
      //events.PublishOnUIThread(new ChangeViewEvent(ApplicationWideEnums.MessageTypes.WelcomeView, "MainWindowViewModel"));

      //To use windowManager, pass it as a parameter to the viewmodel that needs it. See WelcomeViewModel for example. 
      //Called like this:
      //_winMan.ShowDialog(new dialogViewModel("hrelo men")); u get the idea

    }

    //Pseudo constructor for the view i believe. Called upon initialization of the view, actions relating to startup of the view here. 
    public void Show()
    {

    }


    public sealed override void ActivateItem(IMainViewItem item)
    {
      base.ActivateItem(item);
    }




    public void Handle(ChangeViewEvent message)
    {
      switch (message.ViewName)
      {
        case "WelcomeView":
          GoToWelcomeView();
          break;

        case "ControlPanel":
          GoToControlPanel();
          break;
      }
    }

    public void InputDialog()
    {
      var metroDialogSettings = new MetroDialogSettings
      {
        CustomResourceDictionary =
              new ResourceDictionary
              {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.MahApps.Dialogs.xaml")
              },
        NegativeButtonText = "CANCEL",
        SuppressDefaultResources = true
      };

      DialogCoordinator.Instance.ShowInputAsync(this, "MahApps Dialog", "Using Material Design Themes", metroDialogSettings);
    }

    #region ChangeViewMethods

    private void GoToWelcomeView()
    {
      ActivateItem(new WelcomeViewModel(_windowManager, _eventAggregator, _bingoEvent));
    }

    private void GoToControlPanel()
    {
      ActivateItem(new ControlPanelViewModel());
    }
    #endregion

  }
}
