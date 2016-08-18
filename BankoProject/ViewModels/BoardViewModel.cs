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
    public BoardViewModel()
    {

    }

    protected override void OnViewReady(object view)
    {
      Event = IoC.Get<BingoEvent>();
    }


    public BingoEvent Event
    {
      get { return _event; }
      set { _event = value; NotifyOfPropertyChange(() => Event);}
    }
  }
}
