using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankoProject.Models
{
  class BingoCard
  {
    private BingoCardColumns _columns;
    private BingoCardRows _rows;


    public BingoCard()
    {

    }

    public BingoCardColumns Columns
    {
      get
      {
        return _columns;
      }
    }

    public BingoCardRows Rows
    {
      get
      {
        return _rows;
      }
    }
  }
}
