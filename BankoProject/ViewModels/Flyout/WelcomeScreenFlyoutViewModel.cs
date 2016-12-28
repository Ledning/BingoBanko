using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.Flyout
{
  class WelcomeScreenFlyoutViewModel : PropertyChangedBase, IMainViewItem
  {
    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));
    private BingoEvent _bingoEvent;

    public WelcomeScreenFlyoutViewModel()
    {
      _log.Info("WelcomeScreenFlyoutTriggered");
    }

    public void Reset()
    {
      //TODO: bind til bingoevent pls
    }


    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set { _bingoEvent = value; NotifyOfPropertyChange(() => Event); }
    }
  }
}