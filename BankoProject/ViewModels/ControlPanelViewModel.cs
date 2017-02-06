using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using BankoProject.Tools.Events;
using BankoProject.ViewModels.PresentationScreen;
using Catel.Collections;

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
      //_events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.RbPrezScreen, ApplicationWideEnums.SenderTypes.ControlPanelView));
    }


    public void ShowWelcome()
    {

      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.ChngWelcomeView,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public void SpawnPrezScreen()
    {
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.RbPrezScreen, ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    #region props
    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set
      {
        _bingoEvent = value;
        NotifyOfPropertyChange(() => Event);
      }
    }

    public BoardViewModel BoardVM
    {
      get { return _boardVm; }
      set
      {
        _boardVm = value;
        NotifyOfPropertyChange(() => BoardVM);
      }
    }

    public string PlateNumberTextBox
    {
      get { return _plateNumberTextBox; }
      set
      {
        _plateNumberTextBox = value;
        NotifyOfPropertyChange(() => PlateNumberTextBox);
      }
    }

    public string ContestName
    {
      get { return _contestName; }
      set
      {
        _contestName = value;
        NotifyOfPropertyChange(() => ContestName);
      }
    }

    public int NumberOfContestants
    {
      get { return _numberOfContestants; }
      set
      {
        _numberOfContestants = value;
        NotifyOfPropertyChange(() => NumberOfContestants);
      }
    }

    public int NumberOfTeams
    {
      get { return _numberOfTeams; }
      set
      {
        _numberOfTeams = value;
        NotifyOfPropertyChange(() => NumberOfTeams);
      }
    }

    public int ContestDuration
    {
      get { return _contestDuration; }
      set
      {
        _contestDuration = value;
        NotifyOfPropertyChange(() => ContestDuration);
      }
    }

    private ObservableCollection<Team> _allTeams; 
    public ObservableCollection<Team> AllTeams { get { return _allTeams; } set { _allTeams = value; NotifyOfPropertyChange(()=> AllTeams);} }
#endregion

    public void ShowLatestNumbers()
    {
      throw new NotImplementedException();
    }

    public void BingoButton()
    {
      throw new NotImplementedException();
    }

    //this method gets a random number and marks the boardview, that that number is now marked
    public int DrawRandom()
    {
      //TODO: Lav et eller andet check så man ikke kan trække hvis der er få tal tilbage 
      Random rdn = new Random();

      int rdnnumber = rdn.Next(0, 89);
      while (true)
      {
        if (this.Event.NumberBoard.Board[rdnnumber].IsPicked == false)
        {
          this.Event.NumberBoard.Board[rdnnumber].IsPicked = true;
          Event.NumberBoard.Board[rdnnumber].IsSelected = true;
          return 1;
        }
        rdnnumber = rdn.Next(0, 89);
      }
      return 0;
    }

    public void AddSelectedNumbers()
    {
      //TODO: Make the numbers enter into a secondary queue, so that they might be animated
      foreach (BingoNumber bnum in Event.NumberBoard.Board)
      {
        if (bnum.IsSelected)
        {
          if (!bnum.IsPicked)
          {
            bnum.IsPicked = true;
          }
        }
      }
    }

    public void AddNumber()
    {
      bool? result = _winMan.ShowDialog(new dialogViewModel("Indtast tal...."));
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
      CompetitionObject competition = new CompetitionObject(this.NumberOfContestants, this.NumberOfTeams, this.ContestDuration);

      this.AllTeams = new ObservableCollection<Team>(competition.AllTeams);
      
      //this should prolly also start some counter somewhere
    }
  }
}