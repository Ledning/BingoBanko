using BankoProject.Tools;
using Caliburn.Micro;

namespace BankoProject.ViewModels.PresentationScreen
{
  /// <summary>
  /// This is where you would set up all the shit, so when this is put up, the rest follows.
  /// </summary>
  class PresentationScreenHostViewModel : Conductor<IPresentationScreenItem>.Collection.OneActive
  {
    public PresentationScreenHostViewModel()
    {
      
    }




    public void ShowFullscreenImage()
    {
      //ActivateItem();
    }



  }
}
