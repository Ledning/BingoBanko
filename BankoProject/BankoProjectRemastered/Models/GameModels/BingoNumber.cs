using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProjectRemastered.Tools;
using Prism.Mvvm;

namespace BankoProjectRemastered.Models.GameModels
{
  /// <summary>
  /// Just a number, no info related to the ui 
  /// </summary>
  [Serializable]
  public class BingoNumber : BindableBase
  {
    private int _value;
    private bool _hasBeenDrawn;

    public BingoNumber(int value)
    {
      BnkLogger.LogLowDebugInfo("BingoNumber init nr: " + value);
      HasBeenDrawn = false;
      _value = value;
    }


    #region GetSet
    public bool HasBeenDrawn
    {
      get { return _hasBeenDrawn; }
      set
      {
        _hasBeenDrawn = value;
        RaisePropertyChanged(nameof(HasBeenDrawn));
      }
    }

    public int Value
    {
      get { return _value; }
    }
    #endregion

  }
}