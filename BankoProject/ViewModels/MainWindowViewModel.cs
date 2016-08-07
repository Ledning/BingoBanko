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
  [Export(typeof(IShell))]
  class MainWindowViewModel : Conductor<IMainViewItem>.Collection.OneActive, IShell, IHandle<ChangeViewEvent>
  {
    private IWindowManager _windowManager;
    private IEventAggregator _eventAggregator;
    private BingoEvent _bingoEvent;





    [ImportingConstructor]
    public MainWindowViewModel(IWindowManager windowManager, IEventAggregator events, BingoEvent bingoEvent)
    {
      _windowManager = windowManager;
      _eventAggregator = events;
      _bingoEvent = bingoEvent;
      events.Subscribe(this);
      ActivateItem(new WelcomeViewModel(_windowManager, _eventAggregator, _bingoEvent));

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
