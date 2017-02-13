using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace BankoProject.Models
{
  public class FullscreenOverlaySettings : PropertyChangedBase
  {

    private bool _emptyScreen; //Denne skal hookes ind i blankt overlay knappen
    private bool _boardViewScreen;
    private bool _userDefinedScreen;
    private Visibility _isOverlayVisible = Visibility.Visible;
    private string _selectedBackgroundPath;


    public FullscreenOverlaySettings()
    {
      
    }





    #region props

    public Visibility IsOverlayVisible
    {
      get { return _isOverlayVisible; }
      set { _isOverlayVisible = value; NotifyOfPropertyChange(() => IsOverlayVisible); }
    }

    public string SelectedBackgroundPath
    {
      get { return _selectedBackgroundPath; }
      set { _selectedBackgroundPath = value; NotifyOfPropertyChange(() => SelectedBackgroundPath); }
    }

    #endregion



  }
}
