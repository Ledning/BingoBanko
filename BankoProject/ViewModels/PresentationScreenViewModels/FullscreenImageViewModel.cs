using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Tools;

namespace BankoProject.ViewModels.PresentationScreenViewModels
{
  class FullscreenImageViewModel : IPresentationScreenItem
  {
    public ApplicationWideEnums.AspectRatio AsRatio { get; set; }
  }
}
