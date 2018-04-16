using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProjectRemastered.Interfaces;
using BankoProjectRemastered.Models;
using Prism.Mvvm;
using Prism.Regions;

namespace BankoProjectRemastered.ViewModels
{
  class ControlsViewModel : BindableBase, IViewModel, INavigationAware
  {
    public BankoEvent Event { get; set; }

    public ControlsViewModel(IBankoEventObject eBankoEvent)
    {
      Event = (BankoEvent) eBankoEvent;
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {

    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
      return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }
  }
}
