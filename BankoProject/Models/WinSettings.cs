using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using BankoProject.Tools;
using Caliburn.Micro;
using ScrnHelper = WpfScreenHelper.Screen;

namespace BankoProject.Models
{
  /// <summary>
  /// Settings / info for the control window. DOES not include settings related to the presentation window, except in the capacity that it shows what screen is chosen to keep the presentation on.
  /// What it does do is populate the list of screens that are available, and calculate basic info about them. 
  /// This is a one-off class. Anything related to a particular screen should be in PresentationScreenSettings. 
  /// </summary>
  /// 
  [Serializable]
  public class WinSettings : PropertyChangedBase
  {
    //These again refer to the controlpanelWindow, not the presentationwindow. 
    private int _width;
    private int _height;
    private int _left;
    private int _top;
    private string currentWindow;

    //This object holds reference to the current presentation screen. 
    private PresentationScreenSettings _prsSettings;

    /// <summary>
    /// List of all the available screens.
    /// </summary>

    [NonSerialized]
    private BindableCollection<ScrnHelper> _screens;



    public WinSettings()
    {
      Screens = new BindableCollection<ScrnHelper>(ScrnHelper.AllScreens);
      PrsSettings = new PresentationScreenSettings();
      PrsSettings.State = WindowState.Normal;
    }





#region
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
    [XmlIgnore]
    [_screens: NonSerialized]
    public BindableCollection<ScrnHelper> Screens
    {
      get { return _screens; }
      set { _screens = value; NotifyOfPropertyChange(() => Screens); }
    }

    public PresentationScreenSettings PrsSettings
    {
      get { return _prsSettings; }
      set { _prsSettings = value; NotifyOfPropertyChange(()=> PrsSettings);}
    }

    public string CurrentWindow
    {
      get { return currentWindow; }
      set { currentWindow = value;  NotifyOfPropertyChange(()=>CurrentWindow);}
    }
  }
#endregion
}
