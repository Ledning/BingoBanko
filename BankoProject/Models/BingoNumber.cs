using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows.Media;

namespace BankoProject.Models
{
  public class BingoNumber : PropertyChangedBase
  {
    //this class is necessary because the position in which the numbers are being drawn needs to be recorded.
    private readonly int value;
    private bool isPicked = false;
    private bool isDisabled = false;


    public BingoNumber(int value)
    {
      this.value = value;
      NotifyOfPropertyChange(() => Value);
    }

    public bool IsPicked
    {
      get
      {
        return isPicked;
      }

      set
      {
        isPicked = value;
        NotifyOfPropertyChange(() => IsPicked);
        NotifyOfPropertyChange(() => BackBrush);
      }
    } //whether or not the value has been picked in the current game.

    public int Value
    {
      get
      {
        return value;
      }
    }

    public SolidColorBrush BackBrush
    {
      get
      {
        if (isPicked)
        {
          return Brushes.Red;
        }
        else
        {
          return Brushes.Black;
        }
      }
    }

    public bool IsDisabled
    {
      get
      {
        return isDisabled;
      }

      set
      {
        isDisabled = value;
        NotifyOfPropertyChange(() => IsDisabled);
      }
    }
  }
}
