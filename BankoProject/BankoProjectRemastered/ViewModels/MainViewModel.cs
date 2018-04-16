using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using BankoProjectRemastered.Interfaces;
using BankoProjectRemastered.Models;
using BankoProjectRemastered.Tools;
using BankoProjectRemastered.Views;
using CommonServiceLocator;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace BankoProjectRemastered.ViewModels
{
  class MainViewModel : BindableBase, IViewModel, INavigationAware
  {
    public MainViewModel(IBankoEventObject bEvent, IRegionManager regionManager)
    {
      Event = (BankoEvent) bEvent;
      RegMan = regionManager;
      RegisterViewsForNavigationInRegion();
      Navigate("ContentRegion", "ControlsView");
    }

    #region NavigationFunctionality

    /// <summary>
    /// Use this to register different views for navigation in the regions desired
    /// </summary>
    private void RegisterViewsForNavigationInRegion()
    {
      RegMan.RegisterViewWithRegion("ContentRegion", typeof(ControlsView));
    }


    private void Navigate(string region, string navigatePath)
    {
      if (navigatePath != null)
        RegMan.RequestNavigate(region, navigatePath);
    }

    private void NavigationComplete(NavigationResult obj)
    {
      if (obj.Result != null && (bool)obj.Result)
      {
        BnkLogger.LogLowDebugInfo("Navigation to " + obj.Context.Uri + " complete.");
      }
      else
      {
        BnkLogger.LogLowDebugInfo("Navigation to " + obj.Context.Uri + " failed.");
      }
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

    #endregion


    private BankoEvent Event;
    private readonly IRegionManager RegMan;


  }
}