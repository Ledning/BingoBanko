using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;

namespace BankoProject.ViewModels
{
  class WelcomeViewModel : Screen, IMainViewItem
  {
    private IWindowManager _winMan;
    private IEventAggregator _events;
    private BingoEvent _bingoEvent;


    public WelcomeViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, BingoEvent bingoEvent)
    {
      _winMan = windowManager;
      _events = eventAggregator;
      _bingoEvent = bingoEvent;
    }


    public void CreateEvent()
    {
      _winMan.ShowDialog(new dialogViewModel("popup box!"));
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
