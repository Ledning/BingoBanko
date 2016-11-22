using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using BankoProject.Tools.Events;
using BankoProject.ViewModels.PresentationScreen;

namespace BankoProject.ViewModels
{
  class ControlPanelViewModel : Conductor<IMainViewItem>.Collection.OneActive, IMainViewItem
  {
    private IEventAggregator _events;
    private BingoEvent _bingoEvent;
    private IWindowManager _winMan;
    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));
    private BoardViewModel _boardVm;
    private string _plateNumberTextBox;
    private string _contestName;
    private int _numberOfContestants;
    private int _numberOfTeams;
    private int _contestDuration;


    public ControlPanelViewModel()
    {
      BoardVM = new BoardViewModel();
      ActivateItem(BoardVM);
    }

    protected override void OnViewReady(object view)
    {
      _events = IoC.Get<IEventAggregator>();
      _winMan = IoC.Get<IWindowManager>();
      Event = IoC.Get<BingoEvent>();
      Event.BnkOptions.SingleRow = true;
      Event.VsOptions.EmptyScreen = true;

    }


    public void ShowWelcome()
    {
       PresentationScreenHostViewModel przscrnvm = new PresentationScreenHostViewModel();
       _winMan.ShowWindow(przscrnvm);
    //_events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.ChngWelcomeView, ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set { _bingoEvent = value; NotifyOfPropertyChange(() => Event); }
    }

    public BoardViewModel BoardVM
    {
      get { return _boardVm; }
      set { _boardVm = value; NotifyOfPropertyChange(()=> BoardVM);}
    }

    public string PlateNumberTextBox
    {
      get { return _plateNumberTextBox; }
      set { _plateNumberTextBox = value; NotifyOfPropertyChange(() => PlateNumberTextBox);}
    }

    public string ContestName
    {
      get { return _contestName; }
      set { _contestName = value; NotifyOfPropertyChange(() => ContestName);}
    }

    public int NumberOfContestants
    {
      get { return _numberOfContestants; }
      set { _numberOfContestants = value; NotifyOfPropertyChange(() => NumberOfContestants);}
    }

    public int NumberOfTeams
    {
      get { return _numberOfTeams; }
      set { _numberOfTeams = value; NotifyOfPropertyChange(() => NumberOfTeams);}
    }

    public int ContestDuration
    {
      get { return _contestDuration; }
      set { _contestDuration = value; NotifyOfPropertyChange(() => ContestDuration);}
    }

    public void ShowLatestNumbers()
    {
      throw new NotImplementedException();
    }

    public void BingoButton()
    {
      throw new NotImplementedException();
    }

    public void DrawRandom()
    {
      throw new NotImplementedException();
    }

    public void AddSelectedNumbers()
    {
      throw new NotImplementedException();
    }

    public void CheckPlateButton()
    {
      throw new NotImplementedException();
    }

    public void AddTeamButton()
    {
      throw new NotImplementedException();
    }

    public void StartContest()
    {
      throw new NotImplementedException();
    }





  }
}
