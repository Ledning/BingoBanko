using BankoProject.Models;
using BankoProject.Tools;
using Caliburn.Micro;
using Catel.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BankoProject.ViewModels
{
  public class CountdownTimerControlViewModel : Screen, IMainViewItem
  {
    public CountdowntimerBigScreenViewModel CTBSVM { get; set;}
    
    private WindowManager _winMan;
    
    public CountdownTimerControlViewModel(BindableCollection<Team> allTeams)
    {
      CTBSVM = new CountdowntimerBigScreenViewModel();
      
      this.AllTeams = new BindableCollection<Team>(allTeams);
      this.CurrentTime = new DateTime();

      //setting up the dispatcher thing. This is obviously from stackoverflow..
      this.Timer = new DispatcherTimer();
      this.Timer.Tick += new EventHandler(dispatcherTimer_Tick);
      this.Timer.Interval = new TimeSpan(0, 0, 1);
      
    }
    private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
      this.CurrentTime = this.CurrentTime.AddSeconds(1);
    }


    private BindableCollection<Team> _allTeams; 
    public BindableCollection<Team> AllTeams
    {
      get { return _allTeams; }
      set { _allTeams = value; NotifyOfPropertyChange(() => _allTeams); }
    }
    private DispatcherTimer Timer { get; set; }
    private DateTime _currentTime;

    public DateTime CurrentTime
    {
      get { return _currentTime; }
      set { _currentTime = value; NotifyOfPropertyChange( () => CurrentTime); }
    }

    public void TimerStart()
    {
      if (!this.Timer.IsEnabled)
      {
        this.Timer.Start();
      }
    }

    public void TimerStop()
    {
      if (this.Timer.IsEnabled)
      {
        this.Timer.Stop();
      }
    }

    public void TimerReset()
    {
      if (!this.Timer.IsEnabled)
      {
        this.CurrentTime = new DateTime();
      }
    }
  }
}
