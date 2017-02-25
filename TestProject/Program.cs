using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoProject.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestProject
{
  [TestFixture]
  public class Program
  {
    
    static void Main(string[] args)
    {
      
    }

    [Test] //TestTest
    public void TestOne()
    {
      Assert.AreEqual(5.0, 5.0);
    }
  }
}
