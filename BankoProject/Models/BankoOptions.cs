using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.Models
{
  public enum WinCondition
  {
    SingleRow, 
    DoubleRow,
    FullPlate
  }

  public class BankoOptions : PropertyChangedBase
  {
    private WinCondition winCondition;
    private readonly ILog _log = LogManager.GetLog(typeof(BankoOptions));

    public WinCondition Condition
    {
      get
      {
        if (SingleRow && !DoubleRow && ! FullPlate)
        {
          _log.Info("SingleRow condition");
          return WinCondition.SingleRow;
        }
        if (DoubleRow && !SingleRow && !FullPlate)
        {
          _log.Info("DoubleRow condition");
          return WinCondition.DoubleRow;
        }
        if (FullPlate && !DoubleRow && !SingleRow)
        {
          _log.Info("FullPlate condition");
          return WinCondition.FullPlate;
        }
        else
        {
          throw new ArgumentException("ERROR, no condition found / multiple conditions found, BANKOOPTIONS object");
        }
      }
    }

    public bool SingleRow
    {
      get { _log.Info("SingleRow condition"); return _singleRow; }
      set { _singleRow = value; NotifyOfPropertyChange(()=>SingleRow);}
    }

    public bool DoubleRow
    {
      get { _log.Info("DoubleRow condition"); return _doubleRow; }
      set { _doubleRow = value; NotifyOfPropertyChange(()=>DoubleRow);}
    }

    public bool FullPlate
    {
      get { _log.Info("FullPlate condition"); return _fullPlate; }
      set { _fullPlate = value; NotifyOfPropertyChange(()=>FullPlate);}
    }

    private bool _singleRow;
    private bool _doubleRow;
    private bool _fullPlate;


  }
}
