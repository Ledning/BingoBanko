using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    private string _phTitle;
    private string _seed;
    private int _phPladetal;

    private readonly ILog _log = LogManager.GetLog(typeof(CreateEventViewModel));

    public CreateEventViewModel()
    {
 
    }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set
      {
        _bingoEvent = value;
        NotifyOfPropertyChange(() => Event);
      }
    }

    public int PhPladetal
    {
      get { return _phPladetal; }
      set { _phPladetal = value; NotifyOfPropertyChange(() => PhPladetal);}
    }
    public string PhSeed
    {
      get { return _seed; }
      set { _seed = value; NotifyOfPropertyChange(() => PhSeed); }
    }
    public string PhTitle
    {
      get { return _phTitle; }
      set { _phTitle = value; NotifyOfPropertyChange(() => PhTitle);}
    }

    protected override void OnViewReady(object view)
    {
      Event = IoC.Get<BingoEvent>();
      DisplayName = "Nyt Event";
    }

    public void AcceptButton()
    {
      Event.Initialize(PhSeed, PhTitle, PhPladetal);
      _log.Info("Event created, createeventviewmodel");
      TryClose(true);
    }

    public void CancelButton()
    {
      TryClose(false);
    }



  }
}
