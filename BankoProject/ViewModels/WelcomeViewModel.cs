using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BankoProject.Models;
using BankoProject.Tools.Events;
using BankoProject.ViewModels.PresentationScreen;

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
      //assign dummy data to LatestEvents
      
      Random rdn = new Random();
      BingoEvent bingoevent = new BingoEvent();
      LatestEvents = new BindableCollection<BingoEvent>();

      for (int i = 0; i < 5; i++)
      {
        bingoevent.Initialize("something"+rdn.Next(500).ToString(), "SuperEVENT"+rdn.Next(500).ToString(), rdn.Next(5));
        this.LatestEvents.Add(bingoevent);
      }
      
      Title = "PLACEHOLDER";

    }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set { _bingoEvent = value; NotifyOfPropertyChange(()=>Event);}
    }

    private string _title;
    public string Title
    {
      get { return _title; }
      set { _title = value; NotifyOfPropertyChange(() => Title); }
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
          _events.PublishOnBackgroundThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.Save, ApplicationWideEnums.SenderTypes.WelcomeView));
          _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.ChngControlPanelView, ApplicationWideEnums.SenderTypes.WelcomeView));
        }
      }
    }
    public BindableCollection<BingoEvent> LatestEvents { get; set; }


    //TODO: En collection som Seneste Events kan binde til
    //Navneboksen for Seneste Events er slightly unaligned

    //TODO: Titlen i Welcomeview skal bindes til en string prop

    //TODO: Ordentlig boks til titlen i WelcomewView.
    //Den ligner sku en sæk lort lige pt





    public void OpenFileDialog()
    {
      var ofd = new Microsoft.Win32.OpenFileDialog()
      {
        Filter = "Event filer (*.bingoEvent)|*bingoEvent"
      };

      if (ofd.ShowDialog() ?? false)
      {
        var d = ofd.FileName;
      }
    }
  }
}
