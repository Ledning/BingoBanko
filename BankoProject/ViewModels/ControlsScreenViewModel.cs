using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using Caliburn.Micro;
using BankoProject.Tools;
using BankoProject.Models;

namespace BankoProject.ViewModels
{
  public sealed class ControlsScreenViewModel : Screen, IMainScreenTabItem
  {
    public OptionsFlyoutViewModel OFVM { get; set;}
    public ControlOptions COptions { get; set;}

    public ControlsScreenViewModel()
    {
      OFVM = new OptionsFlyoutViewModel();

      DisplayName = "BingoBanko Kontrol-Skærm";
    }

  }
}
