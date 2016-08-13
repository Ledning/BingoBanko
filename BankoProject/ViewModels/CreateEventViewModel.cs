using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using Caliburn.Micro;

namespace BankoProject.ViewModels
{
  class CreateEventViewModel : Screen
  {
    private BingoEvent _bingoEvent;

    public CreateEventViewModel()
    {





    }
    protected override void OnViewReady(object view)
    {
      _bingoEvent = IoC.Get<BingoEvent>();
    }
  }
}
