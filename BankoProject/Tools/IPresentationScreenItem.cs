using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Tools;

namespace BankoProject.Tools
{
  /// <summary>
  /// Implement this on all items for use on the presentation screen. 
  /// </summary>
  public interface IPresentationScreenItem
  {


    ApplicationWideEnums.AspectRatio AsRatio { get; set; }




  }
}
