using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BankoProject.Models;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.PresentationScreen
{
  class FullscreenImageViewModel : Screen, IPresentationScreenItem
  {
    private BingoEvent _event;

    //Der burde ikke være problemer med at køre IOC her, da den bliver automatisk kaldt ved instantation af viewet
    public FullscreenImageViewModel()
    {
      Event = IoC.Get<BingoEvent>();
      ShowStandard();
      SaveDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BingoBankoKontrol" + "\\Backgrounds";
      var dueTime = TimeSpan.FromSeconds(10);
      var interval = TimeSpan.FromSeconds(10);
      RunPeriodicAsync(UpdateBackgrounds, dueTime, interval, CancellationToken.None);
    }

    #region Overrides of ViewAware

    protected override void OnViewReady(object view)
    {
      ShowStandard();
    }

    #endregion

    #region Background stuff

    private const string resourceFolder = "/BankoProject;component/Resources/";
    private readonly string standardOverlay = resourceFolder + "frontpgggg.PNG";
    private Visibility _isOverlayVisible = Visibility.Visible;
    private string _selectedBackgroundPath; //for whatever is shown on the overlay
    private string _saveDirectory;
    private string _dropdownSelectedBackground;
    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));


    /// <summary>
    /// Shows a seethrough overlay, eg nothing
    /// </summary>
    public void ShowEmpty()
    {
      Event.WindowSettings.PrsSettings.OverlaySettings.IsOverlayVisible = Visibility.Hidden;
    }

    public void ShowStandard()
    {
      Event.WindowSettings.PrsSettings.OverlaySettings.IsOverlayVisible = Visibility.Visible;
     _log.Info(standardOverlay);
      Event.WindowSettings.PrsSettings.OverlaySettings.SelectedBackgroundPath = standardOverlay;
    }

    public void ShowCustom()
    {
      Event.WindowSettings.PrsSettings.OverlaySettings.IsOverlayVisible = Visibility.Visible;
    }



    private static async Task RunPeriodicAsync(System.Action onTick,
      TimeSpan dueTime,
      TimeSpan interval,
      CancellationToken token)
    {
      // Initial wait time before we begin the periodic loop.
      if (dueTime > TimeSpan.Zero)
        await Task.Delay(dueTime, token);

      // Repeat this loop until cancelled.
      while (!token.IsCancellationRequested)
      {
        // Call our onTick function.
        onTick?.Invoke();

        // Wait to repeat again.
        if (interval > TimeSpan.Zero)
          await Task.Delay(interval, token);
      }
    }

    public void UpdateBackgrounds()
    {
      DirectoryInfo info =
  new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                    "\\BingoBankoKontrol\\Background");
      FileInfo[] fInf = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
    }


    #endregion

    #region props


    

    public string SaveDirectory
    {
      get { return _saveDirectory; }
      set { _saveDirectory = value; NotifyOfPropertyChange(()=>SaveDirectory);}
    }

    public string DropdownSelectedBackground
    {
      get { return _dropdownSelectedBackground; }
      set { _dropdownSelectedBackground = value; NotifyOfPropertyChange(()=> DropdownSelectedBackground);}
    }

    public BingoEvent Event
    {
      get { return _event; }
      set { _event = value; NotifyOfPropertyChange(()=> Event);}
    }

    #endregion


  }
}