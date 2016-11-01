using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using BankoProject.Tools;

namespace BankoProject.ViewModels
{
  class BoardViewModel : Screen, IMainViewItem
  {
    private BingoEvent _event;
    private IWindowManager _winMan;
    private readonly ILog _log = LogManager.GetLog(typeof(BingoNumberBoard));
    private int _selectedIndex = 0;
    public BoardViewModel()
    {

    }

    protected override void OnViewReady(object view)
    {
      Event = IoC.Get<BingoEvent>();
      _winMan = IoC.Get<WindowManager>();
    }


    public BingoEvent Event
    {
      get { return _event; }
      set { _event = value; NotifyOfPropertyChange(() => Event);}
    }

    public void PickNumber(int number)
    {
      _log.Info("Picking number: " + number);
      if (!Event.NumberBoard.Board[number].IsPicked)
      {
        bool? result = _winMan.ShowDialog(new dialogViewModel("Træk nr: " + number));
        if (result.HasValue)
        {
          if (result.Value)
          {
            Event.NumberBoard.Board[number].IsPicked = true;
            NotifyOfPropertyChange(() => Event.NumberBoard.Board[number]);
          }
        }
      }
    }

    public void UnPickNumber(int number)
    {
      _log.Info("Un-Picking number: " + number);
      if (Event.NumberBoard.Board[number].IsPicked)
      {
        bool? result = _winMan.ShowDialog(new dialogViewModel("Træk nr: " + number));
        if (result.HasValue)
        {
          if (result.Value)
          {
            Event.NumberBoard.Board[number].IsPicked = false;
            NotifyOfPropertyChange(() => Event.NumberBoard.Board[number]);
          }
        }
      }
    }





    public void ResetBoard()
    {
      _log.Info("Resetting board...");
      bool? result = _winMan.ShowDialog(new dialogViewModel("Bekræft reset af spil"));
      if (result.HasValue)
      {
        if (result.Value)
        {
          foreach (var bingoNumber in Event.NumberBoard.Board)
          {
            bingoNumber.IsPicked = false;
          }
        }
      }
      NotifyOfPropertyChange(() => Event.NumberBoard.Board);
    }

    public BingoNumber SelectedNumber
    {
      get
      {
        return Event.NumberBoard.Board[_selectedIndex];
      }
    }
  }
}
