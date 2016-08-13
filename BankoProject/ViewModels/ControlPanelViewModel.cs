using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using BankoProject.Tools.Events;

namespace BankoProject.ViewModels
{
  class ControlPanelViewModel : Conductor<IMainViewItem>.Collection.OneActive, IMainViewItem
  {
    private IEventAggregator _events;
    private BingoEvent _bingoEvent;
    public ControlPanelViewModel()
    {
      ActivateItem(new BoardViewModel());
    }

    protected override void OnViewReady(object view)
    {
      _events = IoC.Get<IEventAggregator>();
      _bingoEvent = IoC.Get<BingoEvent>();
      Event.BnkOptions.SingleRow = true;
    }


    public void ShowWelcome()
    {
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.WelcomeView, "ControlPanel"));
    }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set { _bingoEvent = value; NotifyOfPropertyChange(() => Event); }
    }
  }
}
