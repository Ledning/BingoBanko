using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.Flyout
{
  class WelcomeScreenFlyoutViewModel : IMainViewItem
  {
    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));

    public WelcomeScreenFlyoutViewModel()
    {
      _log.Info("WelcomeScreenFlyoutTriggered");
    }

    public void Reset()
    {
      //TODO: bind til bingoevent pls
    }
  }
}