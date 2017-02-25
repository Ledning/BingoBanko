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
    #region Fields
    private int _antalPlader;
    #endregion

    #region Constructors
    public ChangePlatesUsedDialogViewModel()
    {
      DisplayName = "Skift antal brugte plader..";
    }
    #endregion

    #region Properties
    public int AntalPlader
    {
      get { return _antalPlader; }
      set { _antalPlader = value; NotifyOfPropertyChange(()=>AntalPlader);}
    }
    #endregion

    #region Buttons
    public void OK()
    {
      TryClose(true);
    }

    public void Cancel()
    {
      TryClose(false);
    }
    #endregion
  }
}
