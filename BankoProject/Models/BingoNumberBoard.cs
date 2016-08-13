using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.IO;
using BankoProject.ViewModels;
using System.Windows.Media;

namespace BankoProject.Models
{
  public class BingoNumberBoard : PropertyChangedBase
  {
    //supposed to implement a large array and all the needed functionality for controlling the numbers 1-90. 
    //this class could technically just be a list of 90 numbers being removed/added as the game went along, but with extraction of that functionality into seperate classes, it allows for extensions and checks to be made. 

    private BindableCollection<BingoNumber> _board;
    private int _boardSize = 90;
    private readonly ILog _log = LogManager.GetLog(typeof(BingoNumberBoard));


    private IWindowManager _winMan;
    public BingoNumberBoard()
    {
      Initialize();
      _winMan = IoC.Get<IWindowManager>();
    }

    private void Initialize()
    {
      _log.Info("Initialising bingo-board with 90 numbers...");
      _board = new BindableCollection<BingoNumber>();
      for (int i = 0; i < _boardSize; i++)
      {
        Board.Add(new BingoNumber(i+1));
      }
      _log.Info("Initialization of board done.");
    }

    public void PickNumber(int number)
    {
      _log.Info("Picking number: " + number);
      if (!Board[number].IsPicked)
      {
        bool? result = _winMan.ShowDialog(new dialogViewModel("Træk nr: " + number));
        if (result.HasValue)
        {
          if (result.Value)
          {
            Board[number].IsPicked = true;
          }
        }
      }
    }

    public void UnPickNumber(int number)
    {
      _log.Info("Un-Picking number: " + number);
      if (Board[number].IsPicked)
      {
        bool? result = _winMan.ShowDialog(new dialogViewModel("Træk nr: " + number));
        if (result.HasValue)
        {
          if (result.Value)
          {
            Board[number].IsPicked = false;
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
          foreach (var bingoNumber in Board)
          {
            bingoNumber.IsPicked = false;
          }
        }
      }
    }
   

    public BindableCollection<BingoNumber> Board
    {
      get
      {
        return _board;
      }
    }
  }
}
