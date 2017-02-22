using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BankoProject.ViewModels.ConfirmationBoxes
{
  class ChangePlatesUsedDialogViewModel : Screen
  {

    public ChangePlatesUsedDialogViewModel()
    {
      DisplayName = "Skift antal brugte plader..";
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
