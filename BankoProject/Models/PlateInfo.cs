using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BingoCardGenerator;
using Caliburn.Micro;

namespace BankoProject.Models
{
  /// <summary>
  /// A class that holds all the information about the plates, how many did we print how many were actually used and so on
  /// </summary>
  [Serializable]
  public class PlateInfo : PropertyChangedBase
  {
    private int _platesGenerated;
    private int _platesUsed;

    [XmlIgnore]
    private Generator _cardGenerator;

    [XmlIgnore]
    public List<int[,]> CardList;

    public PlateInfo()
    {
      CardList = new List<int[,]>();
    }

    public int PlatesGenerated
    {
      get { return _platesGenerated; }
      set { _platesGenerated = value; NotifyOfPropertyChange(()=>PlatesGenerated);}
    }
    public int PlatesUsed
    {
      get { return _platesUsed; }
      set { _platesUsed = value; NotifyOfPropertyChange(()=>PlatesUsed);}
    }

    [XmlIgnore]
    public Generator CardGenerator
    {
      get { return _cardGenerator; }
      set { _cardGenerator = value;}
    }
  }
}
