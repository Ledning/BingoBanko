using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Tools.Events
{
  class ChangeViewEvent
  {
    public ChangeViewEvent(ApplicationWideEnums.MessageTypes msgType, string dispatcher)
    {
      ViewName = msgType.ToString();
      DispatcherName = dispatcher;
    }


    public string ViewName { get; }

    public string DispatcherName { get; }
  }
}
