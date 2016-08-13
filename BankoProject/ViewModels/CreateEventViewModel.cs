using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using Caliburn.Micro;

namespace BankoProject.ViewModels
{
  class CreateEventViewModel : Screen
  {
    private BingoEvent _bingoEvent;
    private string _title;
    private string _seed;
    private int _pladetal;

    private readonly ILog _log = LogManager.GetLog(typeof(CreateEventViewModel));

    public CreateEventViewModel()
    {
      DisplayName = "Nyt Event";
    }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set { _bingoEvent = value; NotifyOfPropertyChange(() => Event);}
    }
    public int Pladetal
    {
      get { return _pladetal; }
      set { _pladetal = value; NotifyOfPropertyChange(() => Pladetal);}
    }
    public string Seed
    {
      get { return _seed; }
      set { _seed = value; NotifyOfPropertyChange(() => Seed); }
    }
    public string Title
    {
      get { return _title; }
      set { _title = value; NotifyOfPropertyChange(() => Title);}
    }

    protected override void OnViewReady(object view)
    {
      _bingoEvent = IoC.Get<BingoEvent>();
    }

    public void AcceptButton()
    {
      Event.Initialize(Seed, Title, Pladetal);
      _log.Info("Event created, createeventviewmodel");
      TryClose(true);
    }

    public void CancelButton()
    {
      TryClose(false);
    }



  }
}
