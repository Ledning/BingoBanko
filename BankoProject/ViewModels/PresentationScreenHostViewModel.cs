using System;
using System.Windows;
using System.Windows.Media;
using BankoProject.Models;
using BankoProject.Tools;
using BankoProject.Tools.Events;
using BankoProject.ViewModels.PresentationScreen;
using Caliburn.Micro;

namespace BankoProject.ViewModels
{
  /// <summary>
  ///   This is where you would set up all the shit, so when this is put up, the rest follows.
  /// </summary>
  class PresentationScreenHostViewModel : Conductor<IPresentationScreenItem>.Collection.OneActive, IHandle<CommunicationObject>
  {
    #region Fields
    private readonly ILog _log = LogManager.GetLog(typeof(PresentationScreenHostViewModel));
    private BingoEvent _event;
    private IEventAggregator _eventAgg;
    private IPresentationScreenItem _currentPrezItem = new FullscreenImageViewModel();
    #endregion

    #region Constructors
    public PresentationScreenHostViewModel()
    {
    } 
    #endregion

    #region Properties
    public BingoEvent Event
    {
      get { return _event; }
      set { _event = value; NotifyOfPropertyChange(() => Event); }
    }

    public IPresentationScreenItem CurrentPrezItem
    {
      get { return _currentPrezItem; }
      set
      {
        _currentPrezItem = value;
        ActivateItem(CurrentPrezItem);
        NotifyOfPropertyChange(() => CurrentPrezItem);
      }
    }
    #endregion

    #region Overrides of ViewAware

    protected override void OnViewReady(object view)
    {
      Event = IoC.Get<BingoEvent>();
      _eventAgg = IoC.Get<IEventAggregator>();
      _eventAgg.Subscribe(this);
      Event.WindowSettings.PrsSettings.Width = (int)Event.WindowSettings.Screens[Event.WindowSettings.ChoosenPresentationScreen].WorkingArea.Width;
      Event.WindowSettings.PrsSettings.Height = (int)Event.WindowSettings.Screens[Event.WindowSettings.ChoosenPresentationScreen].WorkingArea.Height;

      Event.WindowSettings.PrsSettings.Left = (int)Event.WindowSettings.Screens[Event.WindowSettings.ChoosenPresentationScreen].WorkingArea.Left;
      Event.WindowSettings.PrsSettings.Top = (int)Event.WindowSettings.Screens[Event.WindowSettings.ChoosenPresentationScreen].WorkingArea.Top;
      Event.WindowSettings.PrsSettings.State = WindowState.Maximized;
      CurrentPrezItem = new FullscreenImageViewModel();
      ActivateItem(CurrentPrezItem);
    }

    #endregion

    #region Methods
    public int GetPresentationScreen()
    {
      //TODO: Det her skal laves om så man kan vælge en specifik skærm fra en liste
      var screenNr = 0;
      for (; screenNr < Event.WindowSettings.Screens.Count; screenNr++)
        if (!Event.WindowSettings.Screens[screenNr].Primary)
          return screenNr;
      _log.Warn("No Presentation screen was found. ERROR");
      return -1;
    } 
    #endregion

    #region Implementation of IHandle<CommunicationObject>

    public void Handle(CommunicationObject message)
    {
      switch (message.Message)
      {
        case ApplicationWideEnums.MessageTypes.FullscreenOverlay:
          ActivateItem(new FullscreenImageViewModel());
          _log.Info("fullscrnOLhandled");
          break;
        case ApplicationWideEnums.MessageTypes.BoardOverview:
          ActivateItem(new PlateOverlayViewModel());
          _log.Info("BoardOLhandled");
          break;
        case ApplicationWideEnums.MessageTypes.LatestNumbers:
          ActivateItem(new NumberBarViewModel());
          _log.Info("latestnumolhandled");
          break;
        case ApplicationWideEnums.MessageTypes.BingoHappened:
          ActivateItem(new BingoScreenViewModel());
          _log.Info("bingohapndOLHandled");
          break;
        case ApplicationWideEnums.MessageTypes.ClosePrez:
          TryClose();
          _log.Info("Prez closed.");
          break;
        case ApplicationWideEnums.MessageTypes.FullscreenOverlayBlank:
          ActivateItem(new FullscreenImageViewModel(true));
          break;
      }
    }

    #endregion
  }
}