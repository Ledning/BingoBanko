using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
namespace BankoProject.Models
{
  public class WinSettings : PropertyChangedBase
  {
    private int _width;
    private int _height;
    private int _left;
    private int _top;

    public int Width
    {
      get { return _width; }
      set { _width = value; NotifyOfPropertyChange(()=>Width);}
    }

    public int Height
    {
      get { return _height; }
      set { _height = value; NotifyOfPropertyChange(()=>Height);}
    }

    public int Left
    {
      get { return _left; }
      set { _left = value; NotifyOfPropertyChange(()=>Left);}
    }

    public int Top
    {
      get { return _top; }
      set { _top = value; NotifyOfPropertyChange(()=>Top);}
    }
  }
}
