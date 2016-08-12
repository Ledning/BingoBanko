using System;
using System.Collections.Generic;
using Caliburn.Micro;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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


    private BingoNumberBoard _bingoNumberBoard;


    //any aggregated objects; settings object(general/specific), lists of objects for the competitions held during the event, 
    private BindableCollection<CompetitionObject> _competitionList;
    private BindableCollection<BingoNumber> _bingoNumbers;




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

    public BindableCollection<BingoNumber> BingoNumbers
    {
      get { return _bingoNumbers; }
      set { _bingoNumbers = value; NotifyOfPropertyChange(() => BingoNumbers);}
    }
#endregion

    public void Initialize(string seed, string title)
    {
      _seedManipulated = false;
      _originalSeed = seed;
      _seed = GenerateSeedFromKeyword(_originalSeed);
      _eventTitle = title;
      _creationTime = DateTime.Now;
      _bingoNumberBoard = new BingoNumberBoard();
      _competitionList = new BindableCollection<CompetitionObject>();
      _bingoNumbers = new BindableCollection<BingoNumber>();

      for (int i = 1; i < 91; i++)
      {
        _bingoNumbers.Add(new BingoNumber(i));
      }


      _initialised = true;
    }

    private string GenerateSeedFromKeyword(string keyword)
    {
      return keyword;
    }








  }
}
