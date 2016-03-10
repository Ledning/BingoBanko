using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using Caliburn.Micro;
using BankoProject.Tools;

namespace BankoProject.ViewModels
{
  public sealed class ControlsScreenViewModel : Screen, IMainScreenTabItem
  {
    public OptionsFlyoutViewModel OFVM { get; set;}

    public ControlsScreenViewModel()
    {
      OFVM = new OptionsFlyoutViewModel();
      //some sort of message transport between this and flyout is going to have to happen. dunno how. 

      DisplayName = "BingoBanko Kontrol-Skærm";
    }

  }
}
