using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.Models
{
  [Serializable]
  public class TimerOptions : PropertyChangedBase, IDataErrorInfo
  {
    #region Fields
    private TimeSpan _timerTime;
    private string _textTime = "00:00";
    private string _error;

    #endregion

    #region Constructors

    public TimerOptions()
    {
      _timerTime = new TimeSpan();
      NotifyOfPropertyChange(()=>TimerTime);
    }


    #endregion


    #region Methods

    

    #endregion

    #region Props
    public TimeSpan TimerTime
    {
      get { return TimeSpan.Parse(TextTime); }
    }

    public string TextTime
    {
      get { return _textTime; }
      set { _textTime = value; NotifyOfPropertyChange(()=>TextTime); }
    }
    #endregion

    #region Implementation of IDataErrorInfo

    public string this[string columnName]
    {
      get
      {
        string result = null;
        if (columnName == "TextTime")
        {
          TimeSpan? convertedTimespan = null;
          try
          {
            convertedTimespan = TimeSpan.Parse(TextTime);
          }
          catch (Exception)
          {
            result = "Invalid time-format";
          }
          if (convertedTimespan.HasValue)
          {
            if (convertedTimespan <= TimeSpan.Zero)
            {
              result = "The Interval is 0 or less.";
            }
            if (convertedTimespan >= new TimeSpan(0,1,0))
            {
              result = "The interval is larger than an hour.";
            }
          }
        }




        return result;
      }
    }

    public string Error
    {
      get { return _error; }
    }

    #endregion
  }
}
