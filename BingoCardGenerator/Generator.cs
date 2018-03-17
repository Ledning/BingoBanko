using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using AlgorithmTester;
using StarMathLib;

namespace BingoCardGenerator
{
  public class Generator : IGenerator
  {
    private Int32 GENERATIONTOKEN;
    private const int cardRows = 3, cardColumns = 9; //Card size. Change for other sizes. Remember to change the code as well.
    public int DiscardedCards = 0;
    public int originalAmount = 0;
    public int totalAmountOfCardsGenerated = 0;
    public int validCards = 0;



    //Takes in the key, converts it to an int representation of the string, and stores it as a token to be reused by the algorithm.
    public Generator(string key)
    {
      GENERATIONTOKEN = key.MakeInt();
    }

    //Call this function with arbitrary number of amount to generate arbitrary amount of cards.

    public List<int[,]> GenerateCard(int amountOfCards)
    {
      
      originalAmount = amountOfCards;
      List<int[,]> Cards = new List<int[,]>();
      for (int i = 0; i < amountOfCards; i++)
      {
        GENERATIONTOKEN += i;
        Random rndContent = new Random(GENERATIONTOKEN);
        int[,] Card;
        Card = GenerateUncleanedCard(rndContent);
        Card = CleanCard(Card, rndContent);

        if (Card[0,0] == -1)
        {
          amountOfCards++;
        }
        else
        {
          validCards++;
        }
        Cards.Add(Card);
      }
      totalAmountOfCardsGenerated = amountOfCards;
      return Cards;
    }

    /* The next couple of methods perform the initial generation of the bingocard, aka an uncleaned version, and further clean the card so it matches the rules for bingocards.
         * The size of the card is determined by the cardRows and cardColumns vars. 9x3 by default. TESTING UNDONE: RESULTS UNCERTAIN ATM. ARRAY BOUNDARIES UNCERTAIN
         * GenerateUncleanedCard:
         * Forloop #1: generate 9 columns. 
         * if(count == 0) or  == 8: to handle that the edge columns of the card has an odd amount of values. First column 1-9, last column: 80-90. Pushed by 1 to let arrays be arrays
         * Forloop #2: add it back to main card. Exception handling in there because i hate arrays. Assertions/testproject should prolly be done
         * FillColumn:
         * Forloop #1: generates a random amount of numbers, between 1-3. Store it in a temporary list so that it might be sorted, so we ensure that values come in the right order on the card.
         * Returns an array with the numbers that should be in this given column, not positioned. The positioning is done by CleanCard.
         * The card returned by GenerateUncleanedCard should have these: Each column has numbers in its own interval. The numbers in a column should be internally sorted, but not positioned. The columns come in increasing order.
         * CleanCard:
         * Perform "cleaning" of a recently generated card, so that it adheres to the rules of a bingo card.
         * Right now the only thing in there is the fixing of positions
         *   
         * RandNumPosInColumn:
         * Assigns random positions to numbers inside an array of *cardRows* size.
         */



