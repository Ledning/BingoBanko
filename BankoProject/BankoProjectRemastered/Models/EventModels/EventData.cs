using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProjectRemastered.Tools;
using Prism.Mvvm;

namespace BankoProjectRemastered.Models.EventModels
{
  [Serializable]
  class EventData : BindableBase
  {
    private string _eventTitle;
    public EventData()
    {
      BnkLogger.LogLowDebugInfo("EventData init");
      EventTitle = "Unavngivet";
      EventCreatedDateTime = DateTime.Now;
    }

    public EventData(string eventTitle)
    {
      BnkLogger.LogLowDebugInfo("EventData init");
      EventCreatedDateTime = DateTime.Now;
      EventTitle = eventTitle;
    }

    #region GetSet
    public DateTime EventCreatedDateTime { get; }

    public string EventTitle
    {
      get { return _eventTitle; }
      set { _eventTitle = value; RaisePropertyChanged(nameof(EventTitle)); }
    }

    #endregion

  }
}
