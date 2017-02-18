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
    private readonly ILog _log = LogManager.GetLog(typeof(PresentationScreenHostViewModel));
    private BingoEvent _event;
    private IPresentationScreenItem _currentPrezItem = new FullscreenImageViewModel();

    public PresentationScreenHostViewModel()
    {

    }

    #region Overrides of ViewAware

    protected override void OnViewReady(object view)
    {
      Event = IoC.Get<BingoEvent>();
      Event.WindowSettings.PrsSettings.Width = (int)Event.WindowSettings.Screens[1].WorkingArea.Width;
      Event.WindowSettings.PrsSettings.Height = (int)Event.WindowSettings.Screens[1].WorkingArea.Height;

      Event.WindowSettings.PrsSettings.Left = (int)Event.WindowSettings.Screens[1].WorkingArea.Left;
      Event.WindowSettings.PrsSettings.Top = (int)Event.WindowSettings.Screens[1].WorkingArea.Top;
      Event.WindowSettings.PrsSettings.State = WindowState.Maximized;
      CurrentPrezItem = new FullscreenImageViewModel();
      ActivateItem(CurrentPrezItem);
    }

    #endregion

  
    public BingoEvent Event
    {
      get { return _event; }

      set
      {
        _event = value;
        NotifyOfPropertyChange(() => Event);
      }
    }

    public IPresentationScreenItem CurrentPrezItem
    {
      get { return _currentPrezItem; }
      set { _currentPrezItem = value; ActivateItem(CurrentPrezItem);  NotifyOfPropertyChange(()=>CurrentPrezItem);}
    }


    public int GetPresentationScreen()
    {
      var screenNr = 0;
      for (; screenNr < Event.WindowSettings.Screens.Count; screenNr++)
        if (!Event.WindowSettings.Screens[screenNr].Primary)
          return screenNr;
      _log.Warn("No Presentation screen was found. ERROR");
      return -1; 
    }

    #region Implementation of IHandle<CommunicationObject>

    public void Handle(CommunicationObject message)
    {
      if (message.Message == ApplicationWideEnums.MessageTypes.FullscreenOverlay)
      {
        ActivateItem(new FullscreenImageViewModel());
        _log.Info("fullscrnOLhandled");
      }
      else if (message.Message == ApplicationWideEnums.MessageTypes.BoardOverview)
      {
        ActivateItem(new PlateOverlayViewModel());
        _log.Info("BoardOLhandled");
      }
      else if (message.Message == ApplicationWideEnums.MessageTypes.LatestNumbers)
      {
        ActivateItem(new NumberBarViewModel());
        _log.Info("latestnumolhandled");
      }
      else if (message.Message == ApplicationWideEnums.MessageTypes.BingoHappened)
      {
        ActivateItem(new BingoScreenViewModel());
          _log.Info("bingohapndOLHandled");
      }
      else if (message.Message == ApplicationWideEnums.MessageTypes.ClosePrez)
      {
          TryClose();
        _log.Info("Prez closed.");
      }
      else
        _log.Info("Not handled by PresentationScreen.");
    }

    #endregion
  }
}