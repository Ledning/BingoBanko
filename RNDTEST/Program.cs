using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmTester;
using BingoCardGenerator;
using Printer_Project;


namespace RNDTEST
{
  class Program
  {
    static void Main(string[] args)
    {
      string key;
      int pladeCount;
      
      Console.WriteLine("enter key");
      key = Console.ReadLine();
      Console.WriteLine("enter antal plader");
      pladeCount = Convert.ToInt32(Console.ReadLine());

      Generator gen = new Generator(key);

      List<int[,]> cards = gen.GenerateCard(pladeCount);
     // PDFMaker.MakePDF2Plates(cards); //make object instead
    }
  }
}
