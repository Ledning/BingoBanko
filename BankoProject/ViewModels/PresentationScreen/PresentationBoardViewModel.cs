using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using Caliburn.Micro;

namespace BankoProject.ViewModels.PresentationScreen
{
  class PresentationBoardViewModel : Screen
  {
    private BingoEvent _event;

    public PresentationBoardViewModel()
    {
      _event = IoC.Get<BingoEvent>();
    }

    







    public BingoEvent Event
    {
      get { return _event; }
      set { _event = value; NotifyOfPropertyChange(()=>Event);}
    }
  }
}
