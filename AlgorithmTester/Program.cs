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
      int amount = 1;
      Generator gen = new Generator("123");
      var resultingcards = gen.GenerateCard(amount);
      foreach (var card in resultingcards)
      {
        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 9; j++)
          {
            Console.Write(card[j, i] + "   ");
          }
          Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine();
      }
      Console.WriteLine("This was supposed to produce " + gen.originalAmount + " cards.");
      Console.WriteLine(gen.DiscardedCards + "Cards have been discarded.");
      Console.WriteLine(gen.totalAmountOfCardsGenerated + "Cards have been generated.");
      Console.WriteLine(gen.validCards + " valid cards have been generated.");
      Console.ReadLine();
    }
  }
}
