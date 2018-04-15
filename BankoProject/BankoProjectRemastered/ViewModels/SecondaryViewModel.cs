using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProjectRemastered.Interfaces;
using BankoProjectRemastered.Models;
using Prism.Mvvm;

namespace BankoProjectRemastered.ViewModels
{
  class SecondaryViewModel : BindableBase, IViewModel
  {
    public BankoEvent Event { get; set; }
    public void InitializeEvent()
    {
      throw new NotImplementedException();
    }
  }
}
