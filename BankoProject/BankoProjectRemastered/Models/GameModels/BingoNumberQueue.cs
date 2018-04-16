using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BankoProjectRemastered.Interfaces;
using BankoProjectRemastered.Tools;
using Prism.Mvvm;

namespace BankoProjectRemastered.Models.GameModels
{
  [Serializable]
  class BingoNumberQueue : BindableBase, IFieldCopyAble
  {
    private List<BingoNumber> _numberQueue;

    public BingoNumberQueue()
    {
      BnkLogger.LogLowDebugInfo("BingoNumberQueue init");
    }

    #region GetSet
    public List<BingoNumber> NumberQueue
    {
      get { return _numberQueue; }
      set { _numberQueue = value; RaisePropertyChanged(nameof(NumberQueue)); RaisePropertyChanged(nameof(ObservableBingoNumberQueue)); }
    }
    #endregion


    //Observable
    public ObservableCollection<BingoNumber> ObservableBingoNumberQueue()
    {
      return NumberQueue.GetObservable();
    }

    public ObservableCollection<BingoNumber> GetLatest()
    {
      return NumberQueue.GetRange(NumberQueue.Count-10, 10).GetObservable();
    }

    public void CopyFields(object from)
    {
      throw new NotImplementedException();
    }
  }
}
