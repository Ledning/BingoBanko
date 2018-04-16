using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BankoProjectRemastered.Interfaces;
using BankoProjectRemastered.Models;
using BankoProjectRemastered.Tools;
using BankoProjectRemastered.ViewModels;
using BankoProjectRemastered.Views;
using CommonServiceLocator;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;

namespace BankoProjectRemastered
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
#if DEBUG
      BankoEvent defaultEvent = new BankoEvent(EventParams.Default);
      containerRegistry.RegisterInstance<IBankoEventObject>(defaultEvent);

#else
      BankoEvent normalEvent = new BankoEvent(EventParams.NormalMode);
      containerRegistry.RegisterInstance(defaultEvent);
#endif

      containerRegistry.RegisterForNavigation<MainView, MainViewModel>("MainView");
      containerRegistry.RegisterForNavigation<ControlsView, ControlsViewModel>("ControlsView");

      //containerRegistry.RegisterInstance(typeof(IContainerRegistry)); //Generally bad to include this in the container, if made properly it should not be necessary to acces the container at runtime
    }

    protected override Window CreateShell()
    {
      return ServiceLocator.Current.GetInstance<MainView>();
    }
  }
}