using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNDTEST
{
  class Program
  {
    static void Main(string[] args)
    {
      int key, pladeCount;
      dankgen dkg = new dankgen();
      Console.WriteLine("enter key");
      key = (int)Convert.ToInt64(Console.ReadLine());
      Console.WriteLine("enter antal plader");
      pladeCount = (int)Convert.ToInt64(Console.ReadLine());
      for (int i = 0; i < pladeCount; i++)
      {
        int[,] firstIteration = dkg.Generator(key + i);
        int[,] lastIteration = dkg.CardFixer420(firstIteration, key);
      }
      Console.ReadLine();
    }

   
    
  }
}
