using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmHasher
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hash of Algorithm: ");
      Console.WriteLine(checkMD5(@"C:\Users\krist\Documents\GitHub\Banko\BingoCardGenerator\Generator.cs"));
      Console.WriteLine(checkMD5(@"C:\Users\krist\Documents\GitHub\Banko\BingoCardGenerator\MakeIntToken.cs"));
      Console.ReadLine();
    }

    public static string checkMD5(string filename)
    {
      using (var md5 = MD5.Create())
      {
        using (var stream = File.OpenRead(filename))
        {
          return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
        }
      }
    }
  }
}
