using System;
using System.Collections.Generic;
using Caliburn.Micro;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using BingoCardGenerator;
using Printer_Project;

namespace BankoProject.Models
{
  public class BingoEvent : PropertyChangedBase
  {
    private string _eventTitle;
    private DateTime _creationTime;

    private bool _initialised = false;
    private bool _generating = false;

    private readonly ILog _log = LogManager.GetLog(typeof(BingoEvent));

    private BankoOptions _bnkOptions;
    private CompetitionOptions _cmpOptions;
    private VisualsOptions _vsOptions;
    private SeedInfo _seedInfo;
    private PlateInfo _plateInfo;




    //any aggregated objects; settings object(general/specific), lists of objects for the competitions held during the event, 
    private BindableCollection<CompetitionObject> _competitionList; //A list of all the competitions during the game
    private BindableCollection<BingoNumber> _bingoNumberQueue; //the numbers picked in the game, input into this list as they come in. 
    private BingoNumberBoard _bingoNumberBoard; //The bingo board, however it might look during the game




    #region GetterSetter
    public string EventTitle
    {
      get { return _eventTitle; }
      set { _eventTitle = value; NotifyOfPropertyChange(EventTitle);}
    }

    public DateTime CreationTime
    {
      get { return _creationTime; }
    }

    public BingoNumberBoard NumberBoard
    {
      get { return _bingoNumberBoard; }
      set { _bingoNumberBoard = value; NotifyOfPropertyChange(() => NumberBoard);}
    }

    public BindableCollection<CompetitionObject> CompetitionList
    {
      get { return _competitionList; }
      set { _competitionList = value; NotifyOfPropertyChange(() => CompetitionList);}
    }

    public BindableCollection<BingoNumber> BingoNumberQueue
    {
      get { return _bingoNumberQueue; }
      set { _bingoNumberQueue = value; NotifyOfPropertyChange(() => BingoNumberQueue);}
    }

    public bool Generating
    {
      get { return _generating; }
      set {_generating = value; NotifyOfPropertyChange(() => Generating); }
    }

    public BankoOptions BnkOptions
    {
      get { return _bnkOptions; }
      set { _bnkOptions = value; NotifyOfPropertyChange(() => BnkOptions);}
    }

    public CompetitionOptions CmpOptions
    {
      get { return _cmpOptions; }
      set { _cmpOptions = value; NotifyOfPropertyChange(() => CmpOptions);}
    }

    public VisualsOptions VsOptions
    {
      get { return _vsOptions; }
      set { _vsOptions = value; NotifyOfPropertyChange(() => VsOptions);}
    }

    public SeedInfo SInfo
    {
      get { return _seedInfo; }
      set { _seedInfo = value; }
    }

    public PlateInfo PInfo
    {
      get { return _plateInfo; }
      set { _plateInfo = value; }
    }

    #endregion

    public void Initialize(string seed, string title, int pladetal)
    {
      _log.Info("Starting event object initialization...");
      _eventTitle = title;
      PInfo.PlatesGenerated = pladetal;
      SInfo.Seed = GenerateSeedFromKeyword(SInfo.OriginalSeed);
      _creationTime = DateTime.Now;
      _bingoNumberBoard = new BingoNumberBoard();
      _competitionList = new BindableCollection<CompetitionObject>();
      _bingoNumberQueue = new BindableCollection<BingoNumber>();
      BnkOptions = new BankoOptions();
      CmpOptions = new CompetitionOptions();
      VsOptions = new VisualsOptions();
      SInfo = new SeedInfo(seed);

      for (int i = 1; i < 91; i++)
      {
        _bingoNumberQueue.Add(new BingoNumber(i));
      }
      _initialised = true;
      
      _log.Info("Event object initialization done.");
    }


    private string GenerateSeedFromKeyword(string keyword)
    {
      return keyword;
    }








  }
}
