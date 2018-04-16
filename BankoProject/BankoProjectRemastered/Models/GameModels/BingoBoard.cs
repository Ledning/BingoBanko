using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using BankoProjectRemastered.Interfaces;
using BankoProjectRemastered.Tools;
using Prism.Mvvm;

namespace BankoProjectRemastered.Models.GameModels
{

  /// <summary>
  /// Just a board, no info related to the ui
  /// </summary>
  [Serializable]
  class BingoBoard : BindableBase, IFieldCopyAble
  {
    private readonly int _boardSize;
    private ObservableCollection<BingoNumber> _boardNumbers;

    public BingoBoard(int boardSize)
    {
      BnkLogger.LogLowDebugInfo("Board intialization beginning.");
      BnkLogger.LogLowDebugInfo("Boardsize: 90");
      _boardSize = boardSize; //Assumed to be 90, anything else might distort the visuals
      BoardNumbers = new ObservableCollection<BingoNumber>();
      for (int i = 0; i < BoardSize; i++)
      {
        BoardNumbers.Add(new BingoNumber(i+1));
      }

    }


    #region GetSet

    [XmlArray("Board")]
    [XmlArrayItem(Type = typeof(BingoNumber))]
    public ObservableCollection<BingoNumber> BoardNumbers
    {
      get { return _boardNumbers; }
      set { _boardNumbers = value; RaisePropertyChanged(nameof(BoardNumbers));}
    }

    public int BoardSize
    {
      get { return _boardSize; }
    }

    #endregion

    public ObservableCollection<BingoNumber> AvailableNumbers()
    {
      return null;
    }

    public void CopyFields(object from)
    {
      throw new NotImplementedException();
    }
  }
}