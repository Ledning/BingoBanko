using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlgorithmTester
{
  public static class AlgorithmExtentions
  {
    public static int[,] DiscardCard(this int[,] Card)
    {
      for (int i = 0; i < Card.GetLength(0); i++)
      {
        for (int j = 0; j < Card.GetLength(1); j++)
        {
          Card[i, j] = 0;
        }
      }
      Card[0, 0] = -1;
      return Card;
    }

    public static bool ContainsValue(this int[,] Card, int compareValue)
    {
      int cardRows = Card.GetLength(1);
      int cardColumns = Card.GetLength(0);
      for (int i = 0; i < cardRows; i++)
      {
        for (int j = 0; j < cardColumns; j++)
        {
          if (Card[j,i] == compareValue)
          {
            return true;
          }
        }
      }
      return false;
    }
    public static bool IsValidCard(this int[,] Card)
    {
      int cardRows = Card.GetLength(1);
      int cardColumns = Card.GetLength(0);
      for (int i = 0; i < cardRows; i++)
      {
        int columnCounter = 0;
        for (int j = 0; j < cardColumns; j++)
        {
          if (Card[j, i] != 0)
          {
            columnCounter++;
          }
        }
        if (columnCounter != 5)
        {
          return false;
        }
      }
      return true;
    }
    public static int MakeInt(this String str)
    {
      try
      {
          if (str == "")
          {
              throw new ArgumentException("String was empty");
          }
        char[] charArray;
        str = str.ToLower();
        long longChecker = 0;
        int finalNumba = 0;
        string stringChecker = "";
        Regex regx = new Regex("([a-zæøå0-9])+");
        if(regx.IsMatch(str))
        {
          charArray = str.ToCharArray();
          foreach (char item in charArray)
          {
            stringChecker += (Convert.ToInt64(item)).ToString();
            if (Convert.ToInt64(stringChecker) >= Int32.MaxValue - 1000)
            {
              longChecker = Convert.ToInt64(stringChecker) % (Int32.MaxValue - 1000);
              stringChecker = Convert.ToString(longChecker);
            }
          }
          
        }
        return finalNumba = Convert.ToInt32(stringChecker);
      }
      catch (ArgumentException e)
      {
        throw e;
      }
     
      
    }
  }
}
