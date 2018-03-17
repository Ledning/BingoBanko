using System.Collections.Generic;

namespace BingoCardGenerator
{
  public interface IGenerator
  {
    List<int[,]> GenerateCard(int amountOfCards);
  }
}