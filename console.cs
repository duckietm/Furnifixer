using System;

internal class console
{
  private static void Main(string[] args)
  {
    init.Init();
    while (init.bool_1)
    {
      Console.CursorVisible = true;
      Console.Write("CFfixer> ");
      init.start(Console.ReadLine());
    }
  }

  public static void smethod_0(string string_0, ConsoleColor consoleColor_0 = ConsoleColor.Gray)
  {
    Console.ForegroundColor = consoleColor_0;
    Console.WriteLine(string_0);
    Console.ForegroundColor = ConsoleColor.Gray;
  }
}