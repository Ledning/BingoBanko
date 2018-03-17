using Caliburn.Micro;

namespace TestProject.Models
{
  public class Deltagere : PropertyChangedBase
  {
    //How does this work:
    //Use "addPlayer" method in CountdownTimerControlViewModel to add players
    //use "removePlayer" method in countdownTimerControlViewmModel to remove players
    //It it numbers yes, but call it as many times as needed. bounds should be from 1 - 10
    private string name;
    private int time;
    private bool isFinished;

    public Deltagere()
    {

    }
    public Deltagere(string name)
    {
      this.name = name;
    }

    public string Name
    {
      get { return name; }
      set { name = value; NotifyOfPropertyChange(() => Name);}
    }

    public int Time
    { get { return time; }
      set { time = value; NotifyOfPropertyChange(() => Name); }
    }

    public bool IsFinished
    {
      get { return isFinished; }
      set { isFinished = value; NotifyOfPropertyChange(() => IsFinished); }
    }
  }
}
