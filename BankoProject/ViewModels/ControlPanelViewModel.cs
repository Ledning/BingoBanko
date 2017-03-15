﻿using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BankoProject.Models;
using BankoProject.Tools.Events;
using BankoProject.ViewModels.ConfirmationBoxes;
using BankoProject.ViewModels.PresentationScreen;
using Catel.Collections;

namespace BankoProject.ViewModels
{
  class ControlPanelViewModel : Conductor<IMainViewItem>.Collection.OneActive, IMainViewItem, IDataErrorInfo
  {
    #region fields

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
    private string _error;
    private string _text;
    private int _plateToCheck;

    #endregion

    //TODO: Overlay Lbael på view skal fixes - det har en weird kant

    #region Constructors

    public ControlPanelViewModel()
    {
      BoardVM = new BoardViewModel();
      ActivateItem(BoardVM);

      this.StartValue = 0;
    }

    #endregion

    #region Overrides of ViewAware

    protected override void OnViewReady(object view)
    {
      _events = IoC.Get<IEventAggregator>();
      _winMan = IoC.Get<IWindowManager>();
      Event = IoC.Get<BingoEvent>();
      Event.BnkOptions.SingleRow = true;
      //TODO: Fix this so these options are taken care of in bingoevent or in winsettings
      przScrnDelay();
      Event.WindowSettings.PrsSettings.OverlaySettings.StdScrnOl = true;
      Event.WindowSettings.PrsSettings.OverlaySettings.UpdateBackgrounds();
      StartValue = 1;
      Event.WindowSettings.PrsSettings.DockingPlace = Dock.Bottom;
      if (File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                      "\\BingoBankoKontrol\\" +
                      Event.EventTitle + "Plader.pdf"))
      {
        _log.Info("Plates has already been generated.");
        Event.PInfo.HasPlatesBeenGenerated = true;
      }
    }

    #endregion

    #region Properties

