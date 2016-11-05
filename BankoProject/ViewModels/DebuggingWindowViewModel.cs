using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using BankoProject.Tools;
using BankoProject.Tools.Events;
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace BankoProject.ViewModels
{
  class DebuggingWindowViewModel : Screen
  {
    private IEventAggregator _eventAggregator;
    private WinSettings _winSngs;


    public DebuggingWindowViewModel(int width, int height, int left, int top)
    {
      WinSngs = new WinSettings();
      WinSngs.Width = width;
      WinSngs.Height = height;
      WinSngs.Left = left;
      WinSngs.Top = top;
      DisplayName = "DebuggingWindow";
    }

    protected override void OnViewReady(object view)
    {
      _eventAggregator = IoC.Get<IEventAggregator>();
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
  }
}
