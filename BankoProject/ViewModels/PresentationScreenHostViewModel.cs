using BankoProject.Models;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels
{
  /// <summary>
  /// This is where you would set up all the shit, so when this is put up, the rest follows.
  /// </summary>
  class PresentationScreenHostViewModel : Conductor<IPresentationScreenItem>.Collection.OneActive
  {
        BingoEvent _event;
        private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));
        public PresentationScreenHostViewModel()
    {
            Event = IoC.Get<BingoEvent>();
            WWidth = 100;
            WHeight = 300;
            Left = (int)Event.Settings.Screens[GetPresentationScreen()].WorkingArea.Left;
            Top = (int)Event.Settings.Screens[GetPresentationScreen()].WorkingArea.Top;
        }

        public int GetPresentationScreen()
        {
            int screenNr = 0;
            for (; screenNr < Event.Settings.Screens.Count; screenNr++)
            {
                if (!Event.Settings.Screens[screenNr].Primary)
                {
                    return screenNr;
                }
            }
                return -1; //Error no other screen than the primary was found. 
        }

        int wWidth;
        int wHeight;
        int _top;
        int _left;

        public int WWidth
        {
            get
            {
                return wWidth;
            }

            set
            {
                wWidth = value; NotifyOfPropertyChange(() => WWidth);
            }
        }

        public int WHeight
        {
            get
            {
                return wHeight;
            }

            set
            {
                wHeight = value; NotifyOfPropertyChange(() => WHeight);
            }
        }

        public int Top
        {
            get
            {
                return _top;
            }

            set
            {
                _top = value; NotifyOfPropertyChange(() => Top);
            }
        }

        public int Left
        {
            get
            {
                _log.Info("||||||||||||||||||||||||||||LEFTTRIGG||||||||||||||||||||"); return _left; 
            }

            set
            {
                _left = value; NotifyOfPropertyChange(() => Left); 
            }
        }

        public BingoEvent Event
        {
            get
            {
                return _event;
            }

            set
            {
                _event = value; NotifyOfPropertyChange(() => Event);
            }
        }

        public void ShowFullscreenImage()
    {
      //ActivateItem();
    }



  }
}
