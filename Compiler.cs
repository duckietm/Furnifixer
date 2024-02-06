using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

internal class Compiler
{
  public static int int_0;

  public static void smethod_0()
  {
    init.bool_0 = true;
    Compiler.int_0 = 0;
    Console.Clear();
    double num1 = (double) init.datetime();
    init.error("Compile Process started lets do it!", ConsoleColor.DarkMagenta);
    init.error("");
    init.error("Getting all SWF/BinFiles!", ConsoleColor.DarkRed);
    string[] files = Directory.GetFiles("graphicsfurni/", "*.bin");
    init.error("");
    init.error("Found " + (object) files.Length + " binfiles to compile. Lets start Compiling! D:", ConsoleColor.DarkGreen);
    init.error("");
    foreach (string str in files)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      new Thread(new ThreadStart(new Compiler.Class11()
      {
        string_0 = str
      }.method_0)).Start();
    }
    double num2 = (double) init.datetime();
    init.Graphics();
    init.error("Yeah We are done we compiled " + (object) Compiler.int_0 + " binfiles in " + (object) (num2 - num1) + " Seconds!", ConsoleColor.Cyan);
    init.bool_0 = false;
    Console.ReadKey();
    init.console();
  }

  public static void smethod_1(string string_0, string string_1) => Process.Start(new ProcessStartInfo()
  {
    FileName = "swfbinreplace.exe",
    Arguments = "graphicsfurni/" + string_0 + ".swf " + string_1 + " graphicsfurni/" + string_0 + "-" + string_1 + ".bin",
    WindowStyle = ProcessWindowStyle.Hidden
  }).WaitForExit();

  public static void smethod_2(string string_0)
  {
    if ((!File.Exists(string_0) || !string_0.Contains("-") ? 1 : (!string_0.Contains(".bin") ? 1 : 0)) != 0)
      return;
    string str = string_0.Split('-')[1].Replace(".bin", "");
    string string_0_1 = string_0.Split('/')[1].Split('-')[0].Replace(".bin", "");
    if ((!File.Exists("graphicsfurni/" + string_0_1 + ".swf") ? 1 : (!int.TryParse(str, out int _) ? 1 : 0)) != 0)
      return;
    ++Compiler.int_0;
    init.error("Compiling " + string_0_1 + " Binid " + str, ConsoleColor.DarkGreen);
    Compiler.smethod_1(string_0_1, str);
  }

    private class Class11
    {
        public Class11()
        {
        }

        public string string_0 { get; set; }

        internal void method_0()
        {
            throw new NotImplementedException();
        }
    }
}