    public string PlateToCheckText
    {
      get { return _text; }
      set
      {
        _text = value;
        NotifyOfPropertyChange(() => PlateToCheckText);

      }
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

    private int _startValue;

    public int StartValue
    {
      get { return _startValue; }
      set
      {
        _startValue = value;
        NotifyOfPropertyChange(() => StartValue);
      }
    }

    public CompetitionObject Competition { get; set; }

    private BindableCollection<Team> _allTeams;

    public BindableCollection<Team> AllTeams
    {
      get { return _allTeams; }
      set
      {
        _allTeams = value;
        NotifyOfPropertyChange(() => AllTeams);
        NotifyOfPropertyChange(() => CanStartContest);
      }
    }

    #endregion

    #region Async stuff

    public void SpawnPrezScreen()
    {
      if (Event.WindowSettings.PrsSettings.IsOverLayOpen) return;
      _winMan.ShowWindow(new PresentationScreenHostViewModel());
      Event.WindowSettings.PrsSettings.IsOverLayOpen = true;
    }

    public async Task przScrnDelay()
    {
      await Task.Delay(1000);
      SpawnPrezScreen();
    }

    #endregion

    #region Methods

    public void ToggleTimer()
    {
      Event.TimeOpt.ToggleTimer();
    }


    public void ShowWelcome()
    {
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.ChngWelcomeView,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }



    //this method gets a random number and marks the boardview, that that number is now marked
    public void DrawRandom()
    {
      Random rdn = new Random();
      int rdnnumber = rdn.Next(0, Event.AvailableNumbersQueue.Count);

      if (Event.AvailableNumbersQueue.Count > 0)
      {
        if (!Event.NumberBoard.Board[Event.AvailableNumbersQueue[rdnnumber].Value - 1].IsPicked)
        {
          try
          {
            _log.Info(Event.NumberBoard.Board[Event.AvailableNumbersQueue[rdnnumber].Value - 1].Value.ToString());
            Event.NumberBoard.Board[Event.AvailableNumbersQueue[rdnnumber].Value - 1].IsPicked = true;
            Event.NumberBoard.Board[Event.AvailableNumbersQueue[rdnnumber].Value - 1].IsChecked = false;
            Event.AvailableNumbersQueue.Remove(Event.AvailableNumbersQueue[rdnnumber]);
            Event.BingoNumberQueue.Add(Event.NumberBoard.Board[Event.AvailableNumbersQueue[rdnnumber].Value]);
          }
          catch (Exception ex)
          {
            _log.Info(rdnnumber.ToString());
            _log.Info(Event.AvailableNumbersQueue.Count.ToString());
            _log.Info("Exception in random numb!");
          }

          return;
        }
        _log.Info("This should not happen");
      }
    }

    public void AddSelectedNumbers()
    {
      //TODO: Make the numbers enter into a secondary queue, so that they might be animated
      foreach (BingoNumber bnum in Event.NumberBoard.Board)
      {
        if (bnum.IsChecked)
        {
          if (!bnum.IsPicked)
          {
            bnum.IsPicked = true;
            bnum.IsChecked = false;
            Event.BingoNumberQueue.Add(bnum);
            Event.AvailableNumbersQueue.Remove(bnum);
          }
          else if (bnum.IsPicked)
          {
            bnum.IsPicked = false;
            bnum.IsChecked = false;
            Event.BingoNumberQueue.Remove(bnum);
            Event.AvailableNumbersQueue.Add(bnum);
          }
        }
      }
    }

    public void AddNumber()
    {
      var dialog = new AddNumberViewModel();

      var result = _winMan.ShowDialog(dialog);
      if (result == true)
      {
        if (!Event.NumberBoard.Board[dialog.NumberToAdd - 1].IsPicked)
        {
          Event.NumberBoard.Board[dialog.NumberToAdd - 1].IsPicked = true; //minus one to make it fit the array lol
          Event.NumberBoard.Board[dialog.NumberToAdd - 1].IsChecked = false;
          Event.BingoNumberQueue.Add(Event.NumberBoard.Board[dialog.NumberToAdd - 1]);
        }

      }
    }

    public void CheckPlate()
    {
      int[,] chosenPlate = Event.PInfo.CardList[_plateToCheck];
      int rules;
      bool rowFailed = false;
      int winRows = 0;

      if (Event.BnkOptions.SingleRow)
        rules = 1;
      else if (Event.BnkOptions.DoubleRow)
        rules = 2;
      else if (Event.BnkOptions.FullPlate)
        rules = 3;
      else
      {
        _log.Info("No rules. This should not happen");
        return;
      }

      for (int rows = 0; rows < 3; rows++)
      {
        rowFailed = false;

        for (int columns = 0; columns < 9; columns++)
        {
          if (chosenPlate[rows, columns] != 0 || chosenPlate[rows,columns] != Event.BingoNumberQueue[chosenPlate[rows, columns]].Value)
          {
            rowFailed = true;
            break;
          }
        }

        if (!rowFailed)
          winRows++;
      }

      if (winRows >= rules)
      {
        /*WIR HABEN BINGO MOTHERFUCKERS!!!*/
      }
      else
      {
        /*Sad face :( */
      }
    }

    public bool CanCheckPlate
    {
      get
      {
        if (Event.PInfo.CardList != null)
        {
          if (_plateToCheck == -1)
          {
            return false;
          }
          return true;
        }
        return false;
      }
      //This is just a rudimentary check to see if the plates has been generated

    }

    public void AddTeamButton()
    {
      CompetitionObject competition = new CompetitionObject(this.NumberOfContestants, this.NumberOfTeams,
        this.ContestDuration, this.StartValue);
      this.AllTeams = new BindableCollection<Team>(competition.AllTeams);

      this.Competition = competition;
    }

    public void StartContest()
    {
      _winMan.ShowWindow(new CountdownTimerControlViewModel(this.AllTeams, this.ContestDuration));


      // When this method is activated we can either change the view, or keep everything within
      //current view. Which is best?
    }

    public bool CanStartContest
    {
      get
      {
        if (this.AllTeams == null || this.AllTeams.Count == 0)
        {
          return false; //Signifies that no teams has been made
        }
        return true;
      }
    }

    #endregion

    #region PresentationActivation

    public void ActivateFullscreenOverlay()
    {
      if (!Event.WindowSettings.PrsSettings.IsOverLayOpen)
      {
        SpawnPrezScreen();
      }
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.FullscreenOverlay,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
      Event.WindowSettings.PrsSettings.OverlaySettings.IsOverlayVisible = Visibility.Visible;
    }

    public void ActivateLatestNumbersOverlay()
    {
      if (!Event.WindowSettings.PrsSettings.IsOverLayOpen)
      {
        SpawnPrezScreen();
      }
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.LatestNumbers,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public void ActivatePlateOverviewOverlay()
    {
      if (!Event.WindowSettings.PrsSettings.IsOverLayOpen)
      {
        SpawnPrezScreen();
      }
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.BoardOverview,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public void ActivateBingoHappenedOverlay()
    {
      if (!Event.WindowSettings.PrsSettings.IsOverLayOpen)
      {
        SpawnPrezScreen();
      }
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.BingoHappened,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public void ConfirmFullscreenOverlayChange()
    {
      Event.WindowSettings.PrsSettings.OverlaySettings.ScrnActivationRequired = true;
    }

    public void ActivateBlankOverlay()
    {
      if (!Event.WindowSettings.PrsSettings.IsOverLayOpen)
      {
        SpawnPrezScreen();
      }
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.FullscreenOverlayBlank,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    public void ActivateStopWatch()
    {
      _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.Stopwatch,
        ApplicationWideEnums.SenderTypes.ControlPanelView));
    }

    #endregion

    #region ScreenCollectionUpdate

    #endregion

    #region ResetStuff

    public void Reset()
    {
      #region attempt that saves event and stuff

      ////Save the current event, with a different ending
      ////if it was named bingo2017, save a file called bingo2017RESET[TIMESTAMP]
      //_log.Info("Saving event before reset...");
      //_events.PublishOnBackgroundThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.Save,
      //  ApplicationWideEnums.SenderTypes.ControlPanelView));
      //BingoEvent resetEv = new BingoEvent();
      ////The following block is almost excatly like the method CopyTo in MainWindowVM, different in the fact that it does not copy over number positions or queues or anything. 
      //resetEv.Initialize(Event.SInfo.Seed, Event.EventTitle, Event.PInfo.PlatesGenerated, DateTime.Now);
      //resetEv.WindowSettings = Event.WindowSettings;
      //resetEv.PInfo = Event.PInfo;
      //resetEv.SInfo = Event.SInfo;
      ////create a new object, that is the same except for the following:
      ////empty competitionlist
      ////empty numberqueue, reset numberboard
      ////reset singlerow/doublerow/plate to singlerow
      //CopyEvent(resetEv, Event);
      //_log.Info("Reset Done");
      ////TODO: Somebody else needs to check up on if this actually copies it all over correctly and resets the correct thingies
      ////ask kris if summin is missin, maybe theres a stupid ass reason for it

      #endregion

      foreach (BingoNumber bnum in Event.NumberBoard.Board)
      {
        bnum.IsPicked = false;
        bnum.IsChecked = false;
      }

      Event.BingoNumberQueue = new BindableCollection<BingoNumber>();
      Event.AvailableNumbersQueue = new BindableCollection<BingoNumber>();
      for (int i = 1; i <= 90; i++)
      {
        BingoNumber j = new BingoNumber();
        j.Value = i;
        Event.AvailableNumbersQueue.Add(j);
      }

      CompetitionObject competition = new CompetitionObject(0, 0,
        0, 1);
      this.AllTeams = new BindableCollection<Team>(competition.AllTeams);
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

    #region Stopwatch

    private bool _isStopwatchRunning;

    #endregion

    #region Implementation of IDataErrorInfo

    public string this[string columnName]
    {
      get
      {

        string result = null;
        if (columnName == "NumberOfTeams")
        {

        }
        if (columnName=="NumberOfContestants")
        {
          
        }
        if (columnName == "ContestDuration")
        {

        }
        if (columnName == "PlateToCheckText")
        {
          if (PlateToCheckText != null && !PlateToCheckText.Contains("-") && !PlateToCheckText.Contains("-"))
          {
            int? resultInt = null;
            try
            {
              resultInt = int.Parse(PlateToCheckText);
            }
            catch (Exception)
            {

              result = "This is not a correct plate number.";
              _plateToCheck = -1;

            }
            if (resultInt != null)
            {
              if (Event.PInfo.CardList != null)
              {
                if (resultInt >= Event.PInfo.CardList.Count)
                {
                  result = "That number is not within the valid plate-range.";
                  _plateToCheck = -1;
                }
              }
              else
              {
                result = "Cardlist is null!";
                _plateToCheck = -1;
              }
            }


          }
          else
          {
            result = "This is not a valid input number.";
            _plateToCheck = -1;
          }

          if (result == null)
          {
            _plateToCheck = int.Parse(PlateToCheckText);
            NotifyOfPropertyChange(() => CanCheckPlate);
          }
        }
        return result;
      }
    }

    public string Error
    {
      get { return _error; }
    }



    #endregion
  }
}