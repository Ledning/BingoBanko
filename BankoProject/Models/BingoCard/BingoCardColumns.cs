using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Models
{
  class BingoCardColumns
  {
    #region Fields
    private int[] _column1;
    private int[] _column2;
    private int[] _column3;
    private int[] _column4;
    private int[] _column5;
    private int[] _column6;
    private int[] _column7;
    private int[] _column8;
    private int[] _column9;

    private List<int[]> _columns;
    #endregion

    //Input til disse columns, kan kun ske via constructor, aka de er immutable. Sørger for at der aldrig bliver noget fis med at overskrive ting un-intentionally.
    //Det gælder også for BingoCardRows
    public BingoCardColumns(int[][] Columns)
    {
      _column1 = Columns[0];
      _columns.Add(Column1);
      _column2 = Columns[1];
      _columns.Add(Column2);
      _column3 = Columns[2];
      _columns.Add(Column3);
      _column4 = Columns[3];
      _columns.Add(Column4);
      _column5 = Columns[4];
      _columns.Add(Column5);
      _column6 = Columns[5];
      _columns.Add(Column6);
      _column7 = Columns[6];
      _columns.Add(Column6);
      _column8 = Columns[7];
      _columns.Add(Column7);
      _column9 = Columns[8];
      _columns.Add(Column8);
    }

    #region Methods
    public int Count()
    {
      int count = 0;
      foreach (var column in _columns)
      {
        for (int i = 0; i < 3; i++)
        {
          if (column[i] != 0)
          {
            count++;
          }
        }
      }
      return count;
    }
    #endregion
    #region Accessors
    public int[] Column1
    {
      get
      {
        return _column1;
      }
    }

    public int[] Column2
    {
      get
      {
        return _column2;
      }
    }

    public int[] Column3
    {
      get
      {
        return _column3;
      }
    }

    public int[] Column4
    {
      get
      {
        return _column4;
      }
    }

    public int[] Column5
    {
      get
      {
        return _column5;
      }
    }

    public int[] Column6
    {
      get
      {
        return _column6;
      }
    }

    public int[] Column7
    {
      get
      {
        return _column7;
      }
    }

    public int[] Column8
    {
      get
      {
        return _column8;
      }
    }

    public int[] Column9
    {
      get
      {
        return _column9;
      }
    }
    #endregion
  }
}
