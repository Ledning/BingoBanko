using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.Models
{
  //TODO: OMG SOMEBODY PLEASE FIND OUT HOW TO HANDLE SERIALIZATION OF COLLECTIONS


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

    [NonSerialized]
    private BindableCollection<IPresentationScreenItem> _presentationScreenItems;

    /// <summary>
    /// A collection which has the available items to be shown on a presentation screen, by reference to their viewmodels. Filtered based on the IPresentationScreenItem interface. 
    /// </summary>
    [XmlIgnore]
    public BindableCollection<IPresentationScreenItem> PresentationScreenItems
    {
      get { return _presentationScreenItems; }
      set { _presentationScreenItems = value; }
    }

    public SolidColorBrush BackgroundBrush
    {
      get { return backgroundBrush; }
      set { backgroundBrush = value; NotifyOfPropertyChange(()=>BackgroundBrush);}
    }

    public WindowState State
    {
      get { return _state; }
      set { _state = value; NotifyOfPropertyChange(()=> State); }
    }
  }
}
