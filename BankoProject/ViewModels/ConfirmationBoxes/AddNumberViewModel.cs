using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using Caliburn.Micro;

namespace BankoProject.ViewModels.ConfirmationBoxes
{
  class AddNumberViewModel : Screen, IDataErrorInfo
  {
    private int _numberToAdd;
    private string _error;
    private BingoEvent Event;

    public AddNumberViewModel()
    {
      DisplayName = "Indtast tal..";
      NumberToAdd = 1;
      Event = IoC.Get<BingoEvent>();
    }

    public void OK()
    {
      TryClose(true);
    }

    public void Cancel()
    {
      TryClose(false);
    }


    //TODO: InputRestriktion: må kun indtaste mellem 1 og 90


    public int NumberToAdd
    {
      get { return _numberToAdd; }
      set { _numberToAdd = value; NotifyOfPropertyChange(() => NumberToAdd); }
    }

    #region Implementation of IDataErrorInfo

    public string this[string columnName]
    {
      get
      {
        string result = null;
        if (columnName == "NumberToAdd")
        {
          if (NumberToAdd <= 0)
          {
            result = "Tallet må ikke være 0 eller under.";
            return result;
          }
          if (NumberToAdd > 90)
          {
            result = "Tallet må ikke være over 90.";
            return result;
          }
          if (!CheckIfNumberIsPicked(NumberToAdd))
          {
            
          }



        }
        return result;
      }
    }

    /// <summary>
    /// Returns false if the number is picked.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public bool CheckIfNumberIsPicked(int number)
    {
      if (Event.)
      {
        
      }
      for (int i = 0; i < R; i++)
      {
        
      }


      return true;
    }

    public string Error
    {
      get { return _error; }
    }

    #endregion
  }
}
