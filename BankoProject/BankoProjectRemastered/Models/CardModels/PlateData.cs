using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BankoProjectRemastered.Models.GameModels;
using BankoProjectRemastered.Tools;
using Prism.Mvvm;

namespace BankoProjectRemastered.Models.CardModels
{
  [Serializable]
  class PlateData : BindableBase
  {
    private int _totalPlatesGenerated;
    private int platesActuallyUsed;
    private ObservableCollection<int[,]> plateList;

    public PlateData(List<int[,]> plates)
    {
      BnkLogger.LogLowDebugInfo("PlateData init");
      foreach (var plate in plates)
      {
        plateList.Add(plate);
      }
    }

    #region GetSet
    [XmlArray("Plates")]
    [XmlArrayItem(Type = typeof(int[,]))]
    public ObservableCollection<int[,]> PlateList
    {
      get { return plateList; }
      set { plateList = value; RaisePropertyChanged(nameof(PlateList)); }
    }

    public int TotalPlatesGenerated
    {
      get { return _totalPlatesGenerated; }
      set
      {
        _totalPlatesGenerated = value;
        RaisePropertyChanged(nameof(TotalPlatesGenerated));
      }
    }

    public int PlatesActuallyUsed
    {
      get { return platesActuallyUsed; }
      set { platesActuallyUsed = value; RaisePropertyChanged(nameof(PlatesActuallyUsed)); }
    }

    #endregion



    public bool HasPlatesBeenGenerated()
    {
      if (plateList == null)
      {
        return false;
      }

      return true;
    }
  }
}
