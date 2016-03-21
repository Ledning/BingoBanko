using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using Orchestra.Views;
using System.ComponentModel.Composition;
using System.ComponentModel;
using Caliburn.Micro;
using BankoProject.Tools;

namespace BankoProject.ViewModels
{
  [Export(typeof(IShell))]
  class MainWindowViewModel : Conductor<IMainScreenTabItem>.Collection.OneActive, IShell
  {
    //Every property that needs to reflect on GUI when changed needs get set/notifyofpropertychange. The childVM's does not, as their respective notifies are called when stuff is modified.
    public ControlsScreenViewModel ControlsScreenViewModel{get; set;}
    public CountdownTimerControlViewModel CountdownTimerControlViewModel { get; set;}
    public CountdowntimerBigScreenViewModel CountdowntimerBigScreenViewModel { get; set;}

    [ImportingConstructor]
    public MainWindowViewModel(IEnumerable<IMainScreenTabItem> tabs)
    {
      List<IMainScreenTabItem> refList = new List<IMainScreenTabItem>(tabs);
      ControlsScreenViewModel = (ControlsScreenViewModel)refList[0];
      CountdownTimerControlViewModel = (CountdownTimerControlViewModel)refList[1]; //IMPORTANT IF ANY NEW VIEWS ARE ADDED, THIS HAVE TO BE RECONSIDERED.
      CountdowntimerBigScreenViewModel = CountdownTimerControlViewModel.CTBSVM;
      Items.AddRange(tabs);
    }

    public void Show()
    {


    }


  }
}
