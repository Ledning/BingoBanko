using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;

using Caliburn.Micro;
using TestProject.Tools;
using BingoEvent = TestProject.Models.BingoEvent;
using BingoNumber = TestProject.Models.BingoNumber;

namespace TestProject
{
  public class Program
  {
    enum WhatToTest
    {
      NumberOfNumToOneRow = 0,
      NumberOfNumToPlate = 1,

    }

    static void Main(string[] args)
    {
      WhatToTest condition = WhatToTest.NumberOfNumToOneRow;
      //TestSeeds(WhatToTest.NumberOfNumToOneRow, 500, 500);
      //TestSeeds(WhatToTest.NumberOfNumToPlate, 500, 500);
      TestAmountOfPlatesWithBingoAtOnce(500,500);
    }

    private static void TestAmountOfPlatesWithBingoAtOnce(int AmountOfSeedsToTest, int PlatesPerEvent)
    {
      var random = new Random();
      using (CsvFileWriter writer = new CsvFileWriter("TestAmountsOfPlatesWithBingo.csv"))
      {
        CsvRow row = new CsvRow();
        row.Add("Seed");
        row.Add("AmountOfPlatesWithBingo");
        writer.WriteRow(row);
        for (int i = 0; i < AmountOfSeedsToTest; i++)
        {
          BingoEvent TestEvent = new BingoEvent();
          TestEvent.Initialize(RandomSeed(random), "Test nr." + i, PlatesPerEvent);
          TestEvent.PInfo.CardList = TestEvent.PInfo.CardGenerator.GenerateCard(TestEvent.PInfo.PlatesGenerated);
          Random rdnForEvents = new Random();
          int AmountOfPlatesWithBingo = 0;
          while (!RunTestEvent(3, TestEvent, ref AmountOfPlatesWithBingo))
          {
            PickRandomNumber(TestEvent, rdnForEvents);
          }
          //Write here
          CsvRow resultRow = new CsvRow();
          row.Add(TestEvent.SInfo.OriginalSeed);
          row.Add(AmountOfSeedsToTest.ToString());
          writer.WriteRow(row);
        }
      }
    }

    private static void TestSeeds(WhatToTest condition, int AmountOfSeedsToTest, int PlatesPerEvent)
    {
      var random = new Random();
      if (condition == WhatToTest.NumberOfNumToOneRow)
      {
        using (CsvFileWriter writer = new CsvFileWriter("TestOfOneRow.csv"))
        {
          CsvRow row = new CsvRow();
          row.Add("Seed");
          row.Add("AntalTal");
          writer.WriteRow(row);
          for (int i = 0; i < AmountOfSeedsToTest; i++)
          {
            BingoEvent TestEvent = new BingoEvent();
            TestEvent.Initialize(RandomSeed(random), "Test nr." + i, PlatesPerEvent);
            TestEvent.PInfo.CardList = TestEvent.PInfo.CardGenerator.GenerateCard(TestEvent.PInfo.PlatesGenerated);
            Random rdnForEvents = new Random();
            int numbersNeededForCondition = 0;
            while (!RunTestEvent(1, TestEvent))
            {
              PickRandomNumber(TestEvent, rdnForEvents);
              numbersNeededForCondition++;
            }
            //Write here
            CsvRow resultRow = new CsvRow();
            row.Add(TestEvent.SInfo.OriginalSeed);
            row.Add(numbersNeededForCondition.ToString());
            writer.WriteRow(row);
          }
        }
        
        

      }
      else if (condition == WhatToTest.NumberOfNumToPlate)
      {
        using (CsvFileWriter writer = new CsvFileWriter("TestOfPlate.csv"))
        {
          CsvRow row = new CsvRow();
          row.Add("Seed");
          row.Add("AntalTal");
          writer.WriteRow(row);
          for (int i = 0; i < AmountOfSeedsToTest; i++)
          {
            BingoEvent TestEvent = new BingoEvent();
            TestEvent.Initialize(RandomSeed(random), "Test nr." + i, PlatesPerEvent);
            TestEvent.PInfo.CardList = TestEvent.PInfo.CardGenerator.GenerateCard(TestEvent.PInfo.PlatesGenerated);
            Random rdnForEvents = new Random();
            int numbersNeededForCondition = 0;
            while (!RunTestEvent(3, TestEvent))
            {
              PickRandomNumber(TestEvent, rdnForEvents);
              numbersNeededForCondition++;
            }
            //Write here
            CsvRow resultRow = new CsvRow();
            row.Add(TestEvent.SInfo.OriginalSeed);
            row.Add(numbersNeededForCondition.ToString());
            writer.WriteRow(row);
          }
        }
      }
    }

