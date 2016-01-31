using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Models
{
  class BingoCardRows
  {
    #region Fields
    private int[] _row1;
    private int[] _row2;
    private int[] _row3;

    private List<int[]> _rows;
    #endregion


    public BingoCardRows(int[][] Rows)
    {
      _row1 = Rows[0];
      _rows.Add(Row1);
      _row2 = Rows[1];
      _rows.Add(Row2);
      _row3 = Rows[2];
      _rows.Add(Row3);
    }
    #region Methods
    public int Count()
    {
      int count = 0;
      foreach (var row in _rows)
      {
        for (int i = 0; i < 9; i++)
        {
          if (row[i] != 0)
          {
            count++;
          }
        }
      }
      return count;
    }
    #endregion
    #region Accessors
    public int[] Row1
    {
      get
      {
        return _row1;
      }
    }

    public int[] Row2
    {
      get
      {
        return _row2;
      }
    }

    public int[] Row3
    {
      get
      {
        return _row3;
      }
    }
    #endregion
  }
}
