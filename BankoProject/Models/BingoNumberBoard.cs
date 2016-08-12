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



    private IWindowManager _winMan;
    public BingoNumberBoard()
    {
      Initialize();
      _winMan = IoC.Get<IWindowManager>();
    }

    private void Initialize() 
    {
      _board = new BindableCollection<BingoNumber>();
      for (int i = 0; i <= _boardSize; i++)
      {
        Board.Add(new BingoNumber(i+1));
      }
    }

    public void PickNumber(int number)
    {
      if (!Board[number].IsPicked)
      {
        throw new NotImplementedException();
        Board[number].IsPicked = true;
      }
    }

    public void UnPickNumber(int number)
    {
      if (Board[number].IsPicked)
      {
        throw new NotImplementedException();
        Board[number].IsPicked = false;
      }
    }





    public void ResetBoard()
    {
      var result = _winMan.ShowDialog(new dialogViewModel("Bekræft reset af spil"));
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
