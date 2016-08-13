using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Tools.Events
{
  class CommunicationObject
  {
    public CommunicationObject(ApplicationWideEnums.MessageTypes msgType, string dispatcher)
    {
      Message = msgType;
      DispatcherName = dispatcher;
    }


    public ApplicationWideEnums.MessageTypes Message { get; }

    public string DispatcherName { get; }
  }
}
