using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BankoProject.Models;
using BankoProject.Tools;
using BankoProject.Tools.Events;
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace BankoProject.ViewModels
{
  class DebuggingWindowViewModel : Screen, IHandle<CommunicationObject>
  {
    private IEventAggregator _eventAggregator;
    private WinSettings _winSngs;
    private BingoEvent _event;


    public DebuggingWindowViewModel(int width, int height, int left, int top)
    {
      WinSngs = new WinSettings();
      WinSngs.Width = width;
      WinSngs.Height = height;
      WinSngs.Left = left;
      WinSngs.Top = top;
      WinSngs.CurrentWindow = ApplicationWideEnums.MessageTypes.ChngWelcomeView.ToString();
      DisplayName = "DebuggingWindow";
    }

    protected override void OnViewReady(object view)
    {
      _eventAggregator = IoC.Get<IEventAggregator>();
      _eventAggregator.Subscribe(this);
      Event = IoC.Get<BingoEvent>();
    }

    public void RebootPrezScreen()
    {
      CommunicationObject rbps = new CommunicationObject(ApplicationWideEnums.MessageTypes.RbPrezScreen, ApplicationWideEnums.SenderTypes.DebuggingView);
      _eventAggregator.PublishOnUIThread(rbps);
    }


    public WinSettings WinSngs
    {
      get { return _winSngs; }
      set
      {
        _winSngs = value;
        NotifyOfPropertyChange(()=>WinSngs);
      }
    }

    public BingoEvent Event
    {
      get { return _event; }
      set { _event = value; NotifyOfPropertyChange(()=>Event);}
    }

    public void showPresBackground()
    {
      if (Event.PresScreenSettings.BackgroundBrush == null)
      {
        Event.PresScreenSettings.BackgroundBrush = new SolidColorBrush(Colors.Black);
      }
      else if(Event.PresScreenSettings.BackgroundBrush.Color == Colors.Black)
      {
        Event.PresScreenSettings.BackgroundBrush = null;
      }
      else
      {
        Event.PresScreenSettings.BackgroundBrush = new SolidColorBrush(Colors.Black);
      }
      
    }

    public void ShowWelcome()
    {
      CommunicationObject chwe = new CommunicationObject(ApplicationWideEnums.MessageTypes.ChngWelcomeView, ApplicationWideEnums.SenderTypes.DebuggingView);
      _eventAggregator.PublishOnUIThread(chwe);
    }

    public void ShowControl()
    {
      CommunicationObject chwe = new CommunicationObject(ApplicationWideEnums.MessageTypes.ChngControlPanelView, ApplicationWideEnums.SenderTypes.DebuggingView);
      _eventAggregator.PublishOnUIThread(chwe);
    }

    #region Implementation of IHandle<CommunicationObject>

    public void Handle(CommunicationObject message)
    {
      WinSngs.CurrentWindow = message.Message.ToString();
    }

    #endregion
  }
}
