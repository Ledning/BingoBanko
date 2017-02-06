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
  class WelcomeScreenFlyoutViewModel : Screen, IMainViewItem
  {
    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));
    private BingoEvent _bingoEvent;

    public WelcomeScreenFlyoutViewModel()
    {
      _log.Info("WelcomeScreenFlyoutTriggered");
    }


    protected override void OnViewReady(object view)
    {
      Event = IoC.Get<BingoEvent>();
      NotifyOfPropertyChange(() => StartStopText);
    }

    /// <summary>
    /// Resets the board, not the game. If a full application/event reset is desired, a new event should be created instead.
    /// </summary>
    public void Reset()
    {
      Event.BingoNumberQueue = new BindableCollection<BingoNumber>();
      Event.NumberBoard = new BingoNumberBoard();
    }

    public void ToggleBingo()
    {
      if (Event.IsBingoRunning)
      {
        StopBingo();
        NotifyOfPropertyChange(()=> StartStopText);
      }
      else
      {
        StartBingo();
        NotifyOfPropertyChange(() => StartStopText);
      }
    }

    public void StartBingo()
    {
      Event.IsBingoRunning = true;
    }
    public void StopBingo()
    {
      Event.IsBingoRunning = false;
    }

    public string StartStopText
    {
      get
      {
        if (Event.IsBingoRunning)
        {
          return "Stop Bingo";
        }
        else
        {
          return "Start Bingo";
        }
      }
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