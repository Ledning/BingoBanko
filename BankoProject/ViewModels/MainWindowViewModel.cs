using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using Orchestra.Views;
using System.ComponentModel.Composition;
using System.ComponentModel;
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





    [ImportingConstructor]
    public MainWindowViewModel(IWindowManager windowManager, IEventAggregator events)
    {
      _windowManager = windowManager;
      _eventAggregator = events;
      //Items.AddRange(tabs); Reenable to use tabs
      events.Subscribe(this);
      ActivateItem(new ControlPanelViewModel(_windowManager));

      //See below for example of how to publish an event on a thread. Below event will publish a change of views event, which will change the current view to the one corresponding to the message
      //events.PublishOnUIThread(new ChangeViewEvent(ApplicationWideEnums.MessageTypes.WelcomeView, "MainWindowViewModel"));

    }

    //Pseudo constructor for the view i believ. Called upon initialization of the view, actions relating to startup of the view here. 
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
      ActivateItem(new WelcomeViewModel(_windowManager));
    }

    private void GoToControlPanel()
    {
      ActivateItem(new ControlPanelViewModel(_windowManager));
    }
    #endregion

  }
}
