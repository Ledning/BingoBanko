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
    public bool Response
    {
      get
      {
        return _response;
      }

      set
      {
        _response = value;
        NotifyOfPropertyChange(() => Response);
      }
    }

    public void yResponse()
    {
      Response = true;
    }
    public void nResponse()
    {
      Response = false;
    }
  }
}
