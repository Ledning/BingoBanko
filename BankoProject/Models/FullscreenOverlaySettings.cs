using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Catel.Collections;

namespace BankoProject.Models
{
  public class FullscreenOverlaySettings : PropertyChangedBase
  {
    private bool _userDefinedScreen;
    private Visibility _isOverlayVisible = Visibility.Visible;
    private string _selectedBackgroundPath;
    private bool _stdScrnOL;
    private int _selectedIndex = 0;
    public BindableCollection<string> CustomOverlayImages = new BindableCollection<string>();
    private bool _scrnActivationRequired;


    public FullscreenOverlaySettings()
    {
      var dueTime = TimeSpan.FromSeconds(10);
      var interval = TimeSpan.FromSeconds(10);
      RunPeriodicAsync(UpdateBackgrounds, dueTime, interval, CancellationToken.None);
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

    public string ReturnSelectedPath()
    {
      return CustomOverlayImages[SelectedIndex];
    }

    public void UpdateBackgrounds()
    {
      DirectoryInfo info =
        new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                          "\\BingoBankoKontrol\\Background");
      FileInfo[] fInf = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
      foreach (var background in fInf)
      {
        CustomOverlayImages.Add(background.FullName);
      }
    }

    #region props

    public Visibility IsOverlayVisible
    {
      get { return _isOverlayVisible; }
      set
      {
        _isOverlayVisible = value;
        NotifyOfPropertyChange(() => IsOverlayVisible);
      }
    }

    public string SelectedBackgroundPath
    {
      get { return _selectedBackgroundPath; }
      set
      {
        _selectedBackgroundPath = value;
        NotifyOfPropertyChange(() => SelectedBackgroundPath);
      }
    }

    public bool StdScrnOl
    {
      get { return _stdScrnOL; }
      set
      {
        _stdScrnOL = value;
        NotifyOfPropertyChange(() => StdScrnOl);
      }
    }

    public int SelectedIndex
    {
      get { return _selectedIndex; }
      set
      {
        _selectedIndex = value;
        NotifyOfPropertyChange(() => SelectedIndex);
      }
    }

    public bool UserDefinedScreen
    {
      get { return _userDefinedScreen; }
      set
      {
        _userDefinedScreen = value;
        NotifyOfPropertyChange(() => UserDefinedScreen);
      }
    }

    public bool ScrnActivationRequired
    {
      get { return _scrnActivationRequired; }
      set
      {
        _scrnActivationRequired = value;
        NotifyOfPropertyChange(() => ScrnActivationRequired);
      }
    }

    #endregion
  }
}