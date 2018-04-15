using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BankoProjectRemastered.Models.CardModels;
using BankoProjectRemastered.Models.EventModels;
using BankoProjectRemastered.Models.GameModels;
using BankoProjectRemastered.Tools;
using Prism.Logging;
using Prism.Mvvm;


namespace BankoProjectRemastered.Models
{
  public enum EventParams
  {
    Default,
    Testcase,
    NormalMode
  }

  [Serializable]
  class BankoEvent : BindableBase
  {
    private BingoBoard _board;
    private SeedData _seedDataObjet;
    private PlateData _plateDataObject;
    private EventData _eventDataObject;
    private BingoNumberQueue _bingoNumberQueue;

    public BankoEvent()
    {
      BnkLogger.LogLowDebugInfo("BankoEventInitialization beginning.");
    }

    public BankoEvent(EventParams param)
    {
      switch (param)
      {
        case EventParams.Default:
          InitializeEventWithDefaultValues();
          break;
        case EventParams.Testcase:
          throw new NotImplementedException("The test values have not been defined. Maybe they should be.");
          break;
        case EventParams.NormalMode:
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(param), param, null);
      }
    }

    private void InitializeEventWithDefaultValues()
    {
      Board = new BingoBoard(90);
      SeedDataObjet = new SeedData("1234");
      PlateDataObject = new PlateData(new List<int[,]>());
      EventDataObject = new EventData("Defaultevent");
      NumberQueue = new BingoNumberQueue();
    }

    #region GetSet
    public BingoNumberQueue NumberQueue
    {
      get { return _bingoNumberQueue; }
      set { _bingoNumberQueue = value; RaisePropertyChanged(nameof(NumberQueue)); }
    }

    public EventData EventDataObject
    {
      get { return _eventDataObject; }
      set { _eventDataObject = value; RaisePropertyChanged(nameof(EventDataObject)); }
    }

    public PlateData PlateDataObject
    {
      get { return _plateDataObject; }
      set { _plateDataObject = value; RaisePropertyChanged(nameof(PlateDataObject)); }
    }

    public SeedData SeedDataObjet
    {
      get { return _seedDataObjet; }
      set { _seedDataObjet = value; RaisePropertyChanged(nameof(SeedDataObjet)); }
    }

    public BingoBoard Board
    {
      get { return _board; }
      set { _board = value; RaisePropertyChanged(nameof(Board)); }
    }
    #endregion

  }
}