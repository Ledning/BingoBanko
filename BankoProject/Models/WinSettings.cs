using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using ScrnHelper = WpfScreenHelper.Screen;

namespace BankoProject.Models
{
  /// <summary>
  /// Settings / info for the control window. DOES not include settings related to the presentation window. 
  /// </summary>
  public class WinSettings : PropertyChangedBase
  {
    private int _width;
    private int _height;
    private int _left;
    private int _top;

    /// <summary>
    /// List of all the available screens.
    /// </summary>
    private BindableCollection<ScrnHelper> _screens;

    /// <summary>
    /// Describes which screen is the one showing the presentation overlay. 
    /// 
    /// </summary>
    private int _selectedPresentationScreen;

    public WinSettings()
    {
      Screens = new BindableCollection<ScrnHelper>(ScrnHelper.AllScreens);
    }

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
    public BindableCollection<ScrnHelper> Screens
    {
      get { return _screens; }
      set { _screens = value; NotifyOfPropertyChange(() => Screens); }
    }

    /// <summary>
    /// Describes which screen is the one showing the presentation overlay. 
    /// </summary>
    public int SelectedPresentationScreen
    {
      get { return _selectedPresentationScreen; }
      set { _selectedPresentationScreen = value; NotifyOfPropertyChange(()=> SelectedPresentationScreen);}
    }
  }
}
