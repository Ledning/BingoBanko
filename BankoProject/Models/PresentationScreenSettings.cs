using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using BankoProject.Tools;
using BankoProject.ViewModels.PresentationScreen;
using Caliburn.Micro;

namespace BankoProject.Models
{
  //TODO: dw i did it yay
  //TODO: add buttons to controlpanelflyout to control overlay


  /// <summary>
  /// Represents settings for the presentation screen, where is it located, what size/resolution and so on. This class does not know which screen it is, just a central place for info about and actions relating to the presentation screen. 
  /// If the viewmodel itself has need to do anything with the functions here, maybe wrap them inside simple functions. 
  /// </summary>
  [Serializable]
  public class PresentationScreenSettings: PropertyChangedBase
  {

    private int _width;
    private int _height;
    private int _left;
    private int _top;
    private int _selectedPresScreen;
    private SolidColorBrush backgroundBrush;
    private WindowState _state;


    #region Props
    public int Width
    {
      get { return _width; }
      set { _width = value; NotifyOfPropertyChange(() => Width); }
    }

    public int Height
    {
      get { return _height; }
      set { _height = value; NotifyOfPropertyChange(() => Height); }
    }

    public int Left
    {
      get { return _left; }
      set { _left = value; NotifyOfPropertyChange(() => Left); } 
    }

    public int Top
    {
      get { return _top; }
      set { _top = value; NotifyOfPropertyChange(() => Top); }
    }


    public PresentationScreenSettings()
    {
      //PresentationScreenItems.Add(new NumberBarViewModel());
      //PresentationScreenItems.Add(new BingoScreenViewModel());
      //PresentationScreenItems.Add(new FullscreenImageViewModel());
    }
    public int SelectedPresScreen
    {
        get
        {
            return _selectedPresScreen;
        }

        set
        {
            _selectedPresScreen = value; NotifyOfPropertyChange(() => SelectedPresScreen);
        }
    }


    public SolidColorBrush BackgroundBrush
    {
      get { return backgroundBrush; }
      set { backgroundBrush = value; NotifyOfPropertyChange(() => BackgroundBrush); }
    }

    public WindowState State
    {
      get { return _state; }
      set { _state = value; NotifyOfPropertyChange(() => State); }
    }
    #endregion



    

    
    
  }
}
