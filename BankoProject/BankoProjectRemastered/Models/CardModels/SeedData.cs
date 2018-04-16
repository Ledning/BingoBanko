using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProjectRemastered.Interfaces;
using BankoProjectRemastered.Tools;
using Prism.Mvvm;

namespace BankoProjectRemastered.Models.CardModels
{
  [Serializable]
  class SeedData : BindableBase, IFieldCopyAble
  {
    private string _seed;
    private readonly string _originalSeed;

    public SeedData(string seed)
    {
      BnkLogger.LogLowDebugInfo("SeedData init: " + seed);
      Seed = seed;
      _originalSeed = seed;
    }

    #region GetSet
    public string Seed
    {
      get { return _seed; }
      set { _seed = value; }
    }

    public string OriginalSeed
    {
      get { return _originalSeed; }
    }
    #endregion


    public bool IsSeedIntact()
    {
      if (Seed.Equals(OriginalSeed))
      {
        return true;
      }

      return false;
    }

    public void CopyFields(object from)
    {
      throw new NotImplementedException();
    }
  }
}