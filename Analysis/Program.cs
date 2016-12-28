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
        Console.WriteLine("123");
        string key = "123";
        int amount = 10000;

        Console.WriteLine("Tallene 1-90 bliver brugt procentvis så mange gange, i rækkefølge: ");
        double[] analysisArray = dAnal.NumberAnalysis(key, amount, "pct"); //Numberanalysis
        foreach (double item in analysisArray)
        {
          Console.Write(item.ToString(("#.##")) + " ");
        }
      Console.WriteLine("\nPlacement Analysis:");
       double[,] analysisArrayArray = dAnal.PlacementAnalysis(key, amount, "freq"); //Placementanalysis
        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 9; j++)
          {
            Console.Write("{0:0} ", analysisArrayArray[j, i].ToString(("#.##")));
          }
          Console.Write("\n");
        }
        
        Console.WriteLine("Endetalsanalyse: ");
        double[] analysisArraytwo = dAnal.EndNumberAnalysis(key, amount, "pct"); //endNumberAnalysis
        foreach (double item in analysisArraytwo)
        {
          Console.Write("{0:0.00} ", item);
        }
        
        
        Console.ReadLine();
      }
      while (true);
    }
  }
}
