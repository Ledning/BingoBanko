using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RNDTEST
{
  class dankgen
  {
    public int[,] Generator(int key)
    {
      int[,] plate = new int[3, 9];
      dankclass420 dnk = new dankclass420();

      for (int count = 0; count < 9; count++)
      {
        int[] column = new int[3];
        if (count == 0)
        {
          column = dnk.FillColumn(1, 10, key, count);
        }
        if (count == 8)
        {
          column = dnk.FillColumn(80, 91, key, count);
        }
        else
        {
          int lower, upper;
          lower = count * 10;
          upper = lower + 10;
          column = dnk.FillColumn(lower, upper, key, count);
        }
        for (int i = 0; i < 3; i++)
        {
          plate[i, count] = column[i];
        }
      }


      //for (int i = 0; i < 3; i++)
      //{
      //  for (int j = 0; j < 9; j++)
      //  {
      //    Console.Write(plate[i, j] + " | ");
      //  }
      //  Console.WriteLine();
      //}
      //Console.WriteLine();

      return plate;
    }

    public void CardFixer420(int[,] firstIteration, int key)
    {

      for (int row = 0; row < 3; row++)
      {
        int columnCounter = 0;

        for (int column = 0; column < 9; column++)
        {
          if (firstIteration[row, column] != 0)
          {
            columnCounter++;
          }
        }

        if (columnCounter < 5)
        {
          firstIteration = AddNumberToRow(firstIteration, row, key);
        }

        if (columnCounter > 5)
        {
          firstIteration = RemoveNumberFromRow(firstIteration, row, key);
        }
      }
    }

    public int[,] AddNumberToRow(int[,] firstIteration, int currentRow, int key)
    {
      List<int> numberOfRowsInColumns = new List<int>();
      List<int> usableRows = new List<int>();
      Random randcolumnpicker = new Random(key);
      
      for (int column = 0; column < 9; column++)
      {
        int numberOfRows = 0;

        for (int row = 0; row < 3; row++)
        {
          if (firstIteration[row, column] != 0)
          {
            numberOfRows++;
          }
        }
        numberOfRowsInColumns[column] = numberOfRows;
      }

      for (int column = 0; column < 9; column++)
      {
        if (numberOfRowsInColumns[column] == 1)
        {
          usableRows.Add(column);
        }
      }

      int chosenColumn = usableRows[randcolumnpicker.Next(0, usableRows.Count)];
      int addedNumber = 0;
      Random randNumberMade = new Random(key+chosenColumn);

      if (chosenColumn == 0)
      {
        addedNumber = randNumberMade.Next(1, 10);
      }
      else if (chosenColumn == 8)
      {
        addedNumber = randNumberMade.Next(80, 91);
      }
      else
      {
        int lower, upper;
        lower = chosenColumn * 10;
        upper = lower + 10;
        addedNumber = randNumberMade.Next(lower, upper);
      }

      int[,] nextIteration = firstIteration;
      nextIteration[currentRow, chosenColumn] = addedNumber;

      List<int> sortedRow = new List<int>();


      for (int i = 0; i < 3; i++)
      {
        sortedRow.Add(nextIteration[i,chosenColumn]);
        
      }
      int number1, number2, noNumber;

      if (sortedRow[0] == 0)
      {
        noNumber = sortedRow[0];
        number1 = sortedRow[1];
        number2 = sortedRow[2];
      }
      else if (sortedRow[1] == 0)
      {
        number1 = sortedRow[0];
        noNumber = sortedRow[1];
        number2 = sortedRow[2];
      }
      else
      {
        number1 = sortedRow[0];
        number2 = sortedRow[1];
        noNumber = sortedRow[2];
      }

      if (number1 > number2)
      {
        int tempNumber = number1;
        number1 = number2;
        number2 = number1;

        if (sortedRow[0] == 0)
        {
          sortedRow[1] = number1;
          sortedRow[2] = number2;
        }
        else if (sortedRow[1] == 0)
        {
          sortedRow[0] = number1;
          sortedRow[2] = number2;
        }
        else
        {
          sortedRow[0] = number1;
          sortedRow[1] = number2;
        }
      }

      for (int i = 0; i < 3; i++)
      {
        nextIteration[i, chosenColumn] = sortedRow[i];
      }

      return nextIteration;
    }

    public int[,] RemoveNumberFromRow(int[,] firstIteration, int currentRow, int key)
    {
      List<int> numberOfRowsInColumns = new List<int>();
      List<int> usableRows = new List<int>();
      Random randcolumnpicker = new Random(key);

      for (int column = 0; column < 9; column++)
      {
        int numberOfRows = 0;

        for (int row = 0; row < 3; row++)
        {
          if (firstIteration[row, column] != 0)
          {
            numberOfRows++;
          }
        }
        numberOfRowsInColumns[column] = numberOfRows;
      }

      for (int column = 0; column < 9; column++)
      {
        if (numberOfRowsInColumns[column] == 3)
        {
          usableRows.Add(column);
        }
      }

      int chosenColumn = usableRows[randcolumnpicker.Next(0, usableRows.Count)];
      int[,] nextIteration = firstIteration;

      nextIteration[currentRow, chosenColumn] = 0;
      return nextIteration;
    }
  }
}
