using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.Models
{
  /// <summary>
  /// Represents settings for the presentation screen, where is it located, what size/resolution and so on. This class does not know which screen it is, just a central place for info about and actions relating to the presentation screen. 
  /// If the viewmodel itself has need to do anything with the functions here, maybe wrap them inside simple functions. 
  /// </summary>
  public class PresentationScreenSettings: PropertyChangedBase
  {

    private int _width;
    private int _height;
    private int _left;
    private int _top;
    private int _selectedPresScreen;
    private SolidColorBrush backgroundBrush;

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

        /// <summary>
        /// A collection which has the available items to be shown on a presentation screen, by reference to their viewmodels. Filtered based on the IPresentationScreenItem interface. 
        /// </summary>
        public BindableCollection<IPresentationScreenItem> PresentationScreenItems { get; set; }

        
    }
}
