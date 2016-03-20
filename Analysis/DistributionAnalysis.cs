using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingoCardGenerator;


namespace Analysis
{
  class DistributionAnalysis
  {
    const int rows = 3;
    const int columns = 9;
    const int numbersOnCards = 90;

    public double[,] PlacementAnalysis(string key, int amount, string mode)
    {
      int totalNumbers = 0;
      

      double[,] placeFreq = new double[columns, rows];
      double[,] placePct = new double[columns, rows];
      for (int columnCount = 0; columnCount < columns; columnCount++)
      {
        for (int rowCount = 0; rowCount < rows; rowCount++)
        {
          placeFreq[columnCount, rowCount] = 0;
        }
      }


      Generator gen = new Generator(key); 
      List<int[,]> analysedCards = gen.GenerateCard(amount);
      foreach (int[,] item in analysedCards)
      {
        if (item[0, 0] != -1)
        {
          for (int columnCount = 0; columnCount < columns; columnCount++)
          {
            for (int rowCount = 0; rowCount < rows; rowCount++)
            {
              if (item[columnCount, rowCount] != 0)
              {
                placeFreq[columnCount,rowCount]++;
                totalNumbers++;
              }
            }
          }
        }
      }

      if (mode == "freq")
        return placeFreq;
      else if (mode == "pct")
      {
        for (int columnCount = 0; columnCount < columns; columnCount++)
        {
          for (int rowCount = 0; rowCount < rows; rowCount++)
          {
            placePct[columnCount, rowCount] = (placeFreq[columnCount, rowCount] / totalNumbers) * 100;

          }
        }
        return placePct;
      }
      return placePct; //Volvo pls fix
    }

    public double[] NumberAnalysis(string key, int amount, string mode)
    {
      int totalNumbers = 0;
      double[] numberFreq = new double[numbersOnCards];
      double[] numberPct = new double[numbersOnCards];

      for (int i = 0; i < numbersOnCards; i++)
      {
        numberFreq[i] = 0;
        numberPct[i] = 0;
      }

      Generator gen = new Generator(key);
      List<int[,]> analysedCards = gen.GenerateCard(amount);
      foreach (int[,] item in analysedCards)
      {
        if (item[0,0] != -1)
        {
          for (int column = 0; column < 9; column++)
          {
            for (int row = 0; row < 3; row++)
            {
              if (item[column, row] != 0)
              {
                numberFreq[item[column, row] - 1]++;
                totalNumbers++;
              }
            }
          }
        }
      }

      if (mode == "freq")
        return numberFreq;
      else if (mode == "pct")
      {
        for (int i = 0; i < numbersOnCards; i++)
        {
          numberPct[i] = (numberFreq[i] / totalNumbers) * 100;
        }
        return numberPct;
      }
      else
        return numberPct; //volvo pls fix     
    }
  }
}
