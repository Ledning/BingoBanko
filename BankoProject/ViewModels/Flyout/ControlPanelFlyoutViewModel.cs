using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.Flyout
{
  class ControlPanelFlyoutViewModel : Screen, IFlyoutItem
  {

    private bool _isOpen = false;


    public bool IsOpen
    {
      get { return _isOpen; }
      set { _isOpen = value; NotifyOfPropertyChange(() => IsOpen); }
    }
  }
}
