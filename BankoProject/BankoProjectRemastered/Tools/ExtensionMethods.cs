using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProjectRemastered.Models.GameModels;

namespace BankoProjectRemastered.Tools
{
  public static class ExtensionMethods
  {
    public static ObservableCollection<T> GetObservable<T>(this List<T> listColl)
    {
      return new ObservableCollection<T>(listColl);
    }
    public static List<T> GetList<T>(this ObservableCollection<T> listColl)
    {
      return new List<T>(listColl);
    }
  }
}