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

    //TODO: Make this window a totally borderless window
    /*Den skal være helt kantløs, og man behøver ikke kunne flytte den. 
     * Den bør ikke låse musen til programvinduet, bare man ikke kan klikke andet i programmet.
     * Derudover skal det overvejes om der skal være inputrestriktion i dette vindue  */

    //TODO: Husk at lave vinduet om så alle tingene er synlige

    //TODO: LAv Skærmindstillinger dropdownen
    //Den kan bare bruge en enum der betegner alle de forskellige normale aspect ratios, lav enumen i ApplicationWideEnums, så vi kan reffe den otherwhere
    //Det valgte skal så være bundet til en prop på BingoEvent objektet, under PresentationScreen objektet, aka lissom begivenhedsnavnet eller whatev

    //TODO: Make this window great again
    //det ligner lidt lårt gør det ikk? Make it better or summin, idunno

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
      Event.Initialize(PhSeed, PhTitle, PhPladetal); //skal fjernes når vi kan åbne et event rigtigt
      _log.Info("Event created, createeventviewmodel");
      TryClose(true);
    }

    public void CancelButton()
    {
      TryClose(false);
    }



  }
}
