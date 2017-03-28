using System;
using System.Xml.Serialization;
using Caliburn.Micro;

namespace TestProject.Models
{
  /// <summary>
  /// Why does this class exist. it should be migrated into WinSettings or something. Atleast not here and not name like this its retarded. 
  /// </summary>
  [Serializable]
  public class VisualsOptions : PropertyChangedBase
  {
    [XmlIgnore]
    private readonly ILog _log = LogManager.GetLog(typeof(BankoOptions));

    private bool _emptyScreen;
    private bool _plateScreen;
    private bool _userDefinedScreen;
    private BindableCollection<string> _userDefinedScreens;

#region
    public BindableCollection<string> UserDefinedScreens
    {
      get { return _userDefinedScreens; }
      set { _userDefinedScreens = value; NotifyOfPropertyChange(() => UserDefinedScreens); }
    }

    public bool EmptyScreen
    {
      get { return _emptyScreen; }
      set { _emptyScreen = value; NotifyOfPropertyChange(() => EmptyScreen);}
    }

    public bool PlateScreen
    {
      get { return _plateScreen; }
      set { _plateScreen = value; NotifyOfPropertyChange(() => PlateScreen);}
    }

    public bool UserDefinedScreen
    {
      get { return _userDefinedScreen; }
      set { _userDefinedScreen = value; NotifyOfPropertyChange(() => UserDefinedScreen);}
    }
#endregion
  }
}
