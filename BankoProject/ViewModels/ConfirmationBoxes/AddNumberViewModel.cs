using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.ViewModels.ConfirmationBoxes
{
  class AddNumberViewModel : Screen
  {
    private int _numberToAdd;


    public AddNumberViewModel()
    {
      DisplayName = "Indtast tal..";
      NumberToAdd = 1;
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
  }
}
