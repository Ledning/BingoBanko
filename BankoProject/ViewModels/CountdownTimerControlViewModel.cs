using BankoProject.Tools;
using Caliburn.Micro;
using Catel.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace BankoProject.ViewModels
{
  public sealed class CountdownTimerControlViewModel : Screen, IMainScreenTabItem
  {
    public CountdownTimerBigScreenViewModel CTBSVM { get; set;}
    Timer tmr;



    private BindableCollection<Button> _buttonsList;    //How many buttons do you want? dont matter 1 button call polit biru
    private int _counter; //do all the counting in miliseconds, mod by 60 if needed min or shit.
    private double countInterval = 10;


    public CountdownTimerControlViewModel()
    {
      tmr = new Timer(countInterval);
      tmr.Elapsed += HandleTimerCountdown;

      _buttonsList = new BindableCollection<Button>();
      CTBSVM = new CountdownTimerBigScreenViewModel();
      
      //INITALIZATION OF BUTTONLIST

      Counter = 180000; //3 minutes.

      DisplayName = "Nedtællings-kontrolskærm";
    }

    
    







    private void HandleTimerCountdown(object source, ElapsedEventArgs e)
    {
      Counter = Counter - (int)countInterval;
    }//SNBM
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

    public BindableCollection<Button> ButtonsList
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
