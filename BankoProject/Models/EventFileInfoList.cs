using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.Models
{
  class EventFileInfoList : PropertyChangedBase
  {
    private Dictionary<string, string> _events;


    public EventFileInfoList()
    {
    }


    /// <summary>
    /// Updates the list of latest events. Remember there is no filtering on this, it could technically show a completely unrelated file as being an eventfile. 
    /// </summary>
    /// <returns>The return value indicates whether or not anything changed.</returns>
    public bool UpdateLatestEvents()
    {
      bool changeHappened = false;
      DirectoryInfo info = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BingoBankoKontrol\\LatestEvents");
      Events = new Dictionary<string, string>();
      FileInfo[] fInf = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
      foreach (var file in fInf)
      {
        if (!Events.ContainsKey(file.Name))
        {
          Events.Add(file.Name, file.CreationTime.ToString());
          changeHappened = true;
        }
        else
        {
          string value;
          Events.TryGetValue(file.Name, out value);
          if (value != null)
          {
            if (value != file.CreationTime.ToString())
            {
              Events.Remove(file.Name);
              Events.Add(file.Name, file.CreationTime.ToString());
              changeHappened = true;
            }
          }
          
        }
      }
      return changeHappened;
    }

    public Dictionary<string, string> Events
    {
      get { return _events; }
      set { _events = value; NotifyOfPropertyChange(()=>Events);}
    }
  }
}