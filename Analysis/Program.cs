using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analysis
{
  class Program
  {
    static void Main(string[] args)
    {
      DistributionAnalysis dAnal = new DistributionAnalysis();

      do
      {
        Console.WriteLine("key");
        string key = Console.ReadLine();
        Console.WriteLine("amount");
        int amount = Convert.ToInt32(Console.ReadLine());


        //double[] analysisArray = dAnal.NumberAnalysis(key, amount, "pct");
        //foreach (double item in analysisArray)
        //{
        //  Console.Write(item + " ");
        //}

        double[,] analysisArrayArray = dAnal.PlacementAnalysis(key, amount, "freq");
        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 9; j++)
          {
            Console.Write("{0:0} ", analysisArrayArray[j, i]);
          }
          Console.Write("\n");
        }
        Console.ReadLine();
      }
      while (true);
    }
  }
}
