using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.Models
{
  public class FullscreenOverlaySettings : PropertyChangedBase
  {

    private bool _emptyScreen; //Denne skal hookes ind i blankt overlay knappen
    private bool _boardViewScreen;
    private bool _userDefinedScreen;
    private string _isOverlayVisible;
    private string _selectedBackgroundPath;


    public FullscreenOverlaySettings()
    {
      
    }





    #region props

    public string IsOverlayVisible
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
