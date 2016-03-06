using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmTester;
using StarMathLib;

namespace BingoCardGenerator
{
  public class Generator
  {
    private Int32 GENERATIONTOKEN;
    private const int cardRows = 3, cardColumns = 9; //const bcs array stuff

    //Takes in the key, converts it to an int representation of the string, and stores it as a token to be reused by the algorithm.
    public Generator(string key)
    {
      GENERATIONTOKEN = key.MakeInt();
    }

    //Two versions, one for generating 1 card, and one for generating multiple cards.
    public int[,] GenerateCard()
    {
      int[,] Card;
      Card = GenerateUncleanedCard();
      Card = CleanCard(Card);
      return Card;
    }

    public List<int[,]> GenerateCard(int amountOfCards)
    {
      List<int[,]> Cards = new List<int[,]>();
      for (int i = 0; i < amountOfCards; i++)
      {
        int[,] Card;
        GENERATIONTOKEN += i;
        Card = GenerateUncleanedCard();
        Card = CleanCard(Card);
        Cards.Add(Card);
      }
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



    private int[,] GenerateUncleanedCard()
    {
      int[,] card = new int[cardColumns, cardRows];

      try
      {
        //9 is the amount of columns on a standard bingo card. change if you want differently sized cards. Maybe expand to program option. for now it is globel var
        for (int count = 0; count < cardColumns; count++)
        {
          int[] column = new int[cardRows] {0, 0, 0};
          if (count == 0)
          {
            column = FillColumn(1, 10, count);
          }
          else if (count == 8)
          {
            column = FillColumn(80, 91, count);
          }
          else
          {
            int lower, upper;
            lower = count*10;
            upper = lower + 10;
            column = FillColumn(lower, upper, count);
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

    private int[] FillColumn(int lowerBound, int upperBound, int columnCount)
    {
      int UpperBound = upperBound + 1;
      List<int> columnContent = new List<int>();


      //the randoms used in this function. Both using the generationtoken
      Random rndColumn = new Random(GENERATIONTOKEN + columnCount + 1);
      Random rndContent = new Random(GENERATIONTOKEN);

      int filledRows = rndColumn.Next(1, 4);
      ; //the amount of slots to be filled

      for (int count = 0; count < filledRows; count++)
      {
        int num = rndContent.Next(lowerBound, UpperBound);
        if (!columnContent.Contains(num))
        {
          columnContent.Add(num);
        }
        else
        {
          if (count <= 0) // wat?
          {
            count--;
          }
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

    private int[,] CleanCard(int[,] Card)
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
      return ColumnRowFixer(Card);
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
    private int[,] ColumnRowFixer(int[,] Card)
    {
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
            Card = AddNumberToRow(Card, i);
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
        throw new ArgumentException("Something went very wrong. At the card cleaning part, removenumberfomrow part");
      }
      return Card;
    }

    private int[,] AddNumberToRow(int[,] Card, int rowToAlter)
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
        throw new ArgumentException("something went very wrong. addnumbertorow");
      }

      int addedNumber = 0;
      Random randNumberMade = new Random(GENERATIONTOKEN + chosenColumn);

      if (chosenColumn == 0)
      {
        addedNumber = randNumberMade.Next(1, 10);
      }
      else if (chosenColumn == 8)
      {
        addedNumber = randNumberMade.Next(80, 91);
      }
      else
      {
        int lower, upper;
        lower = chosenColumn*10;
        upper = lower + 10;
        addedNumber = randNumberMade.Next(lower, upper);
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

      return CleanedCard;
    }
  }
}
