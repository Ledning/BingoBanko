using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using Orchestra.Views;
using System.ComponentModel.Composition;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Windows;
using BankoProject.Models;
using Caliburn.Micro;
using BankoProject.Tools;
using BankoProject.Tools.Events;
using BingoCardGenerator;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Printer_Project;
using System.Windows.Forms;
using FormScrn = System.Windows.Forms.Screen;
using WpfScreenHelper;
using MessageBox = System.Windows.MessageBox;
using ScrnHelpScrn = WpfScreenHelper.Screen;

namespace BankoProject.ViewModels
{
  /*
   HOW TO DO EVENTS AND USE WINDOWMANAGER:

    In order to use anything contained in the IOC container this project has, it is as simple as creating a property to contain it, and then assigning that propert to the corresponding value from IOC:
    someproperty = IoC.Get<IWindowManager>();

    As of right now, it has 3(4) things:
    WindowManager  (Use IWindowManager)
    EventAggregator (Use IEventAggregator)
    BingoEvent (Use BingoEvent)

    Windowmanager is the only one which is not a singleton (meaning it wont be the same instance passed around)
    BingoEvent has all the info needed to run and show an event.

    To use WindowManager to show a dialog:
    _winMan.ShowDialog(new dialogViewModel("dialog text"))


    PH means placeholder
  */
  class MainWindowViewModel : Conductor<IMainViewItem>.Collection.OneActive, IShell, IHandle<CommunicationObject>
  {
    private IWindowManager _winMan;
    private IEventAggregator _eventAggregator;
    private BingoEvent _bingoEvent;
    private readonly ILog _log = LogManager.GetLog(typeof(MainWindowViewModel));
    private bool _isFlyoutOpen = false;



    public MainWindowViewModel()
    {
      ActivateItem(new WelcomeViewModel());
    }

    public BingoEvent Event
    {
      get { return _bingoEvent; }
      set { _bingoEvent = value; NotifyOfPropertyChange(()=> Event);}
    }

    public bool IsFlyoutOpen
    {
      get { return _isFlyoutOpen; }
      set { _isFlyoutOpen = value; NotifyOfPropertyChange(()=> IsFlyoutOpen);}
    }



    //The function below can be used as a constructor for the view. Everything in it will happen after the view is loaded.
    protected override void OnViewReady(object view)
    {
      _winMan = IoC.Get<IWindowManager>();
      _winMan.ShowWindow(new DebuggingWindowViewModel(490,490,-500,500));
      _eventAggregator = IoC.Get<IEventAggregator>();
      Event = IoC.Get<BingoEvent>();
      _eventAggregator.Subscribe(this);
      DisplayName = "Bingo Kontrol";
      _log.Info("Main View loaded");
     
      worker.DoWork += worker_DoWork;
      worker.RunWorkerCompleted += worker_RunWorkerCompleted;
      //foreach (var przScrn in GetTypesInNamespace(Assembly.GetExecutingAssembly(), "BankoProject.ViewModels.PresentationScreenViewModels"))
      //{
      //  //Activator.CreateInstance<IPresentationScreenItem>();
      //}
    }

    private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
      return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
    }

    public void SpawnPrezScreen()
    {
     _winMan.ShowWindow(new PresentationScreenHostViewModel());
    }

    public void Handle(CommunicationObject message)
    {
      switch (message.Message)
      {
        case ApplicationWideEnums.MessageTypes.ChngWelcomeView:
          _log.Info("Changing to WelcomeView...");
          DisplayName = "Bingo Kontrol";
          GoToWelcomeView();
          break;
        case ApplicationWideEnums.MessageTypes.ChngControlPanelView:
          _log.Info("Changing to ControlPanelView...");
          DisplayName = DisplayName + ": " + Event.EventTitle;
          GoToControlPanel();
          break;

        case ApplicationWideEnums.MessageTypes.Save:
          _log.Info("Saving...");
          SaveSession();
          break;

        case ApplicationWideEnums.MessageTypes.Load:
          _log.Info("Loading...");
          LoadSession();
          break;

        case ApplicationWideEnums.MessageTypes.GeneratePlates:
          _log.Info("Generate-plates message recieved...");
          GeneratePlates();
          break;
        case ApplicationWideEnums.MessageTypes.RbPrezScreen:
          SpawnPrezScreen();
          break;
      }
    }

    #region async plate generation
    private readonly BackgroundWorker worker = new BackgroundWorker();

    private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      _log.Info("Async plate-generation running...");
      Event.Generating = true;
      Generator gen = new Generator(Event.SInfo.Seed);
      PDFMaker maker = new PDFMaker();
      maker.MakePDF(gen.GenerateCard(Event.PInfo.PlatesGenerated));
    }


    private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      Event.Generating = false;
      _log.Info("Generation done.");
    }

    public void GeneratePlates()
    {
      worker.RunWorkerAsync();
    }

    #endregion


    #region eventbusMethods

    private void GoToWelcomeView()
    {
      ActivateItem(new WelcomeViewModel());
      DisplayName = "Bingo Banko";
    }

    private void GoToControlPanel()
    {
      ActivateItem(new ControlPanelViewModel());
      DisplayName = "Bingo Bango: " + Event.EventTitle;
    }
    #endregion


    public bool LoadSession()
    {

      ////find the file
      //string eventDir = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\", @"Resources\Events", Event.EventTitle);
      //string eventDataDirString = eventDir + Event.EventTitle + @".bingoevent";
      //FileStream fs = new FileStream(eventDataDirString, FileMode.Open);
      //try
      //{
      //  BinaryFormatter formatter = new BinaryFormatter();
      //  Event = (BingoEvent)formatter.Deserialize(fs);
      //}
      //catch (SerializationException e)
      //{
      //  _log.Error(e);
      //  _log.Warn(e.Message);
      //}
      //finally
      //{
      //  fs.Close();
      //}
      //_log.Warn("NOT IMPLEMENTED");
      return true;
    }


    public bool SaveSession() 
    {
      
      //string eventDir = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\", @"Resources\Events", Event.EventTitle);
      //if(Directory.Exists(eventDir))
      //{
      //  MessageBoxResult dR = MessageBox.Show("Event already exists. Do you want to override it?", "Confirmation box",
      //    MessageBoxButton.YesNo);
      //  if (dR == MessageBoxResult.No)
      //  {
      //    return false;
      //  }
      //  else
      //  {
      //    Directory.Delete(eventDir);
      //  }
      //}
      //string eventDataDirString = eventDir + Event.EventTitle + @".bingoprojekt";

      //FileStream fStream = new FileStream(eventDataDirString, FileMode.Create);
      //BinaryFormatter bFormatter = new BinaryFormatter();
      //try
      //{
      //  bFormatter.Serialize(fStream, Event);
      //}
      //catch (SerializationException e)
      //{
      //  _log.Error(e);
      //  _log.Warn(e.Message);
      //  return false;
      //}
      //finally
      //{
      //  fStream.Close();
      //}
      return true;
    }

    public void Show()
    {
    }
  }
}
