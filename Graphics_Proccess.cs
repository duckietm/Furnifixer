using System;
using System.IO;
using System.Text.RegularExpressions;

internal class Graphics_Proccess
{
  public static void smethod_0(bool bool_0)
  {
    init.bool_0 = true;
    Console.Clear();
    double num1 = (double) init.datetime();
    if (bool_0)
      init.error("Graphics Remove proces started! Wait before its done!", ConsoleColor.DarkMagenta);
    else
      init.error("Graphics add proces started! Wait before its done!", ConsoleColor.DarkMagenta);
    init.error("");
    init.error("Removing Old bin files because it can cause errors", ConsoleColor.DarkRed);
    Directory.GetFiles("graphicsfurni/", "*.bin");
    init.Graphics();
    string[] files1 = Directory.GetFiles("graphicsfurni/", "*.swf");
    int length = files1.Length;
    foreach (string string_0 in files1)
    {
      init.error("found furni: " + string_0, ConsoleColor.Green);
      SWFimporter.smethod_0(string_0);
    }
    int num2 = 0;
    init.error(length.ToString() + " furnis are decompiled lets start replacing shit in the bin files yeahh :$", ConsoleColor.DarkGreen);
    string[] files2 = Directory.GetFiles("graphicsfurni/", "*.bin");
    init.error("Found " + (object) files2.Length + " Bin files lets search in it!");
    foreach (string str1 in files2)
    {
      if ((!File.Exists(str1) ? 0 : (str1.StartsWith("graphicsfurni/") ? 1 : 0)) != 0)
      {
        string str2 = File.ReadAllText(str1);
        bool flag = false;
        string contents = "";
        string str3 = str1.Split('/')[1].Split('-')[0];
        if (!bool_0)
        {
          string oldValue = "<visualizationData type=\"@FurniName\">".Replace("@FurniName", str3).Replace("/", "");
          if ((str2.Contains("<graphics>") || !str2.Contains("</visualizationData>") ? 1 : (!str2.Contains(oldValue) ? 1 : 0)) == 0)
          {
            flag = true;
            contents = str2.Replace(oldValue, oldValue + Environment.NewLine + "<graphics>").Replace("</visualizationData>", "</graphics>" + Environment.NewLine + " </visualizationData>");
          }
        }
        else if ((!str2.Contains("<graphics>") ? 1 : (!str2.Contains("</graphics>") ? 1 : 0)) == 0)
        {
          flag = true;
          contents = str2.Replace("<graphics>", "").Replace("</graphics>", "");
        }
        if ((!flag ? 1 : (!(contents != "") ? 1 : 0)) == 0)
        {
          ++num2;
          init.error("Found a File Thats Has or needs a graphics tag!", ConsoleColor.Green);
          File.WriteAllText(str1, contents);
          Compiler.smethod_1(str3, new Regex("-(.*).bin").Match(str1).Groups[1].ToString());
          init.error("Added graphics tags to and compiled the furni: " + str1, ConsoleColor.Green);
        }
      }
    }
    init.error("Removing Bin Files! and places the fixed furni into a new folder");
    init.Graphics();
    foreach (string str4 in files1)
    {
      string fileName = Path.GetFileName(str4);
      if (File.Exists(str4))
      {
        string str5 = "WithfixedFurni/" + fileName;
        if (bool_0)
          str5 = "WithoutFixedFurni/" + fileName;
        if (File.Exists(str5))
          File.Delete(str5);
        File.Copy(str4, str5);
        File.Delete(str4);
      }
    }
    init.error("We edited " + (object) num2 + " bin files to fix all furnis! in " + ((double) init.datetime() - num1).ToString());
    if (!bool_0)
      init.error("Its done yeahh! All fixed furnis are placed in WithfixedFurni", ConsoleColor.Cyan);
    else
      init.error("Its done yeahh! All fixed furnis are placed in WithoutFixedFurni", ConsoleColor.Cyan);
    init.bool_0 = false;
    Console.ReadKey();
    init.console();
  }
}
