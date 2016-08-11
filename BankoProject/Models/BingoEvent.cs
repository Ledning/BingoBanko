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





    public void Initialize(string seed)
    {
      _seedManipulated = false;
      _originalSeed = seed;
      seed = GenerateSeedFromKeyword(_originalSeed);


      _initialised = true;
    }

    private string GenerateSeedFromKeyword(string keyword)
    {
      return keyword;
    }






  }
}
