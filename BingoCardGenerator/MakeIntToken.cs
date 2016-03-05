using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlgorithmTester
{
  public static class TokenExtentions
  {
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
