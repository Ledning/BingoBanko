using System.Windows;
using System.Windows.Media;
using BankoProject.Models;
using BankoProject.Tools;
using BankoProject.ViewModels.PresentationScreen;
using Caliburn.Micro;

namespace BankoProject.ViewModels
{
  /// <summary>
  ///   This is where you would set up all the shit, so when this is put up, the rest follows.
  /// </summary>
  class PresentationScreenHostViewModel : Conductor<IPresentationScreenItem>.Collection.OneActive
  {
    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));
    private BingoEvent _event;
    private IPresentationScreenItem _currentPrezItem;

    public PresentationScreenHostViewModel()
    {
      Event = IoC.Get<BingoEvent>();
    }

    #region Overrides of ViewAware

    protected override void OnViewReady(object view)
    {
      Event.WindowSettings.PrsSettings.Width = (int)Event.WindowSettings.Screens[1].WorkingArea.Width;
      Event.WindowSettings.PrsSettings.Height = (int)Event.WindowSettings.Screens[1].WorkingArea.Height;

      Event.WindowSettings.PrsSettings.Left = (int)Event.WindowSettings.Screens[1].WorkingArea.Left;
      Event.WindowSettings.PrsSettings.Top = (int)Event.WindowSettings.Screens[1].WorkingArea.Top;
      Event.WindowSettings.PrsSettings.State = WindowState.Maximized;
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
  }
}