using System;
using System.Collections;
using System.Collections.Generic;
using Caliburn.Micro;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Text;
using System.Windows.Controls.Primitives;
using System.Xml.Serialization;
using BingoCardGenerator;
using Printer_Project;

namespace BankoProject.Models
{
  [Serializable]
  public class BingoEvent : PropertyChangedBase
  {
    private int _queueLength = 0;
    private string _eventTitle;
    private DateTime _creationTime;
    private WinSettings _winSettings;
    private bool _initialised = false;
    private bool _generating = false;

    [NonSerialized] private readonly ILog _log = LogManager.GetLog(typeof(BingoEvent));

    private BankoOptions _bnkOptions;
    private CompetitionOptions _cmpOptions;
    private VisualsOptions _vsOptions;
    private SeedInfo _seedInfo;
    private PlateInfo _plateInfo;
    private BingoNumberBoard _numberBoard;

    //any aggregated objects; settings object(general/specific), lists of objects for the competitions held during the event,
    [XmlArray("CompetitionList")] [XmlArrayItem("Competition")] private BindableCollection<CompetitionObject>
      _competitionList; //A list of all the competitions during the game

    [XmlArray("BingoNumberQueue")] [XmlArrayItem("BingoNumber")] private BindableCollection<BingoNumber>
      _bingoNumberQueue; //the numbers picked in the game, input into this list as they come in. 


    #region GetterSetter

    public string EventTitle
    {
      get { return _eventTitle; }
      set
      {
        _eventTitle = value;
        NotifyOfPropertyChange(EventTitle);
      }
    }

    public BindableCollection<string> RecentFiles { get; set; }

    public DateTime CreationTime
    {
      get { return _creationTime; }
    }
    
    public BingoNumberBoard NumberBoard
    {
      get { return _numberBoard; }
      set
      {
        _numberBoard = value;
        NotifyOfPropertyChange(() => NumberBoard);
      }
    }

    [XmlArray("CompetitionList")]
    [XmlArrayItem(Type = typeof(CompetitionObject))]
    public BindableCollection<CompetitionObject> CompetitionList
    {
      get { return _competitionList; }
      set
      {
        _competitionList = value;
        NotifyOfPropertyChange(() => CompetitionList);
      }
    }
    [XmlArray("BingoNumberQueue")]
    [XmlArrayItem(Type = typeof(BingoNumber))]
    public BindableCollection<BingoNumber> BingoNumberQueue
    {
      get { return _bingoNumberQueue; }
      set
      {
        _bingoNumberQueue = value;
        QueueLength = _bingoNumberQueue.Count;
        NotifyOfPropertyChange(() => BingoNumberQueue);
      }
    }

    public bool Generating
    {
      get { return _generating; }
      set
      {
        _generating = value;
        NotifyOfPropertyChange(() => Generating);
      }
    }

    public BankoOptions BnkOptions
    {
      get { return _bnkOptions; }
      set
      {
        _bnkOptions = value;
        NotifyOfPropertyChange(() => BnkOptions);
      }
    }

    public CompetitionOptions CmpOptions
    {
      get { return _cmpOptions; }
      set
      {
        _cmpOptions = value;
        NotifyOfPropertyChange(() => CmpOptions);
      }
    }

    public VisualsOptions VsOptions
    {
      get { return _vsOptions; }
      set
      {
        _vsOptions = value;
        NotifyOfPropertyChange(() => VsOptions);
      }
    }

    public SeedInfo SInfo
    {
      get { return _seedInfo; }
      set
      {
        _seedInfo = value;
        NotifyOfPropertyChange(() => SInfo);
      }
    }

    public PlateInfo PInfo
    {
      get { return _plateInfo; }
      set
      {
        _plateInfo = value;
        NotifyOfPropertyChange(() => PInfo);
      }
    }

    public WinSettings Settings
    {
      get { return _winSettings; }
      set
      {
        _winSettings = value;
        NotifyOfPropertyChange(() => Settings);
      }
    }

    public int QueueLength
    {
      get { return _queueLength; }
      set
      {
        _queueLength = value;
        NotifyOfPropertyChange(() => QueueLength);
      }
    }

    #endregion

    public void Initialize(string seed, string title, int pladetal)
    {
      _log.Info("Starting event object initialization...");
      NumberBoard = new BingoNumberBoard();
      CompetitionList = new BindableCollection<CompetitionObject>();
      BingoNumberQueue = new BindableCollection<BingoNumber>();
      BnkOptions = new BankoOptions();
      CmpOptions = new CompetitionOptions();
      VsOptions = new VisualsOptions();
      SInfo = new SeedInfo(seed);
      PInfo = new PlateInfo();
      Settings = new WinSettings();
      //PresScreenSettings = new PresentationScreenSettings();
      EventTitle = title;
      PInfo.PlatesGenerated = pladetal;
      Settings = new WinSettings();

      _creationTime = DateTime.Now;
      NotifyOfPropertyChange(() => CreationTime);
      _initialised = true;

      _log.Info("Event object initialization done.");
    }

    private string GenerateSeedFromKeyword(string keyword)
    {
      return keyword;
    }
  }
}