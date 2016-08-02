using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.ViewModels
{
  class ControlPanelViewModel : Conductor<IMainViewItem>.Collection.OneActive, IMainScreenTabItem
  {
    public ControlPanelViewModel()
    {
      //ActivateItem(); //Lav det her heehe
    }
  }
}
