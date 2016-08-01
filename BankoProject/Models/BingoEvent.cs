using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Models
{
  class BingoEvent
  {


    //names, dates, general stuff
    private string _title;
    private DateTime _startDateTime;

    //flags (has seed been manipulated, what was original seed, technical stuff
    private bool _seedManipulated; //basically isDirty
    private string _seed;//what is the seed rn? might have changed
    private string _originalSeed; // generated based on event-name, then fed into algorithm


    //any aggregated objects; settings object(general/specific), lists of objects for the competitions held during the event, 
    //private List<competitionObject> _compettionList;





  }
}
