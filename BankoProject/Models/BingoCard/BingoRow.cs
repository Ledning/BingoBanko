using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Models
{
  class BingoRow
  {
    private int[] _row;

    public BingoRow(int[] row)
    {

      _row = row;
    }









    public int[] Row
    {
      get
      {
        return _row;
      }
    }
  }
}
