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

    As of right now, it has 3(4) things:
    WindowManager  (Use IWindowManager)
    EventAggregator (Use IEventAggregator)
    BingoEvent (Use BingoEvent)

    Windowmanager is the only one which is not a singleton (meaning it wont be the same instance passed around)
    BingoEvent has all the info needed to run and show an event.

    To use WindowManager to show a dialog:
    _winMan.ShowDialog(new dialogViewModel("dialog text"))



  */
  [Export(typeof(IShell))]
  class MainWindowViewModel : Conductor<IMainViewItem>.Collection.OneActive, IShell, IHandle<CommunicationObject>, ISave, ILoad
  {
    private IWindowManager _winMan;
    private IEventAggregator _eventAggregator;
    private BingoEvent _bingoEvent;

    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));






    public MainWindowViewModel()
    {

      ActivateItem(new WelcomeViewModel());
      DisplayName = "Banko-Kontrol";
    }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set { _bingoEvent = value; NotifyOfPropertyChange(()=> Event);}
    }


    //The function below can be used as a constructor for the view. Everything in it will happen after the view is loaded.
    protected override void OnViewReady(object view)
    {
      _winMan = IoC.Get<IWindowManager>();
      _eventAggregator = IoC.Get<IEventAggregator>();
      Event = IoC.Get<BingoEvent>();
      _eventAggregator.Subscribe(this);
      _log.Info("Main View loaded");
      //_winMan.ShowWindow(new DebuggingWindowViewModel());
    }

    public void Handle(CommunicationObject message)
    {
      switch (message.Message)
      {
        case ApplicationWideEnums.MessageTypes.ChngWelcomeView:
          _log.Info("Changing to WelcomeView...");
          DisplayName = "Bingo Kontrol";
          GoToWelcomeView();
          break;

        case ApplicationWideEnums.MessageTypes.ChngControlPanelView:
          _log.Info("Changing to ControlPanelView...");
          DisplayName = DisplayName + ": " + Event.EventTitle;
          GoToControlPanel();
          break;

        case ApplicationWideEnums.MessageTypes.Save:
          _log.Info("Saving...");
          SaveSession(ref _bingoEvent);
          break;

        case ApplicationWideEnums.MessageTypes.Load:
          _log.Info("Loading...");
          LoadSession(ref _bingoEvent);
          break;
      }
    }


    #region eventbusMethods

    private void GoToWelcomeView()
    {
      ActivateItem(new WelcomeViewModel());
      DisplayName = "Banko-Kontrol";
    }

    private void GoToControlPanel()
    {
      ActivateItem(new ControlPanelViewModel());
      DisplayName = Event.EventTitle;
    }
    #endregion


    public bool LoadSession(ref BingoEvent bingoEvent)
    {
      _log.Warn("NOT IMPLEMENTED");
      return false;
    }

    public bool SaveSession(ref BingoEvent bingoEvent)
    {
      _log.Warn("NOT IMPLEMENTED");
      return false;
    }

    public void Show()
    {
    }
  }
}
