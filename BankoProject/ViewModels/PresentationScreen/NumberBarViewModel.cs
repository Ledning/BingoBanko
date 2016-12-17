using BankoProject.Tools;
using BankoProject.Models;
using Caliburn.Micro;

namespace BankoProject.ViewModels.PresentationScreen
{
  class NumberBarViewModel : Screen, IPresentationScreenItem
  {
    private IWindowManager _winMan;
    private IEventAggregator _events;
    private BingoEvent _bingoEvent;
    private readonly ILog _log = LogManager.GetLog(typeof(WelcomeViewModel));


    public ApplicationWideEnums.AspectRatio AsRatio { get; set; }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set { _bingoEvent = value; NotifyOfPropertyChange(() => Event); }
    }

    protected override void OnViewReady(object view)
    {
      _winMan = IoC.Get<IWindowManager>();
      _events = IoC.Get<IEventAggregator>();
      _bingoEvent = IoC.Get<BingoEvent>();
    }
  }
}
