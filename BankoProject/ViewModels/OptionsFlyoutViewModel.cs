using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Tools;

namespace BankoProject.ViewModels
{
  /// <summary>
  /// lul
  /// </summary>
  public class OptionsFlyoutViewModel : Screen
  {

    public BindableCollection<IEnumerable<Monitor>> MonitorList { get; set; }
    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));

    public OptionsFlyoutViewModel()
    {

    }

    protected override void OnViewReady(object view)
    {
      MonitorList = new BindableCollection<IEnumerable<Monitor>>();
      MonitorList.Add(Monitor.AllMonitors);
      _log.Info("TRIGGERED");
      _log.Info("\n\n\n\n\n");
    }




  }
}
