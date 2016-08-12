using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using Orchestra.Views;
using System.ComponentModel.Composition;
using System.ComponentModel;
using BankoProject.Models;
using Caliburn.Micro;
using BankoProject.Tools;
using BankoProject.Tools.Events;

namespace BankoProject.ViewModels
{
  /*
   HOW TO DO EVENTS AND USE WINDOWMANAGER:

    In order to use anything contained in the IOC container this project has, it is as simple as creating a property to contain it, and then assigning that propert to the corresponding value from IOC:
    someproperty = IoC.Get<IWindowManager>();

    As of right now, it has 4 things:
    WindowManager  (Use IWindowManager)
    EventAggregator (Use IEventAggregator)
    BingoEvent (Use BingoEvent)

    Windowmanager is the only one which is not a singleton (meaning it wont be the same instance passed around)
    BingoEvent has all the info needed to run and show an event.

    To use WindowManager to show a dialog:
    _winMan.ShowDialog(new dialogViewModel("dialog text"))



  */
  [Export(typeof(IShell))]
  class MainWindowViewModel : Conductor<IMainViewItem>.Collection.OneActive, IShell, IHandle<ChangeViewEvent>, ISave, ILoad
  {
    private IWindowManager _winMan;
    private IEventAggregator _eventAggregator;
    private BingoEvent _bingoEvent;

    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));






    public MainWindowViewModel()
    {

      ActivateItem(new WelcomeViewModel());
      DisplayName = "Bingo Kontrol";


    }


    //The function below can be used as a constructor for the view. Everything in it will happen after the view is loaded.
    protected override void OnViewReady(object view)
    {
      _winMan = IoC.Get<IWindowManager>();
      _eventAggregator = IoC.Get<IEventAggregator>();
      _bingoEvent = IoC.Get<BingoEvent>();
      _eventAggregator.Subscribe(this);
      _log.Info("Main View loaded");
      _winMan.ShowWindow(new DebuggingWindowViewModel());
    }

    public void Handle(ChangeViewEvent message)
    {
      switch (message.ViewName)
      {
        case "WelcomeView":
          _log.Info("Changing to WelcomeView...");
          GoToWelcomeView();
          break;

        case "ControlPanelView":
          _log.Info("Changing to ControlPanelView...");
          GoToControlPanel();
          break;

        case "Save":
          _log.Info("Saving...");
          SaveSession(ref _bingoEvent);
          break;

        case "Load":
          _log.Info("Loading...");
          LoadSession(ref _bingoEvent);
          break;
      }
    }


    #region eventbusMethods

    private void GoToWelcomeView()
    {
      ActivateItem(new WelcomeViewModel());
    }

    private void GoToControlPanel()
    {
      ActivateItem(new ControlPanelViewModel());
    }
    #endregion


    public bool LoadSession(ref BingoEvent bingoEvent)
    {
      throw new NotImplementedException();
    }

    public bool SaveSession(ref BingoEvent bingoEvent)
    {
      throw new NotImplementedException();
    }

    public void Show()
    {
    }
  }
}
