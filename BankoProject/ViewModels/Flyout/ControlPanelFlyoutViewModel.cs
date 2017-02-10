using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using BankoProject.Tools;
using BankoProject.Tools.Events;
using Caliburn.Micro;

namespace BankoProject.ViewModels.Flyout
{
  class ControlPanelFlyoutViewModel : Screen, IFlyoutItem
  {

    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));
    private BingoEvent _bingoEvent;
    private bool _isOpen = false;
    private string _startStopText = "Stop BingoBanko";
    private IEventAggregator _events;

    //IMPORTANT
    //For once in this apps lifetime, these things have to be loaded in here. since this is a flyout, OnViewReady is not called/not called at the appropriate time, so that does not work.
    //On the other hand, this one is 100% called before the flyout is shown, so this works for these. (not for anything else afaik.)
    public ControlPanelFlyoutViewModel()
    {
      Event = IoC.Get<BingoEvent>();
      _events = IoC.Get<IEventAggregator>();
      DisplayName = "";
      _log.Info("ControlFlyoutTriggered");
    }



    public bool IsOpen
    {
      get { return _isOpen; }
      set { _isOpen = value; NotifyOfPropertyChange(() => IsOpen); }
    }

    #region Overrides of ViewAware

    protected override void OnViewReady(object view)
    {

    }

    #endregion

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
        else
        {
          NotifyOfPropertyChange(()=>StartStopText);
        }
      }
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
