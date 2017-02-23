using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using Caliburn.Micro;

namespace BankoProject.ViewModels.ConfirmationBoxes
{
  class ChangePlatesUsedDialogViewModel : Screen
  {
    private int _antalPlader;


    public ChangePlatesUsedDialogViewModel()
    {
      DisplayName = "Skift antal brugte plader..";
    }

    public int AntalPlader
    {
      get { return _antalPlader; }
      set { _antalPlader = value; NotifyOfPropertyChange(()=>AntalPlader);}
    }


    public void OK()
    {
      TryClose(true);
    }

    public void Cancel()
    {
      TryClose(false);
    }
  }
}
