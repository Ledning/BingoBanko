using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using Orchestra.Views;
using System.ComponentModel.Composition;

namespace BankoProject.ViewModels
{
  [Export(typeof(IShell))]
  class MainWindowViewModel : ViewModelBase, IShell
  {
    [ImportingConstructor]
    public MainWindowViewModel()
    {

    }
    public void Show()
    {
      
    }
  }
}
