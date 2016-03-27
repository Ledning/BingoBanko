using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using Caliburn.Micro;
using BankoProject.Tools;
using BankoProject.Models;

namespace BankoProject.ViewModels
{
  public sealed class ControlsScreenViewModel : Screen, IMainScreenTabItem
  {
    private OptionsFlyoutViewModel _OFVM;
    private ControlOptions _COptions;
    private BingoNumberBoard _BNBoard;
    private Random randomNumber; //use to pick new random number. Maybe make into seperate class that adjusts according to prev numbers being picked.

    public ControlsScreenViewModel()
    {
      OFVM = new OptionsFlyoutViewModel();

      DisplayName = "BingoBanko Kontrol-Skærm";
    }





    #region GetterSetter
    public OptionsFlyoutViewModel OFVM
    {
      get
      {
        return _OFVM;
      }

      set
      {
        _OFVM = value;
        NotifyOfPropertyChange(() => OFVM);
      }
    }
    public ControlOptions COptions
    {
      get
      {
        return _COptions;
      }

      set
      {
        _COptions = value;
        NotifyOfPropertyChange(() => COptions);
      }
    }
    public BingoNumberBoard BNBoard
    {
      get
      {
        return _BNBoard;
      }

      set
      {
        _BNBoard = value;
        NotifyOfPropertyChange(() => BNBoard);
      }
    }
    #endregion







  }
}
