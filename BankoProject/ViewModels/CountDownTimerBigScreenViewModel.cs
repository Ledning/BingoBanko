using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using System.Timers;
using System.Windows.Controls;
using Caliburn.Micro;
using BankoProject.Models;
using BankoProject.Tools;

namespace BankoProject.ViewModels
{
  public sealed class CountdowntimerBigScreenViewModel : Screen
  {
    //The colour of the button at start and finish. NSFP. Modify if different colours are wanted. cannot be made from designer. 
    string finishCoulour = "#FF3FBD5B";
    string unfinishColour = "#FFBF5050";


    int _buttonWidth, _buttonHeight; //buttonsize. maybe make scale with button count? certainly possible, try scaling in control and syncing that here. SFP

    int _counter; //timer count int ms, SFP
    BindableCollection<Deltagere> _buttonsList; //list of buttons on the screen. set to 0 if no buttons are desired, it will now show only the timer. hopefully. helpers might be needed. SFP

    public CountdowntimerBigScreenViewModel()
    {
      _buttonsList = new BindableCollection<Deltagere>();
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
        NotifyOfPropertyChange(() => Counter);
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
        NotifyOfPropertyChange(() => ButtonsList);
      }
    }

    #endregion

  }
}
