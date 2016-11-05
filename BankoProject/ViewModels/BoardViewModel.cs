using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using BankoProject.Tools;

namespace BankoProject.ViewModels
{
  class BoardViewModel : Screen, IMainViewItem
  {
    private BingoEvent _event;
    private IWindowManager _winMan;
    private readonly ILog _log = LogManager.GetLog(typeof(BingoNumberBoard));
    private int _selectedIndex = 0;
    public BoardViewModel()
    {

    }

    protected override void OnViewReady(object view)
    {
      Event = IoC.Get<BingoEvent>();
      _winMan = IoC.Get<WindowManager>();
    }


    public BingoEvent Event
    {
      get { return _event; }
      set { _event = value; NotifyOfPropertyChange(() => Event);}
    }


  }
}
