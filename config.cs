using System;
using System.Collections.Generic;
using System.IO;

internal class config
{
  internal Dictionary<string, string> dictionary_0;
  internal bool bool_0 = false;

  internal config(string filePath)
  {
    this.dictionary_0 = new Dictionary<string, string>();
    if (!File.Exists(filePath))
      throw new ArgumentException("Unable to locate configuration file at '" + filePath + "'.");
    this.bool_0 = true;
    try
    {
      using (StreamReader streamReader = new StreamReader(filePath))
      {
        string str;
        while ((str = streamReader.ReadLine()) != null)
        {
          if ((str.Length < 1 ? 0 : (!str.StartsWith("#") ? 1 : 0)) != 0)
          {
            int length = str.IndexOf('=');
            if (length != -1)
              this.dictionary_0.Add(str.Substring(0, length), str.Substring(length + 1));
          }
        }
        streamReader.Close();
      }
    }
    catch (Exception ex)
    {
      throw new ArgumentException("Could not process configuration file: " + ex.Message);
    }
  }
}
