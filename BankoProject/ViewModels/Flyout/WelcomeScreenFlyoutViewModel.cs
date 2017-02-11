using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.Flyout
{
  class WelcomeScreenFlyoutViewModel : Screen, IFlyoutItem
  {



    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));
    private BingoEvent _bingoEvent;
    private bool _isOpen = false;
    private string _startStopText = "Stop BingoBanko";

    //IMPORTANT
    //For once in this apps lifetime, these things have to be loaded in here. since this is a flyout, OnViewReady is not called/not called at the appropriate time, so that does not work.
    //On the other hand, this one is 100% called before the flyout is shown, so this works for these. (not for anything else afaik.)
    public WelcomeScreenFlyoutViewModel()
    {
      Event = IoC.Get<BingoEvent>();
      DisplayName = "";
      _log.Info("WelcomeScreenFlyoutTriggered");
    }


    protected override void OnViewReady(object view)
    {
      
    }


    public bool IsOpen
    {
      get { return _isOpen; }
      set { _isOpen = value; NotifyOfPropertyChange(() => IsOpen); }
    }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set
      {
        _bingoEvent = value;
        NotifyOfPropertyChange(() => Event);
      }
    }

  }
}