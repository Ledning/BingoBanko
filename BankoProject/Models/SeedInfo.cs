using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.Models
{
  /// <summary>
  /// Contains:
  ///  - Originalseed
  ///     The initial seed the user entered when creating the event. 
  ///  - Seed
  ///     What the seed is right now - it might have been altered due to reasons unknown. Maybe the output from that given seed was a really bad distribution. 
  ///  - SeedManipulated
  ///     has the seed been manipulated compared to the original seed?
  /// </summary>
  class SeedInfo : PropertyChangedBase
  {

    public SeedInfo(string originalseed)
    {
      NotifyOfPropertyChange(() => OriginalSeed);
      _originalSeed = originalseed;
    }

    private string _originalSeed;
    private string _seed;
    private bool _seedManipulated;

    public string OriginalSeed
    {
      get {return _originalSeed; }
    }

    public string Seed
    {
      get { return _seed; }
      set { _seed = value; NotifyOfPropertyChange();}
    }

    public bool SeedManipulated
    {
      get
      {
        _seedManipulated = Seed.Equals(OriginalSeed);
        NotifyOfPropertyChange(() => SeedManipulated);
        return _seedManipulated;
      }
    }
  }
}
