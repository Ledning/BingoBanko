using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.PresentationScreen
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
