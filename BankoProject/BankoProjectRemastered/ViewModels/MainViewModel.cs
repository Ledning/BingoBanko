using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProjectRemastered.Interfaces;
using BankoProjectRemastered.Models;
using CommonServiceLocator;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace BankoProjectRemastered.ViewModels
{
    class MainViewModel : BindableBase, IViewModel, INavigationAware
    {

      public MainViewModel(BankoEvent bEvent)
      {
        Event = bEvent;
      }

      public BankoEvent Event { get; set; }
      public void InitializeEvent()
      {

      }

      public void OnNavigatedTo(NavigationContext navigationContext)
      {
        throw new NotImplementedException();
      }

      public bool IsNavigationTarget(NavigationContext navigationContext)
      {
        throw new NotImplementedException();
      }

      public void OnNavigatedFrom(NavigationContext navigationContext)
      {
        throw new NotImplementedException();
      }
    }
}
