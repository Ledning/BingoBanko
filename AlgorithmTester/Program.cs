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
            Generator gen = new Generator("");
            var resultingcard = gen.GenerateCard();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(resultingcard[j, i] + "   ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        } 
    }
}
