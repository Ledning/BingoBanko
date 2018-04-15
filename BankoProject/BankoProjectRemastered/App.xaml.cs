using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BankoProjectRemastered.Models;
using BankoProjectRemastered.ViewModels;
using BankoProjectRemastered.Views;
using CommonServiceLocator;
using Prism.Ioc;
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
      containerRegistry.RegisterInstance(defaultEvent);

#else 
      BankoEvent normalEvent = new BankoEvent(EventParams.NormalMode);
      containerRegistry.RegisterInstance(defaultEvent);
#endif

      containerRegistry.RegisterForNavigation<MainView, MainViewModel>("MainView");
    }

    protected override Window CreateShell()
    {
      return ServiceLocator.Current.GetInstance<MainView>();
    }
  }
}
