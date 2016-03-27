using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.Models
{
  public class BingoNumber : PropertyChangedBase
  {
    //this class is necessary because the position in which the numbers are being drawn needs to be recorded.
    private int value;
    private int position = -1;
    private bool isPicked = false;


    public BingoNumber(int value)
    {
      this.value = value;
    }

    public int Position
    {
      get
      {
        return position;
      }

      set
      {
        position = value;
        NotifyOfPropertyChange(() => Position);
      }
    } //When the number was drawn
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
      }
    } //whether or not the value has been picked in the current game.
  }
}
