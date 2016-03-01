using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoCardGenerator
{
    public class Generator
    {
        private int GENERATIONTOKEN;
        private const int cardRows = 3, cardColumns = 9; //const bcs array stuff

        //Takes in the key, converts it to an int representation of the string, and stores it as a token to be reused by the algorithm.
        public Generator(string key)
        {
            GenerationToken = Convert.ToInt16(Convert.ToChar(key));
        }

        //Two versions, one for generating 1 card, and one for generating multiple cards.
        public int[,] GenerateCard(string key)
        {
            int[,] Card;
            Card = GenerateUncleanedCard();
            Card = CleanCard(Card);
            return Card;
        }
        public List<int[,]> GenerateCard(string key, int amountOfCards)
        {
            List<int[,]> Cards = new List<int[,]>();
            for (int i = 0; i < amountOfCards; i++)
            {
                int[,] Card;
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
            int[,] Card = new int[cardColumns,cardRows];
            try
            {
                //9 is the amount of columns on a standard bingo card. change if you want differently sized cards. Maybe expand to program option
                for (int count = 0; count < 9; count++)
                {
                    int[] column = new int[3];
                    if (count == 0)
                    {
                        column = FillColumn(1, 10, count);
                    }
                    if (count == 8)
                    {
                        column = FillColumn(80, 91, count);
                    }
                    else
                    {
                        int lower, upper;
                        lower = count * 10;
                        upper = lower + 10;
                        column = FillColumn(lower, upper, count);
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        Card[i, count] = column[i];
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Array Init Failed. Check boundary values");
            }
            return Card;
        }
        private int[] FillColumn(int lowerBound, int upperBound, int columnCount)
        {
            int UpperBound = upperBound + 1;
            List<int> columnContent = new List<int>();
            int filledRowAmount;//the amount of slots to be filled

            //the randoms used in this function. Both using the generationtoken, but 
            Random rndColumn = new Random(GENERATIONTOKEN + columnCount);
            Random rndContent = new Random(GENERATIONTOKEN);
            filledRowAmount = rndColumn.Next(1, 4);



            for (int count = 1; count <= filledRowAmount; count++)
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
            return columnContent.ToArray();
        }

        private int[,] CleanCard(int[,] Card)
        {

            //gets random places, and properly returns it to the original array. arrays are dumb in sharp, y no subarray method built in. this is like boilerplate code 1 o 1
            for (int i = 0; i < cardColumns; i++)
            {
                int[] newPositions = new int[cardColumns];
                Array.Copy(Card, newPositions, i);
                newPositions = RandNumPosInColumn(newPositions);
                for (int j = 0; j < cardRows; j++)
                {
                    Card[j, i] = newPositions[j];
                }
            }



            return Card;
        }
        private int[] RandNumPosInColumn(int[] columnContent)
        {
            int filledRowAmount = columnContent.Count();
            int[] positionedColumn = new int[cardRows] { 0, 0, 0 };
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
    }
}
