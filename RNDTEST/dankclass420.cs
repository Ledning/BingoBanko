using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNDTEST
{
  class dankclass420
  {
    //key == kodeord + pladenummer
    //Array starting points n shit
    public  int[] FillColumn(int lowerBound, int upperBound, int key, int columnCount)
    {
      int UpperBound = upperBound + 1;
      int[] column = new int[3] { 0, 0, 0 };
      List<int> temp = new List<int>();
      int filledRows;


      Random rndColumn = new Random(key + columnCount);
      filledRows = rndColumn.Next(1, 4);


      Random rndContent = new Random(key);
      for (int count = 1; count <= filledRows; count++)
      {
        int num = rndContent.Next(lowerBound, UpperBound);
        if (!temp.Contains(num))
        {
          temp.Add(num);
        }
        else
        {
          count--;
        }

      }
      temp.Sort();

      Random rndPosition = new Random(key + filledRows);
      switch (filledRows)
      {
        case 1:
          column[rndPosition.Next(0, 3)] = temp.First();
          break;
        case 2:
          int pos = rndPosition.Next(0, 2);
          column[pos] = temp.First();
          if (pos == 0)
          {
            column[rndPosition.Next(1, 3)] = temp[1];
          }
          if (pos == 1)
          {
            column[2] = temp[1];
          }
          break;
        case 3:
          column[0] = temp[0];
          column[1] = temp[1];
          column[2] = temp[2];
          break;
        default:
          throw new ArgumentException();
          break;
      }
      return column;
    }
  }
}
