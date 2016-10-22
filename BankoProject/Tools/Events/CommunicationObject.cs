using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Tools.Events
{
  class CommunicationObject
  {
    /// <summary>
    /// Used for communicating between viewmodels.
    /// Remember to add Viewmodels if additional ones are added. See ApplicationWideEnums for more. 
    /// </summary>
    /// <param name="msgType"></param>
    /// <param name="dispatcher"></param>
    public CommunicationObject(ApplicationWideEnums.MessageTypes msgType, ApplicationWideEnums.SenderTypes dispatcher)
    {
      Message = msgType;
      DispatcherName = dispatcher;
    }


    public ApplicationWideEnums.MessageTypes Message { get; }

    public ApplicationWideEnums.SenderTypes DispatcherName { get; }
  }
}
