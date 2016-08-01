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

    [ImportingConstructor]
    public MainWindowViewModel(IEnumerable<IMainScreenTabItem> tabs)
    {
      //Items.AddRange(tabs); Reenable to use tabs
      ActivateItem(new WelcomeViewModel());
    }

    public void Show()
    {


    }


  }
}
