using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Models
{
  /// <summary>
  /// A class that holds all the information about the plates, how many did we print how many were actually used and so on
  /// </summary>
  public class PlateInfo
  {
    private int _platesGenerated;
    private int _platesUsed;


    public int PlatesGenerated
    {
      get { return _platesGenerated; }
      set { _platesGenerated = value; }
    }

    public int PlatesUsed
    {
      get { return _platesUsed; }
      set { _platesUsed = value; }
    }
  }
}
