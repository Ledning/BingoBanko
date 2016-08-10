using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using Caliburn.Micro;
using System.ComponentModel.Composition;
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
    private Stack<BingoNumber> pickingOrder;



    private WindowManager winMan;
    public BingoNumberBoard(int boardSize, WindowManager winMan)
    {
      this.BoardSize = boardSize;
      Initialize();
      this.winMan = winMan;
    }


    private void Initialize() 
    {
      Board = new BindableCollection<BingoNumber>();
      for (int i = 0; i <= BoardSize; i++)
      {
        Board.Add(new BingoNumber(i+1));
      }
    }

    public void PickNumber(int number)
    {
      var result = winMan.ShowDialog(new dialogViewModel("Bekræft tilføjelse: " + number));
      if (result.HasValue)
      {
        if (result.Value)
        {
          Board[number].IsPicked = true;
          PickingOrder.Push(Board[number]);
        }
      }
      else
      {
        throw new ArgumentException();
      }
      
    }
    public void UnPickNumber(int number)
    {
      var result = winMan.ShowDialog(new dialogViewModel("Bekræft fjernelse: " + number));

      if (result.HasValue)
      {
        if (result.Value)
        {
          Board[number].IsPicked = false;
          PickingOrder.Pop();
        }
      }      
    }

    public void ResetBoard()
    {
      var result = winMan.ShowDialog(new dialogViewModel("Bekræft reset af spil"));
      if (result.HasValue)
      {
        if (result.Value)
        {
          Board = null;
          Initialize();
        }
      }
    }
   

    public BindableCollection<BingoNumber> Board
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
    public Stack<BingoNumber> PickingOrder
    {
      get
      {
        return pickingOrder;
      }

      set
      {
        pickingOrder = value;
        NotifyOfPropertyChange(() => PickingOrder);
      }
    }
  }
}
