using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProjectRemastered.Models;

namespace BankoProjectRemastered.Interfaces
{
  interface IViewModel
  {
    BankoEvent Event { get; set; }

    void InitializeEvent();
  }
}
