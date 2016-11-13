using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.PresentationScreenViewModels
{
  /// <summary>
  /// This is where you would set up all the shit, so when this is put up, the rest follows.
  /// </summary>
  class PresentationScreenHostViewModel : Conductor<IPresentationScreenItem>.Collection.OneActive
  {





    public void ShowFullscreenImage()
    {
      ActiveItem
    }



  }
}
