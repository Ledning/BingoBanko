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
    public MainWindowViewModel(IEnumerable<IMainViewItem> tabs, IWindowManager windowManager, IEventAggregator events)
    {
      _windowManager = windowManager;
      _eventAggregator = events;
      //Items.AddRange(tabs); Reenable to use tabs
      events.Subscribe(this);

      ActivateItem(new ControlPanelViewModel());

    }

    public void Show()
    {

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
      ActivateItem(new WelcomeViewModel());
    }

    private void GoToControlPanel()
    {
      ActivateItem(new ControlPanelViewModel());
    }
    #endregion

  }
}
