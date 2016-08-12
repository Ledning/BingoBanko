﻿using System;
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

    #endregion

    public void Initialize(string seed, string title)
    {
      _seedManipulated = false;
      _eventTitle = title;
      _originalSeed = seed;
      _seed = GenerateSeedFromKeyword(_originalSeed);
      _creationTime = DateTime.Now;
      _bingoNumberBoard = new BingoNumberBoard();
      _competitionList = new BindableCollection<CompetitionObject>();
      _bingoNumberQueue = new BindableCollection<BingoNumber>();

      for (int i = 1; i < 91; i++)
      {
        _bingoNumberQueue.Add(new BingoNumber(i));
      }
      _initialised = true;
      worker.DoWork += worker_DoWork;
      worker.RunWorkerCompleted += worker_RunWorkerCompleted;
    }

    #region async plate generation
    private readonly BackgroundWorker worker = new BackgroundWorker();

    private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      Generator gen = new Generator(Seed);
      PDFMaker maker = new PDFMaker();
      maker.MakePDF(gen.GenerateCard(_platesGenerated));
      
    }


    private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      _generating = true;
    }




    #endregion


    private string GenerateSeedFromKeyword(string keyword)
    {
      return keyword;
    }








  }
}
