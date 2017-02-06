using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows.Media;
using BankoProject.ViewModels;

namespace BankoProject.Models
{
  [Serializable]
  public class BingoNumber : PropertyChangedBase
  {
    //this class is necessary because the position in which the numbers are being drawn needs to be recorded.
    private readonly int value;
    private bool isPicked = false;
    private bool isSelected = false;
    private readonly ILog _log = LogManager.GetLog(typeof(BingoNumber));

    //TODO: Samme deal som med Comp Obj o lign, der skal laves en alternativ måde at gøre det på.
    public BingoNumber()
    {
      
    }

    public BingoNumber(int value)
    {
      this.value = value;
      NotifyOfPropertyChange(() => Value);
    }

    public bool IsPicked
    {
      get { return isPicked; }

      set
      {
        isPicked = value;
        NotifyOfPropertyChange(() => IsPicked);
      }
    } //whether or not the value has been picked in the current game.

    public bool IsSelected
    {
      get { return isSelected; }
      set { isSelected = value; NotifyOfPropertyChange(()=> IsSelected); }
    }

    public int Value
    {
      get { return value; }
      set
      {
        var temp = value;
      }
    }

    public string VText
    {
      get { return Value.ToString(); }
    }
  }
}