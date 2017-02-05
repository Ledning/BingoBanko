using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.Models
{
  public class CompetitionObject : PropertyChangedBase
  {
    public CompetitionObject(int numberOfParticipants, int numberOfTeams, int competitionDuration)
    {
      this.AllTeams = new List<Team>();
      for (int i = 0; i < numberOfTeams; i++)
      {
        Team team = new Team();
        this.AllTeams.Add(team);
      }
      this.NumberOfParticipants = numberOfParticipants;
      this.CompetitionDuration = CompetitionDuration;
    }
    public int NumberOfParticipants { get; set; }
    public int CompetitionDuration { get; set; }
    public List<Team> AllTeams { get; set; }
  }
}