    private static void PickRandomNumber(BingoEvent TestEvent, Random rdn)
    {
      BindableCollection<BingoNumber> numberList = new BindableCollection<BingoNumber>();
      foreach (BingoNumber number in TestEvent.NumberBoard.Board)
      {
        if (!number.IsPicked)
        {
          numberList.Add(number);
        }
      }
      TestEvent.AvailableNumbersQueue = numberList;
      int rdnnumber = rdn.Next(0, TestEvent.AvailableNumbersQueue.Count);
      if (TestEvent.AvailableNumbersQueue.Count > 0)
      {
        if (!TestEvent.NumberBoard.Board[TestEvent.AvailableNumbersQueue[rdnnumber].Value - 1].IsPicked)
        {
          try
          {
            //_log.Info(Event.NumberBoard.Board[Event.AvailableNumbersQueue[rdnnumber].Value - 1].Value.ToString());
            TestEvent.NumberBoard.Board[TestEvent.AvailableNumbersQueue[rdnnumber].Value - 1].IsPicked = true;
            TestEvent.NumberBoard.Board[TestEvent.AvailableNumbersQueue[rdnnumber].Value - 1].IsChecked = false;
            TestEvent.BingoNumberQueue.Add(TestEvent.NumberBoard.Board[TestEvent.AvailableNumbersQueue[rdnnumber].Value - 1]);

          }
          catch (Exception ex)
          {
            //_log.Info(rdnnumber.ToString());
            //_log.Info(Event.AvailableNumbersQueue.Count.ToString());
            //_log.Info("Exception in random numb!");
          }
          return;
        }
        PickRandomNumber(TestEvent, rdn);
      }
    }

    private static bool RunTestEvent(int condition, BingoEvent Event)
    {
      //int[,] chosenPlate = Event.PInfo.CardList[_plateToCheck];

      int rules = condition;
      int i = 0;
      foreach (var chosenPlate in Event.PInfo.CardList)
      {

        bool rowFailed = false;
        int winRows = 0;
        for (int rows = 0; rows < 3; rows++)
        {
          rowFailed = false;

          for (int columns = 0; columns < 9; columns++)
          {
            if (chosenPlate[0, 0] == -1)
            {
              rowFailed = true;
              break;
            }
            if (chosenPlate[columns, rows] != 0)
            {
              if (chosenPlate[columns, rows] != Event.NumberBoard.Board[chosenPlate[columns, rows] - 1].Value)
              {


                rowFailed = true;
                break;
                //i believe this can be removed? these values will always be equal afaik. maybe leave in as dumb check for errors
              }
              if (!Event.NumberBoard.Board[chosenPlate[columns, rows] - 1].IsPicked)
              {
                //MMMM NNESTING
                rowFailed = true;
                break;
              }
            }
          }

          if (!rowFailed)
            winRows++;
        }

        if (winRows >= rules)
        {
          /*WIR HABEN BINGO MOTHERFUCKERS!!!*/
          //Show window with whether or not plate has bingo? to prevent overtyping and misunderstandings in the gui. I think that would be cool.

          //if it does not have bingo, show window with the missing numbers. crosscheck with board.
          //Consider maybe showing the plate. otherwise it has to be fail-tested a lot
          return true;
        }
        else
        {
          //nothing, this method is for testing banko
        }
        i++;
      }
      return false;
    }

    private static bool RunTestEvent(int condition, BingoEvent Event, ref int numberOfPlatesWithBingo)
    {
      //int[,] chosenPlate = Event.PInfo.CardList[_plateToCheck];

      int rules = condition;
      int i = 0;
      int numberofplates = 0;
      foreach (var chosenPlate in Event.PInfo.CardList)
      {

        bool rowFailed = false;
        int winRows = 0;
        for (int rows = 0; rows < 3; rows++)
        {
          rowFailed = false;

          for (int columns = 0; columns < 9; columns++)
          {
            if (chosenPlate[0, 0] == -1)
            {
              rowFailed = true;
              break;
            }
            if (chosenPlate[columns, rows] != 0)
            {
              if (chosenPlate[columns, rows] != Event.NumberBoard.Board[chosenPlate[columns, rows] - 1].Value)
              {


                rowFailed = true;
                break;
                //i believe this can be removed? these values will always be equal afaik. maybe leave in as dumb check for errors
              }
              if (!Event.NumberBoard.Board[chosenPlate[columns, rows] - 1].IsPicked)
              {
                //MMMM NNESTING
                rowFailed = true;
                break;
              }
            }
          }

          if (!rowFailed)
            winRows++;
        }

        if (winRows >= rules)
        {
          /*WIR HABEN BINGO MOTHERFUCKERS!!!*/
          //Show window with whether or not plate has bingo? to prevent overtyping and misunderstandings in the gui. I think that would be cool.

          //if it does not have bingo, show window with the missing numbers. crosscheck with board.
          //Consider maybe showing the plate. otherwise it has to be fail-tested a lot
          numberofplates++;
        }
        else
        {
          //nothing, this method is for testing banko
        }
        i++;
      }
      if (numberofplates != 0)
      {
        numberOfPlatesWithBingo = numberofplates;
        return true;
      }
      numberOfPlatesWithBingo = 0;
      return false;
    }

    private static string RandomSeed(Random random)
    {
      var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
      var stringChars = new char[8];


      for (int i = 0; i < stringChars.Length; i++)
      {
        stringChars[i] = chars[random.Next(chars.Length)];
      }

      var finalString = new String(stringChars);
      return finalString;
    }
  }
}