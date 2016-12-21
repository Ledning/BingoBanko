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
  internal class PresentationScreenHostViewModel : Conductor<IPresentationScreenItem>.Collection.OneActive
  {
    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));
    private BingoEvent _event;
    private int _left;
    private int _top;
    private int wHeight;

    private int wWidth;

    public PresentationScreenHostViewModel()
    {
      Event = IoC.Get<BingoEvent>();

    }

    #region Overrides of ViewAware

    protected override void OnViewReady(object view)
    {
      WWidth = 300;
      WHeight = 300;
      Left = (int)Event.Settings.Screens[GetPresentationScreen()].WorkingArea.Left;
      Top = (int)Event.Settings.Screens[GetPresentationScreen()].WorkingArea.Top;
      ActivateItem(new NumberBarViewModel());
    }

    #endregion

    public int WWidth
    {
      get { return wWidth; }

      set
      {
        wWidth = value;
        NotifyOfPropertyChange(() => WWidth);
      }
    }
    public int WHeight
    {
      get { return wHeight; }

      set
      {
        wHeight = value;
        NotifyOfPropertyChange(() => WHeight);
      }
    }
    public int Top
    {
      get { return _top; }

      set
      {
        _top = value;
        NotifyOfPropertyChange(() => Top);
      }
    }
    public int Left
    {
      get
      {
        return _left;
      }

      set
      {
        _left = value;
        NotifyOfPropertyChange(() => Left);
      }
    }
    public BingoEvent Event
    {
      get { return _event; }

      set
      {
        _event = value;
        NotifyOfPropertyChange(() => Event);
      }
    }


    public int GetPresentationScreen()
    {
      var screenNr = 0;
      for (; screenNr < Event.Settings.Screens.Count; screenNr++)
        if (!Event.Settings.Screens[screenNr].Primary)
          return screenNr;
      return -1; //Error no other screen than the primary was found. 
    }

    public void ShowFullscreenImage()
    {
      //ActivateItem();
    }
  }
}