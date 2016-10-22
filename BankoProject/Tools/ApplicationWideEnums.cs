using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Tools
{
  class ApplicationWideEnums
  {

    public enum MessageTypes
    {
      ChngWelcomeView,
      ChngControlPanelView,
      ChngEventSelectedView,
      ChngMainWindowView,
      Save,
      Load
    }

    public enum SenderTypes
    {
      WelcomeView, 
      ControlPanelView,
      EventSelectedView,
      MainWindowView
    }
  }
}
