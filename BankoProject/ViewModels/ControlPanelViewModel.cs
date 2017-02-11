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


    //TODO: Overlay Lbael på view skal fixes - det har en weird kant

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
      //TODO: Fix this so these options are taken care of in bingoevent or in winsettings
      //Event.VsOptions.EmptyScreen = true;
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.RbPrezScreen, ApplicationWideEnums.SenderTypes.ControlPanelView));
    }


    public void ShowWelcome()
    {
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.ChngWelcomeView,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public void SpawnPrezScreen()
    {
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.RbPrezScreen,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
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

    public ObservableCollection<Team> AllTeams
    {
      get { return _allTeams; }
      set
      {
        _allTeams = value;
        NotifyOfPropertyChange(() => AllTeams);
      }
    }

    #endregion

    public void ShowLatestNumbers()
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


    public void CheckPlateButton()
    {
      //OO this one is...intredasting
      //TODO: implement lol
      throw new NotImplementedException();
    }

    public void AddTeamButton()
    {
      throw new NotImplementedException();
    }

    public void StartContest()
    {
      CompetitionObject competition = new CompetitionObject(this.NumberOfContestants, this.NumberOfTeams,
        this.ContestDuration);

      this.AllTeams = new ObservableCollection<Team>(competition.AllTeams);

      //this should prolly also start some counter somewhere
    }

    #region PresentationActivation
    public void ActivateFullscreenOverlay()
    {
      _events.PublishOnBackgroundThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.FullscreenOverlay,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public void ActivateLatestNumbersOverlay()
    {
      _events.PublishOnBackgroundThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.LatestNumbers,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public void ActivatePlateOverviewOverlay()
    {
      _events.PublishOnBackgroundThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.BoardOverview,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public void ActivateBingoHappenedOverlay()
    {
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.BingoHappened, ApplicationWideEnums.SenderTypes.ControlPanelView));
    }
    public void ConfirmFullscreenOverlayChange()
    {
      throw new NotImplementedException();
      //TODO: til den lille aktiveringsknap, hensigten er at man klikke rpå den når man vil ændre ift hvad der står i de 3 radiobuttons
    }


    #endregion


    #region ResetStuff

    public void Reset()
    {
      //Save the current event, with a different ending
      //if it was named bingo2017, save a file called bingo2017RESET[TIMESTAMP]
      _log.Info("Saving event before reset...");
      _events.PublishOnBackgroundThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.Save,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
      BingoEvent resetEv = new BingoEvent();
      //The following block is almost excatly like the method CopyTo in MainWindowVM, different in the fact that it does not copy over number positions or queues or anything. 
      resetEv.Initialize(Event.SInfo.Seed, Event.EventTitle, Event.PInfo.PlatesGenerated, DateTime.Now);
      resetEv.WindowSettings = Event.WindowSettings;
      resetEv.PInfo = Event.PInfo;
      resetEv.SInfo = Event.SInfo;
      //create a new object, that is the same except for the following:
      //empty competitionlist
      //empty numberqueue, reset numberboard
      //reset singlerow/doublerow/plate to singlerow
      CopyEvent(resetEv, Event);
      _log.Info("Reset Done");
      //TODO: Somebody else needs to check up on if this actually copies it all over correctly and resets the correct thingies
      //ask kris if summin is missin, maybe theres a stupid ass reason for it
    }

    private void CopyEvent(BingoEvent fr, BingoEvent to)
    {
      to.Initialize(fr.SInfo.Seed, fr.EventTitle, fr.PInfo.PlatesGenerated);
      to.BingoNumberQueue = fr.BingoNumberQueue;
      to.BnkOptions = fr.BnkOptions;
      to.CmpOptions = fr.CmpOptions;
      to.CompetitionList = fr.CompetitionList;
      to.EventTitle = fr.EventTitle;
      to.Generating = fr.Generating;
      to.NumberBoard = fr.NumberBoard;
      to.PInfo = fr.PInfo;
      to.RecentFiles = fr.RecentFiles;
      to.WindowSettings = fr.WindowSettings;
      to.SInfo = fr.SInfo;
      //to.VsOptions = fr.VsOptions;
    }

    #endregion

    //todo: somebody for the love of christ make applicationwideenums a shortcut in the files it is use din jeezus i get cancer
    //TODO: Maybe we should consider having a superuser mode, where  there is no confirmationboxes? and shit
    //Could be done with just a single bool on the bingoEvent object. 
  }
}