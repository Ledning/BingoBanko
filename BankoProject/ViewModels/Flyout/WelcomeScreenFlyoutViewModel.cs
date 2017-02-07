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


    public WelcomeScreenFlyoutViewModel()
    {
      Event = IoC.Get<BingoEvent>();
      DisplayName = "";
      _log.Info("WelcomeScreenFlyoutTriggered");
    }


    protected override void OnViewReady(object view)
    {

    }

    public void ToggleBingo()
    {
      if (Event != null)
      {
        if (Event.IsBingoRunning)
        {
          Event.IsBingoRunning = false;
          StartStopText = "Start BingoBanko";
        }
        else if (!Event.IsBingoRunning)
        {
          Event.IsBingoRunning = true;
          StartStopText = "Stop BingoBanko";
        }
      }
    }

    public void Reset()
    {
      //Not sure waht goes in here yet
      _log.Info("NOTIMPLEMENTED");
    }

    #region props

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set
      {
        _bingoEvent = value;
        NotifyOfPropertyChange(() => Event);
      }
    }

    public bool IsOpen
    {
      get { return _isOpen; }
      set
      {
        _isOpen = value;
        NotifyOfPropertyChange(() => IsOpen);
      }
    }

    public string StartStopText
    {
      get { return _startStopText; }
      set
      {
        _startStopText = value;
        NotifyOfPropertyChange(() => StartStopText);
      }
    }

    #endregion
  }
}