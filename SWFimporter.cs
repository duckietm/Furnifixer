
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

internal class SWFimporter
{
  public static void smethod_0(string string_0) => Process.Start(new ProcessStartInfo()
  {
    FileName = "swfbinexport.exe",
    Arguments = string_0,
    WindowStyle = ProcessWindowStyle.Hidden
  }).WaitForExit();

  public static void smethod_1()
  {
    init.bool_0 = true;
    Console.Clear();
    init.error("Im decompiling all swf in the furni folder! Please wait before im done!", ConsoleColor.DarkMagenta);
    init.error("");
    init.error("Removing Old bin files because it can cause errors", ConsoleColor.DarkRed);
    init.Graphics();
    string[] files = Directory.GetFiles("graphicsfurni/", "*.swf");
    init.error("Found in total " + (object) files.Length + " Furni's to decompile!", ConsoleColor.DarkGreen);
    foreach (string str in files)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      new Thread(new ThreadStart(new SWFimporter.Class13()
      {
        string_0 = str
      }.method_0)).Start();
    }
    init.error("Its done yeahh! All SWFS are decompiled yeahh :P", ConsoleColor.Cyan);
    init.bool_0 = false;
    Console.ReadKey();
    init.console();
  }

    private class Class13
    {
        public Class13()
        {
        }

        public string string_0 { get; set; }

        internal void method_0()
        {
            throw new NotImplementedException();
        }
    }
}
