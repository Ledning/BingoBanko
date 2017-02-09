using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.PresentationScreen
{
  class FullscreenImageViewModel : IPresentationScreenItem
  {
    /// <summary>
    /// Contains a list of all the different pictures to be shown
    /// </summary>
    public BindableCollection<string> ImageList { get; set; }

    /// <summary>
    /// Get the images available for presenting.
    /// </summary>
    /// <remarks>
    /// Consider making this a periodic task, like every 10s or something. Or maybe bind the udpating to a certain button/action.
    /// </remarks>
    public void GetImages()
    {
    }
  }
}