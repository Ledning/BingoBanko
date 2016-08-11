using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Tools;
using BankoProject.Tools.Events;
using Caliburn.Micro;

namespace BankoProject.ViewModels
{
  class DebuggingWindowViewModel : Screen
  {
    private IEventAggregator _eventAggregator;


    public DebuggingWindowViewModel()
    {
      DisplayName = "DebuggingWindow";
    }

    protected override void OnViewReady(object view)
    {
      _eventAggregator = IoC.Get<IEventAggregator>();
    }


    public void ShowWelcome()
    {
      ChangeViewEvent chwe = new ChangeViewEvent(ApplicationWideEnums.MessageTypes.WelcomeView, "DebuggingView");
      _eventAggregator.PublishOnUIThread(chwe);
    }

    public void ShowControl()
    {
      ChangeViewEvent chwe = new ChangeViewEvent(ApplicationWideEnums.MessageTypes.ControlPanelView, "DebuggingView");
      _eventAggregator.PublishOnUIThread(chwe);
    }
  }
}
