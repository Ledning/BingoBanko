using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.Models
{
  [Serializable]
  public class VisualsOptions : PropertyChangedBase
  {
    [NonSerialized]
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
