using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Logging;

namespace BankoProjectRemastered.Tools
{
  public static class BnkLogger //: ILoggerFacade Implement this interface to integrate more automated things?
  {
    private static readonly TraceLogger Logger = new TraceLogger();


    public static void LogLowDebugInfo(string message)
    {
      Logger.Log(message, Category.Debug, Priority.Low);
    }

    public static void LogHighDebugInfo(string message)
    {
      Logger.Log(message, Category.Debug, Priority.High);
    }

    public static void LogExInfoLow(string message)
    {
      Logger.Log(message, Category.Exception, Priority.Low);
    }

    public static void LogExInfoHigh(string message)
    {
      Logger.Log(message, Category.Exception, Priority.Low);
    }
  }
}