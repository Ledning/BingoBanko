using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using BankoProject.ViewModels;

namespace BankoProject.Models
{
  [Export(typeof(BingoNumberBoard))]
  public class BingoNumberBoard : PropertyChangedBase
  {
    //supposed to implement a large array and all the needed functionality for controlling the numbers 1-90. 
    //this class could technically just be a list of 90 numbers being removed/added as the game went along, but with extraction of that functionality into seperate classes, it allows for extensions and checks to be made. 

    private BingoNumber[] _board;
    private int _boardSize = 90;
    private WindowManager winMan;

    [ImportingConstructor]
    public BingoNumberBoard(WindowManager winMan)
    {
      Initialize();
      this.winMan = winMan;
    }
    [ImportingConstructor]
    public BingoNumberBoard(int boardSize, WindowManager winMan)
    {
      this.BoardSize = boardSize;
      Initialize();
      this.winMan = winMan;
    }


    private void Initialize()
    {
      Board = new BingoNumber[BoardSize];
      for (int i = 0; i < BoardSize; i++)
      {
        Board[i] = new BingoNumber(i);
      }
    }

    public void PickNumber(int number)
    {
      winMan.ShowDialog(new dialogViewModel("Bekræft tilføjelse: " + number));
    }
    public void UnPickNumber(int number)
    {
      winMan.ShowDialog(new dialogViewModel("Bekræft fjernelse: " + number));
    }
    public void ResetBoard()
    {
      bool confirmed = false;
      dialogViewModel dVM;
      winMan.ShowDialog(dVM = new dialogViewModel("Bekræft reset af spil"));
      confirmed = dVM.Response;
    }






    public BingoNumber[] Board
    {
      get
      {
        return _board;
      }

      set
      {
        _board = value;
        NotifyOfPropertyChange(() => Board);
      }
    }
    public int BoardSize
    {
      get
      {
        return _boardSize;
      }

      set
      {
        _boardSize = value;
        NotifyOfPropertyChange(() => BoardSize);
      }
    }





  }
}
