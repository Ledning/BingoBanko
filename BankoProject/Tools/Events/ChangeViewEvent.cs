using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Tools.Events
{
  class ChangeViewEvent
  {
    string _viewName;
    string _dispatcherName;

    public string ViewName
    {
      get
      {
        return _viewName;
      }

      set
      {
        _viewName = value;
      }
    }

    public string DispatcherName
    {
      get
      {
        return _dispatcherName; 
        
      }
      set
      {
        _dispatcherName = value; 
        
      }
    }
  }
}
