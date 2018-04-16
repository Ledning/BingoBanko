using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankoProjectRemastered.Tools
{
  public static class SaveLoadTool
  {
    private static string _folderPath = "";

    public enum SaveState
    {
      Saved,
      Autosaved,
      Unsaved,
      SaveError
    }

    public enum LoadState
    {
      Loaded,
      LoadError 
    }

    public static string SavedEventsPath
    {
      get { return FolderPath + "\\Seneste"; }
    }

    public static string BackgroundsPath
    {
      get { return FolderPath + "\\Billeder"; }
    }

    public static string SettingsPath
    {
      get { return FolderPath + "\\Konfiguration"; }
    }

    public static string FolderPath
    {
      get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BingoBankoKontrol"; }

    }

    /// <summary>
    /// Saves the specified object to the specified path in xml form. Object must implement iserializable
    /// </summary>
    /// <param name="Object">The object to be saved as XML</param>
    /// <param name="filepath">The file path of where to save the file</param>
    /// <returns></returns>
    public static SaveState Save(ISerializable Object, string filepath)
    {
      StreamWriter writer = null;
      try
      {
        XmlSerializer xml = new XmlSerializer((Object.GetType()));
        writer = new StreamWriter(filepath);
        xml.Serialize(writer, Object);
      }
      catch (Exception e)
      {
        BnkLogger.LogLowDebugInfo("Error Saving: " + e.Message + "Data: " + e.Data);
        return SaveState.SaveError;
      }
      finally
      {
        writer?.Close();
      }

      return SaveState.Saved;
    }



    /// <summary>
    /// Supply the filepath, and the target file-type, and get a status back with the state and the loaded object (or null if nothing was loaded)
    /// </summary>
    /// <typeparam name="T">The type of the output object</typeparam>
    /// <param name="filepath">the filepath(and name)</param>
    /// <returns>A tuple of LoadState and the loaded object</returns>
    public static Tuple<T, LoadState> Load<T>(string filepath, string filename)
    {
      string combinedFilePath = filepath + "//" + filename;
      Type type = typeof(T);
      T result;
      try
      {
        XmlSerializer xml = new XmlSerializer(type);
        FileStream fs = new FileStream(combinedFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        result = (T)xml.Deserialize(fs);
        fs.Close();
      }
      catch (Exception e)
      {
        BnkLogger.LogLowDebugInfo("Error Loading: " + e.Message + "Data: " + e.Data);
        result = default(T);
        return new Tuple<T, LoadState>(result, LoadState.LoadError);
      }
      return new Tuple<T, LoadState>(result, LoadState.Loaded);
    }

  }
}
