using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.PresentationScreenViewModels
{
  class FullscreenImageViewModel : IPresentationScreenItem
  {
    public ApplicationWideEnums.AspectRatio AsRatio { get; set; }


    /// <summary>
    /// Contains a list of all the different pictures to be shown
    /// </summary>
    public BindableCollection<string> ImageList { get; set; }
  }
}
