using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProjectRemastered.Interfaces
{
  interface IFieldCopyAble
  {
    /// <summary>
    /// Should update each field of the data object with the corresponding field in the from parameter
    /// </summary>
    /// <param name="from"></param>
    void CopyFields(object from);
  }
}
