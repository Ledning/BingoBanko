using BankoProject.Tools;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BankoProject.Models;
using BankoProject.Tools.Events;
using BankoProject.ViewModels.PresentationScreen;

namespace BankoProject.ViewModels
{
  class WelcomeViewModel : Screen, IMainViewItem
  {
    private IWindowManager _winMan;
    private IEventAggregator _events;
    private BingoEvent _bingoEvent;
    private readonly ILog _log = LogManager.GetLog(typeof(WelcomeViewModel));

    public struct EventFileInfo //TODO: make this more sensible lol, this was just quick to make it work
    {
      public string title;
      public string date;

      public EventFileInfo(string _title, string _date)
      {
        title = _title;
        date = _date;
      }
    }

    public WelcomeViewModel()
    {
      SaveDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      CreateApplicationDirectories();
      LatestEvents = new BindableCollection<EventFileInfo>();
      DirectoryInfo info = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BingoBankoKontrol\\LatestEvents");

      FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
      foreach (FileInfo file  in files)
      {
        LatestEvents.Add(new EventFileInfo(file.Name, file.LastAccessTime.ToString()));
      }


      Title = "BingoBanko Kontrol";

    }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set { _bingoEvent = value; NotifyOfPropertyChange(()=>Event);}
    }

    private string _title;
    public string Title
    {
      get { return _title; }
      set { _title = value; NotifyOfPropertyChange(() => Title); }
    }

    protected override void OnViewReady(object view)
    {
      SaveDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      _winMan = IoC.Get<IWindowManager>();
      _events = IoC.Get<IEventAggregator>();
      Event = IoC.Get<BingoEvent>();
    }


    public void CreateEvent()
    {

      bool? result = _winMan.ShowDialog(new CreateEventViewModel()); 
      if (result.HasValue)
      {
        if (result.Value)
        {
          _log.Info("exit on event created, welcomeview");
          _events.PublishOnBackgroundThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.Save, ApplicationWideEnums.SenderTypes.WelcomeView));
          _log.Info("SAVEMESSAGE");
          _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.ChngControlPanelView, ApplicationWideEnums.SenderTypes.WelcomeView));
        }
      }
    }
    public BindableCollection<EventFileInfo> LatestEvents { get; set; }

    public string SaveDirectory
    {
      get { return _saveDirectory; }
      set { _saveDirectory = value; }
    }


    //TODO: En collection som Seneste Events kan binde til
    //Navneboksen for Seneste Events er slightly unaligned

    //TODO: Titlen i Welcomeview skal bindes til en string prop

    //TODO: Ordentlig boks til titlen i WelcomewView.
    //Den ligner sku en sæk lort lige pt





    public void OpenFileDialog()
    {
      var ofd = new Microsoft.Win32.OpenFileDialog()
      {
        Filter = "Event filer (*.xml)|*xml"
      };
      var d = "NOFILE";
      if (ofd.ShowDialog() ?? false)
      {
        d = ofd.FileName;
      }
      if (d != "NOFILE")
      {
        d = Path.GetFileNameWithoutExtension(d);
        _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.Load, ApplicationWideEnums.SenderTypes.WelcomeView, d));
        _events.PublishOnUIThread(new CommunicationObject(ApplicationWideEnums.MessageTypes.ChngControlPanelView, ApplicationWideEnums.SenderTypes.WelcomeView));
      }
    }



    #region DirectoryStuff

    private string _saveDirectory;
    //TODO: Make these use a string for each of the subdirectories and the main directory, no chance of spelling error
    private bool ApplicationDirectoryExists()
    {
      if (Directory.Exists(SaveDirectory + "\\BingoBankoKontrol"))
      {
        return true;
      }
      return false;
    }
    private bool ApplicationSubDirectoriesExists()
    {
      bool result = false;
      if (Directory.Exists(SaveDirectory + "\\BingoBankoKontrol" + "\\LatestEvents"))
      {
        if (Directory.Exists(SaveDirectory + "\\BingoBankoKontrol" + "\\Settings"))
        {
          if (Directory.Exists(SaveDirectory + "\\BingoBankoKontrol" + "\\Background"))
          {
            result = true;
          }
        }
      }
      else
        result = false;
      return result;
    }
    private bool ApplicationDirectoriesExist()
    {
      if (ApplicationDirectoryExists())
      {
        if (ApplicationSubDirectoriesExists())
        {
          return true;
        }
      }
      return false;
    }

    private void CreateApplicationDirectories()
    {
      Directory.CreateDirectory(SaveDirectory + "\\BingoBankoKontrol");
      CreateLatestDirectory();
      CreateSettingsDirectory();
      CreateBackgroundsDirectories();
    }

    private void CreateLatestDirectory()
    {
      Directory.CreateDirectory(SaveDirectory + "\\BingoBankoKontrol" + "\\LatestEvents");
    }

    private void CreateSettingsDirectory()
    {
      Directory.CreateDirectory(SaveDirectory + "\\BingoBankoKontrol" + "\\Settings");
    }

    private void CreateBackgroundsDirectories()
    {
      Directory.CreateDirectory(SaveDirectory + "\\BingoBankoKontrol" + "\\Background");
    }

    #endregion
  }

}
