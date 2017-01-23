using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BankoProject;
using BingoCardGenerator;

namespace AlgorithmTester
{
  //Use this project to test the input/output of the plate generation algorithm. 
  class Program
  {
    static void Main(string[] args)
    {
      int amount = 100000;
      Generator gen = new Generator("123");
      var resultingcards = gen.GenerateCard(amount);
      //foreach (var card in resultingcards)
      //{
      //  for (int i = 0; i < 3; i++)
      //  {
      //    for (int j = 0; j < 9; j++)
      //    {
      //      Console.Write(card[j, i] + "   ");
      //    }
      //    Console.WriteLine();
      //  }
      //  Console.WriteLine();
      //  Console.WriteLine();
      //}
      List<decimal> usageList = new List<decimal>();
      for (int i = 0; i <= 90; i++)
      {
        usageList.Add(0);
      }
      foreach (var card in resultingcards)
      {
        if (card[0,0]!= -1)
        {
          for (int i = 0; i < 3; i++)
          {
            for (int j = 0; j < 9; j++)
            {
              usageList[card[j, i]]++;
            }
          }
        }
      }
      Console.WriteLine("This was supposed to produce " + gen.originalAmount + " cards.");
      Console.WriteLine(gen.DiscardedCards + "Cards have been discarded.");
      Console.WriteLine(gen.totalAmountOfCardsGenerated + "Cards have been generated.");
      Console.WriteLine(gen.validCards + " valid cards have been generated.");
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("NumberUsage statistics:");
      for (int i = 0; i <= 90; i++)
      {
        Console.WriteLine(i + " was used " + usageList[i] + " times.");
      }
      decimal total = 0;
      decimal totalDecimal = 0;
      decimal tempdec;
      for (int i = 0; i <= 90; i++)
      {
        total = total + usageList[i];
      }
      for (int i = 0; i <= 90; i++)
      {
        tempdec = (usageList[i]/total)*100;
        Console.WriteLine(i + " was used " + tempdec + " %");
        totalDecimal += tempdec;
      }
      Console.WriteLine("Total: " + totalDecimal);
      Console.ReadLine();
    }
  }
}
