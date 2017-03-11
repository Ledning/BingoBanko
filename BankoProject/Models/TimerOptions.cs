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
    private string _textTime;
    private string _error;
    private bool _canShow;
    private string _toggleTimerText;

    #endregion

    #region Constructors

    public TimerOptions()
    {
      _timerTime = new TimeSpan(0,0,0);
      NotifyOfPropertyChange(()=>TimerTime);
      TextTime = "05:00";
      CanShow = true;
      
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
            var temp = DateTime.Parse(TextTime);
            convertedTimespan = TimeSpan.Parse("0:" + TextTime);
          }
          catch (Exception)
          {
            CanShow = false;
            result = "Invalid time-format";
          }
          if (convertedTimespan.HasValue)
          {
            if (convertedTimespan <= TimeSpan.Zero)
            {
              result = "The Interval is 0 or less.";
              CanShow = false;
            }
            if (convertedTimespan >= new TimeSpan(1,0,0))
            {
              result = "The interval is larger than an hour.";
              CanShow = false;
            }
          }
          if (TextTime.Contains("-"))
          {
            result = "No negative values.";
            CanShow = false;
          }
        }



        if (result == null)
        {
          CanShow = true;
        }
        return result;
      }
    }

    public string Error
    {
      get { return _error; }
    }

    public bool CanShow
    {
      get { return _canShow; }
      set { _canShow = value; NotifyOfPropertyChange(()=>CanShow);}
    }

    public string ToggleTimerText
    {
      get { return _toggleTimerText; }
      set { _toggleTimerText = value; NotifyOfPropertyChange(()=>ToggleTimerText);}
    }

    #endregion
  }
}
