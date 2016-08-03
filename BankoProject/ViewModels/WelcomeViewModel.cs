using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.ViewModels
{
  class WelcomeViewModel : IMainViewItem
  {
    private readonly IWindowManager _winMan;


    public WelcomeViewModel(IWindowManager windowManager)
    {
      _winMan = windowManager;
    }




    public void CreateEvent()
    {
      _winMan.ShowDialog(new dialogViewModel("hrelo men"));
    }

    public void OpenFileDialog()
    {
      var ofd = new Microsoft.Win32.OpenFileDialog()
      {
        Filter = "Event filer (*.bingoEvent)|*bingoEvent"
      };

      if (ofd.ShowDialog() ?? false)
      {
        var d = ofd.FileNames;
      }
    }
  }
}
