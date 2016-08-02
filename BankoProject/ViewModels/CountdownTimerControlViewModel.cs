using BankoProject.Models;
using BankoProject.Tools;
using Caliburn.Micro;
using Catel.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace BankoProject.ViewModels
{
  [Export(typeof(CountdownTimerControlViewModel))]
  public sealed class CountdownTimerControlViewModel : Screen, IMainViewItem
  {
    public CountdowntimerBigScreenViewModel CTBSVM { get; set;}
    private Timer tmr;
    private bool _isTmrRunning;



    private BindableCollection<Deltagere> _buttonsList;    //How many buttons do you want? dont matter 1 button call polit biru
    private int _counter = 0, counterStartVal = 0; //do all the counting in miliseconds, mod by 60 if needed min or shit.
    private double countInterval = 10;
    private WindowManager winMan;


    [ImportingConstructor]
    public CountdownTimerControlViewModel(WindowManager wman)
    {
      //initalization
      tmr = new Timer(countInterval);
      tmr.Elapsed += HandleTimerCountdown;
      _buttonsList = new BindableCollection<Deltagere>(); //basically list of players
      CTBSVM = new CountdowntimerBigScreenViewModel();
      winMan = wman;
      DisplayName = "Nedtællings-kontrolskærm";
    }

    public void timerStart()
    {
      tmr.Start();
      IsTmrRunning = true;
    }
    public void timerStop()
    {
      tmr.Stop();
      IsTmrRunning = false;
    }
    public void timerReset()
    {
      timerStop();
      Counter = counterStartVal;
    }

    private void HandleTimerCountdown(object source, ElapsedEventArgs e)
    {
      Counter = Counter - (int)countInterval;
    }
    #region GetterSetter
    public int Counter
    {
      get
      {
        return _counter;
      }

      set
      {
        _counter = value;
        CTBSVM.Counter = value;

      }
    }

    public BindableCollection<Deltagere> ButtonsList
    {
      get
      {
        return _buttonsList;
      }

      set
      {
        _buttonsList = value;
        CTBSVM.ButtonsList = value;
        NotifyOfPropertyChange(() => ButtonsList);
      }
    }

    public bool IsTmrRunning
    {
      get
      {
        return _isTmrRunning;
      }

      set
      {
        _isTmrRunning = value;
      }
    }
    #endregion
  }
}
