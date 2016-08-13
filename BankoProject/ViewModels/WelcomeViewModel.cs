using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BankoProject.Models;
using BankoProject.Tools.Events;

namespace BankoProject.ViewModels
{
  class WelcomeViewModel : Screen, IMainViewItem
  {
    private IWindowManager _winMan;
    private IEventAggregator _events;
    private BingoEvent _bingoEvent;
    private readonly ILog _log = LogManager.GetLog(typeof(WelcomeViewModel));


    public WelcomeViewModel()
    {

    }

    protected override void OnViewReady(object view)
    {
      _winMan = IoC.Get<IWindowManager>();
      _events = IoC.Get<IEventAggregator>();
      _bingoEvent = IoC.Get<BingoEvent>();
    }


    public void CreateEvent()
    {

      bool? result = _winMan.ShowDialog(new CreateEventViewModel()); 
      if (result.HasValue)
      {
        if (result.Value)
        {
          _log.Info("exit on event created, welcomeview");
          _events.PublishOnBackgroundThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.Save, "WelcomeViewModel"));
          _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.ControlPanelView, "WelcomeViewModel"));
        }
      }

    }


    public void OpenFileDialog()
    {
      var ofd = new Microsoft.Win32.OpenFileDialog()
      {
        Filter = "Event filer (*.bingoEvent)|*bingoEvent"
      };

      if (ofd.ShowDialog() ?? false)
      {
        var d = ofd.FileNames;
      }
    }
  }
}
