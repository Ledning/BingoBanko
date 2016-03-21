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
  public sealed class CountdownTimerControlViewModel : Screen, IMainScreenTabItem
  {
    public CountdowntimerBigScreenViewModel CTBSVM { get; set;}
    Timer tmr;



    private BindableCollection<Deltagere> _buttonsList;    //How many buttons do you want? dont matter 1 button call polit biru
    private int _counter; //do all the counting in miliseconds, mod by 60 if needed min or shit.
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

      Counter = 180000; //3 minutes.

      DisplayName = "Nedtællings-kontrolskærm";
      ButtonsList.Add(new Deltagere(""));
      ButtonsList.Add(new Deltagere(""));
      ButtonsList.Add(new Deltagere(""));
      ButtonsList.Add(new Deltagere(""));
    }

    public void Start()
    {
      ButtonsList.RemoveAt(1);
    }
    public void Stop()
    {
      throw new NotImplementedException();
    }
    public void Reset()
    {
      throw new NotImplementedException();
    }

    private void HandleTimerCountdown(object source, ElapsedEventArgs e)
    {
      Counter = Counter - (int)countInterval;
    }
    private void ResetTimer()
    {
      Counter = 0;
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
    #endregion
  }
}
