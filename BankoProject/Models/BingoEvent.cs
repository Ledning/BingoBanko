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

    //names, dates, general stuff
    private string _eventTitle; //Titlen
    private DateTime _creationTime;

    //flags (has seed been manipulated, what was original seed, technical stuff
    private bool _initialised = false;
    private bool _seedManipulated; //basically isDirty
    private string _seed;//what is the seed rn? might have changed
    private string _originalSeed; // generated based on event-name, then fed into algorithm

    private int _platesGenerated; //the amount of plates generated in the beginning of the event.
    private int _platesUsed; //Whatever number of plates you wish to be generated. It is stored with the name "platesUsed", to signify that this is the amount of plates we actually use
    private bool _generating = false;
    private readonly ILog _log = LogManager.GetLog(typeof(BingoEvent));

    private BankoOptions _bnkOptions;
    private CompetitionOptions _cmpOptions;
    private VisualsOptions _vsOptions;




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
      set { _creationTime = value; NotifyOfPropertyChange(() => CreationTime);}
    }

    public bool SeedManipulated
    {
      get { return _seedManipulated; }
      set { _seedManipulated = value; NotifyOfPropertyChange(() => SeedManipulated);}
    }

    public string Seed
    {
      get { return _seed; }
      set { _seed = value;  NotifyOfPropertyChange(() => Seed);}
    }

    public string OriginalSeed
    {
      get { return _originalSeed; }
      set {
        if (_originalSeed != null)
        {
          _originalSeed = value;
          NotifyOfPropertyChange(() => OriginalSeed);
        }
        else
          _log.Info("This should not be set, and is effectively one time set only. maybe readonly works?");
      }   
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

    public int PlatesUsed
    {
      get { return _platesUsed; }
      set { _platesUsed = value; }
    }

    #endregion

    public void Initialize(string seed, string title, int pladetal)
    {
      _log.Info("Starting event object initialization...");
      _seedManipulated = false;
      _eventTitle = title;
      _originalSeed = seed;
      _platesGenerated = pladetal;
      _seed = GenerateSeedFromKeyword(_originalSeed);
      _creationTime = DateTime.Now;
      _bingoNumberBoard = new BingoNumberBoard();
      _competitionList = new BindableCollection<CompetitionObject>();
      _bingoNumberQueue = new BindableCollection<BingoNumber>();
      BnkOptions = new BankoOptions();
      CmpOptions = new CompetitionOptions();
      VsOptions = new VisualsOptions();

      for (int i = 1; i < 91; i++)
      {
        _bingoNumberQueue.Add(new BingoNumber(i));
      }
      _initialised = true;
      worker.DoWork += worker_DoWork;
      worker.RunWorkerCompleted += worker_RunWorkerCompleted;
      _log.Info("Event object initialization done.");
    }

    #region async plate generation
    private readonly BackgroundWorker worker = new BackgroundWorker();

    private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      _log.Info("Async plate-generation running...");
      _generating = true;
      Generator gen = new Generator(Seed);
      PDFMaker maker = new PDFMaker();
      maker.MakePDF(gen.GenerateCard(_platesGenerated));
    }


    private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      _generating = false;
      _log.Info("Generation done.");
    }
    #endregion

    public void GeneratePlates()
    {
      worker.RunWorkerAsync();
    }

    private string GenerateSeedFromKeyword(string keyword)
    {
      return keyword;
    }








  }
}
