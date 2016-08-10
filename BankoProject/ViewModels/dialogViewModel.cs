using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.ViewModels
{
  class dialogViewModel : Screen
  {
    private bool _response = false;
    private string _text;

    public dialogViewModel(string text)
    {
      Text = text;
      DisplayName = "Bekræft Handling";
    }

    public string Text
    {
      get
      {
        return _text;
      }

      set
      {
        _text = value;
        NotifyOfPropertyChange(() => Text);
      }
    }

    public void AcceptButton()
    {
      TryClose(true);
    }

    public void CancelButton()
    {
      TryClose(false);
    }




    //HOW TO USE THIS PIECE OF SHIT? LOOOK NO FURTHER
    //Same procedure if the result was != true
    //Og, hvis du vil bruge denne i en viewmodel, skal den have en instans af WindowManageren sendt med. 
    /*
    bool? result = winMan.ShowDialog(new dialogViewModel("Bekræft tilføjelse: " + number));
      if (result != null)
      {
        if (result.Value)
        {
          //Do some shit here
        }
      }






    */
  }
}
