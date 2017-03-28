using System;
using Caliburn.Micro;

namespace TestProject.Models
{
  [Serializable]
  public class Team : PropertyChangedBase
  {
    public Team()
    {
      
    }
    public Team(int teamMembers)
    {
      this.Points = 0;
      this.TeamNumber = _teamNumber;
      _teamNumber++;
      
    }
    private static int _teamNumber = 0;
    public int TeamNumber;

    private int _points;
    public int Points
    {
      get { return _points; }
      set { _points = value; NotifyOfPropertyChange(() => Points); }
    }
    public override string ToString()
    {
      //returns the team
      return "Hold" + " " + this.TeamNumber.ToString();
    }

    private bool _isTeamActive;

    public bool IsTeamActive
    {
      get { return _isTeamActive; }
      set { _isTeamActive = value; NotifyOfPropertyChange( () => IsTeamActive); }
    }

    public string TeamInfo { get { return ToString(); } }

  }
}