    private int[,] GenerateUncleanedCard(Random rndContent)
    {
      int[,] card = new int[cardColumns, cardRows];
      

      try
      {
        
        for (int count = 0; count < cardColumns; count++)
        {
          int[] column = new int[cardRows] {0, 0, 0};
          if (count == 0)
          {
            column = FillColumn(1, 9, count, rndContent);
          }
          else if (count == 8)
          {
            column = FillColumn(80, 90, count, rndContent);
          }
          else
          {
            int lower, upper;
            lower = count*10;
            upper = lower + 9;
            column = FillColumn(lower, upper, count, rndContent);
          }
          for (int i = 0; i < cardRows; i++)
          {
            card[count, i] = column[i];
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("Array error most likely. Check boundary values");
        Console.WriteLine(e.Message);
      }
      return card;
    }

    private int[] FillColumn(int lowerBound, int upperBound, int columnCount, Random rndContent)
    {
      int UpperBound = upperBound + 1;
      List<int> columnContent = new List<int>();


      //the randoms used in this function. Both using the generationtoken
      Random rndColumn = new Random(GENERATIONTOKEN + columnCount + 1);
      

      int filledRows = rndColumn.Next(1, 4);

      for (int count = 0; count < filledRows; count++)
      {
        int num = rndContent.Next(lowerBound, UpperBound);
        if (!columnContent.Contains(num))
        {
          columnContent.Add(num);
        }
        else
        {
          count--;
        }

      }
      columnContent.Sort();
      int[] returnColumnContentArray = new int[cardRows] {0, 0, 0};
      for (int i = 0; i < columnContent.Count; i++)
      {
        returnColumnContentArray[i] = columnContent[i];
      }
      return returnColumnContentArray;
    }

    private int[,] CleanCard(int[,] Card, Random rndContent)
    {

      //gets random places, and properly returns it to the original array. arrays are dumb in sharp, y no subarray method built in. this is like boilerplate code 1 o 1
      for (int i = 0; i < cardColumns; i++)
      {
        if (Card.GetRow(i).Count(s => s != 0) != 0)
        {
          int[] newPositions = new int[cardRows];
          for (int j = 0; j < cardRows; j++)
          {
            newPositions[j] = Card[i, j];
          }
          newPositions = RandNumPosInColumn(newPositions);
          for (int j = 0; j < cardRows; j++)
          {
            Card[i, j] = newPositions[j];
          }
        }
      }
      return ColumnRowFixer(Card, rndContent);
    }

    private int[] RandNumPosInColumn(int[] columnContent)
    {
      int filledRowAmount = columnContent.Count(s => s != 0);
      int[] positionedColumn = new int[cardRows] {0, 0, 0};
      Random rndPosition = new Random(GENERATIONTOKEN + filledRowAmount);
      switch (filledRowAmount)
      {
        case 1:
          positionedColumn[rndPosition.Next(0, cardRows)] = columnContent.First();
          break;
        case 2:
          int pos = rndPosition.Next(0, 2);
          positionedColumn[pos] = columnContent.First();
          if (pos == 0)
          {
            positionedColumn[rndPosition.Next(1, cardRows)] = columnContent[1];
          }
          if (pos == 1)
          {
            positionedColumn[2] = columnContent[1];
          }
          break;
        case 3:
          positionedColumn[0] = columnContent[0];
          positionedColumn[1] = columnContent[1];
          positionedColumn[2] = columnContent[2];
          break;
        default:
          throw new ArgumentException();
          break;
      }
      return positionedColumn;
    }


    //This part looks at if a row has the right amount of numbers, aka not more or less than 5. When done it just returns the fixed card to the caller.
    //it might have to be run twice, as it is it could produce wrong output since we more or less always start with 1 row of 0
    //
    private int[,] ColumnRowFixer(int[,] Card, Random rndContent)
    {
      //this step is made to avoid edge cases where we have all numbers in one row or something similar.
      int totalCount = 0;
      for (int i = 0; i < cardRows; i++)
      {
        for (int j = 0; j < cardColumns; j++)
        {
          if (Card[j,i] != 0)
          {
            totalCount++;
          }
        }
      }
      if (totalCount < 10)
      {
        for (int i = totalCount; i <= 10 ; i++)
        {
          Random rndColumn = new Random(GENERATIONTOKEN + i);
          AddNumberToRow(Card, rndColumn.Next(0, cardRows), rndContent);
        }
      }


      //run through every row
      for (int i = 0; i < cardRows; i++)
      {
        //used to count how many columns in a row has too many or too few numbers, it has to be excatly 5.
        int columnCounter = 0;
        //Actually count the amount of columns with numbers
        for (int column = 0; column < 9; column++)
        {
          if (Card[column, i] != 0)
          {
            columnCounter++;
          }
        }
        //now we either have too many of too few numbers, and we call methods on that respectively.
        if (columnCounter < 5)
        {
          for (int j = columnCounter; j < 5; j++)
          {
            Card = AddNumberToRow(Card, i, rndContent);
          }
        }

        else if (columnCounter > 5)
        {
          for (int k = columnCounter; k > 5; k--)
          {
            Card = RemoveNumberFromRow(Card, i);
          }
        }
      }
      if (!Card.IsValidCard())
      {
        Card = Card.DiscardCard();
        DiscardedCards++;
        return Card;
      }
      return Card;
    }

    private int[,] RemoveNumberFromRow(int[,] Card, int rowToAlter)
    {
      List<int> numberOfRowsInColumns = new List<int>(); //The number of row that are filled in each column
      List<int> usableRows = new List<int>();
      List<int> usableRowsTwo = new List<int>();
      //which of the rows that can be deleted from, aka the ones with more than 1 number in it
      Random randColumnPicker = new Random(GENERATIONTOKEN);


      for (int column = 0; column < cardColumns; column++)
        //run through all columns, detect how many rows are filled in each column
      {
        int numberOfRows = 0;

        for (int row = 0; row < cardRows; row++)
        {
          if (Card[column, row] != 0)
          {
            numberOfRows++;
          }
        }

        if (numberOfRows == 3)
        {
          usableRows.Add(column);
        }
        if (numberOfRows == 2)
        {
          usableRowsTwo.Add(column);
        }
      }

      if (usableRows.Count != 0)
      {
        Card[usableRows[randColumnPicker.Next(0, usableRows.Count)], rowToAlter] = 0;
        //just picks out the right spot, and puts an empty field aka 0 there instead of content.
      }
      else if (usableRowsTwo.Count != 0)
      {
        Card[usableRowsTwo[randColumnPicker.Next(0, usableRowsTwo.Count)], rowToAlter] = 0;
      }
      else
      {
        Card.DiscardCard();
        DiscardedCards++;
        return Card;
      }
      return Card;
    }

    private int[,] AddNumberToRow(int[,] Card, int rowToAlter, Random rndContent)
    {
      List<int> usableRows = new List<int>();
      List<int> usableRowsTwo = new List<int>();
      Random randcolumnpicker = new Random(GENERATIONTOKEN);

      for (int column = 0; column < 9; column++)
      {
        int numberOfRows = 0;
        for (int row = 0; row < 3; row++)
        {
          if (Card[column, row] != 0)
          {
            numberOfRows++;
          }
        }
        if (numberOfRows == 1)
        {
          usableRows.Add(column);
        }
        if (numberOfRows == 2)
        {
          usableRowsTwo.Add(column);
        }
      }

      int chosenColumn = -1;
      //hopefully this never gets to stay this value. exceptions should be thrown before that point
      if (usableRows.Count != 0)
      {
        chosenColumn = usableRows[randcolumnpicker.Next(0, usableRows.Count)];
      }
      else if (usableRowsTwo.Count != 0)
      {
        chosenColumn = usableRowsTwo[randcolumnpicker.Next(0, usableRowsTwo.Count)];
      }
      else
      {
        Card = Card.DiscardCard();
        DiscardedCards++;
        return Card;
      }

      int addedNumber = 0;

      if (chosenColumn == 0)
      {
        addedNumber = rndContent.Next(1, 9);
        while (Card.ContainsValue(addedNumber))
        {
          addedNumber = rndContent.Next(1, 9);
        }
      }
      else if (chosenColumn == 8)
      {
        addedNumber = rndContent.Next(80, 91);
        while (Card.ContainsValue(addedNumber))
        {
          addedNumber = rndContent.Next(80, 91);
        }
      }
      else
      {
        int lower, upper;
        lower = chosenColumn*10;
        upper = lower + 10;
        addedNumber = rndContent.Next(lower, upper);
        while (Card.ContainsValue(addedNumber))
        {
          addedNumber = rndContent.Next(lower, upper);
        }
      }

      int[,] CleanedCard = Card;
      CleanedCard[chosenColumn, rowToAlter] = addedNumber;

      List<int> sortedRow = new List<int>();


      for (int i = 0; i < 3; i++)
      {
        sortedRow.Add(CleanedCard[chosenColumn, i]);

      }
      int number1, number2, noNumber;

      if (sortedRow[0] == 0)
      {
        noNumber = sortedRow[0];
        number1 = sortedRow[1];
        number2 = sortedRow[2];
      }
      else if (sortedRow[1] == 0)
      {
        number1 = sortedRow[0];
        noNumber = sortedRow[1];
        number2 = sortedRow[2];
      }
      else
      {
        number1 = sortedRow[0];
        number2 = sortedRow[1];
        noNumber = sortedRow[2];
      }

      if (number1 > number2)
      {
        int tempNumber = number1;
        number1 = number2;
        number2 = number1;

        if (sortedRow[0] == 0)
        {
          sortedRow[1] = number1;
          sortedRow[2] = number2;
        }
        else if (sortedRow[1] == 0)
        {
          sortedRow[0] = number1;
          sortedRow[2] = number2;
        }
        else
        {
          sortedRow[0] = number1;
          sortedRow[1] = number2;
        }
      }

      for (int i = 0; i < 3; i++)
      {
        CleanedCard[chosenColumn, i] = sortedRow[i];
      }

      //Remove duplicates
      int dupeRemover = 0;
      int fullColumn = 0;
      for (int columnCount = 0; columnCount < cardColumns; columnCount++)
      {
        for (int rowCount = 0; rowCount < cardRows; rowCount++)
        {
          if (CleanedCard[columnCount, rowCount] != 0)
          {
            fullColumn++;
            if (dupeRemover == CleanedCard[columnCount, rowCount])
            {
              Card.DiscardCard();
            }
            dupeRemover = CleanedCard[columnCount, rowCount];
          }
        }
        if (fullColumn == 3)
        {
          if (dupeRemover == CleanedCard[columnCount, 0])
          {
            Card.DiscardCard();
          }
        }
        dupeRemover = 0;
        fullColumn = 0;
      }

      return CleanedCard;
    }
  }
}
