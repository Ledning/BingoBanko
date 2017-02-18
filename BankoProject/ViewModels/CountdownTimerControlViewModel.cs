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
    
    private WindowManager winMan;
    
    public CountdownTimerControlViewModel()
    {
      CTBSVM = new CountdowntimerBigScreenViewModel();
      DispatcherTimer timer = new DispatcherTimer();
      this.Timer = timer;

    }
    
    #region GetterSetter

    private BindableCollection<Team> _allTeams; 
    public BindableCollection<Team> AllTeams
    {
      get { return _allTeams; }
      set { _allTeams = value; NotifyOfPropertyChange(() => _allTeams); }
    }
    private DispatcherTimer Timer { get; set; }
    
    #endregion

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
      throw new NotImplementedException();
    }

  }
}
