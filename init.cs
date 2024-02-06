using System;
using System.IO;

internal class init
{
  public static config config;
  public static bool bool_0 = false;
  public static bool bool_1 = true;
  public static string string_0 = Path.GetTempPath();
  public static string floor;
  public static string furnidata_old;
  public static string wall;
  public static string catalogItem;
  public static string defaultFurniture;
  public static uint catalogItemID = 0;
  public static uint furnitureItemId = 0;
  public static uint baseItemId = 0;
  public static bool useStartBaseItem = false;

  public static void Init()
  {
    if (!Directory.Exists("graphicsfurni"))
      Directory.CreateDirectory("graphicsfurni");
    if (!Directory.Exists("WithfixedFurni"))
      Directory.CreateDirectory("WithfixedFurni");
    if (!Directory.Exists("sql/furni"))
      Directory.CreateDirectory("sql/furni");
    init.config = new config("settings/config.ini");
    init.catalogItem = init.config.dictionary_0["DefaultCatalogItem"];
    init.defaultFurniture = init.config.dictionary_0["DefaultFurniture"];
    init.floor = File.ReadAllText("settings/Default_furnidata_floor.txt");
    init.wall = File.ReadAllText("settings/Default_furnidata_wall.txt");
    init.furnidata_old = File.ReadAllText("settings/Default_furnidata_old.txt");
    init.catalogItemID = Convert.ToUInt32(init.config.dictionary_0["StartCatalogItemID"]);
    init.furnitureItemId = Convert.ToUInt32(init.config.dictionary_0["StartFurnitureItemID"]);
    init.baseItemId = Convert.ToUInt32(init.config.dictionary_0["StartBaseItemID"]);
    init.useStartBaseItem = init.config.dictionary_0["UseStartBaseitem"] == "true";
    init.console();
  }

  public static void console()
  {
    Console.Title = "Custom furni / normal furni Fixer By SpotIfy";
    if (!Directory.Exists("graphicsfurni"))
      Directory.CreateDirectory("graphicsfurni");
    if (!Directory.Exists("WithfixedFurni"))
      Directory.CreateDirectory("WithfixedFurni");
    if (!Directory.Exists("sql/furni"))
      Directory.CreateDirectory("sql/furni");
    Console.Clear();
    init.error("Welcome to the automaticly Furni fixer!", ConsoleColor.Green);
    init.error("This program is made by SpotIFy!", ConsoleColor.DarkYellow);
    init.error("Automatic furni fixer adds or removes the graphics tags in all swf in the furni folder by simpel typing add or remove");
    init.error("");
    init.error("Commands:", ConsoleColor.DarkCyan);
    init.error("'add' add the Graphics tag into the swfs for fixing customs in plus r2", ConsoleColor.DarkGreen);
    init.error("'remove' Remove the Graphics tags from the swfs to fix new furni for old builds", ConsoleColor.DarkGreen);
    init.error("'decompileall' Decompile all swf in the furni folder", ConsoleColor.DarkGreen);
    init.error("'decompile' decompile a swf usage: decompile SWFNAME BINID", ConsoleColor.DarkGreen);
    init.error("'compileall' Compile all binfiles into all swfs", ConsoleColor.DarkGreen);
    init.error("'compile' compile a binfile into a swf usage: compile SWFNAME BINID", ConsoleColor.DarkGreen);
    init.error("'download' Downloads and generates the SQL's from the furnidata for new furni!", ConsoleColor.DarkGreen);
    init.error("'downloadn' Downloads from the furnidata for redownloading the hof_furni", ConsoleColor.DarkGreen);
    init.error("'generate' Generate SQLS/furnidata from sql/furni map without furnidata", ConsoleColor.DarkGreen);
  }

  public static void start(string string_6)
  {
    if (init.bool_0 || string_6.Length < 1)
      return;
    string[] strArray = string_6.Split(' ');
    switch (strArray[0].ToLower())
    {
      case "add":
        Graphics_Proccess.smethod_0(false);
        break;
      case "remove":
        Graphics_Proccess.smethod_0(true);
        break;
      case "decompileall":
        SWFimporter.smethod_1();
        break;
      case "decompile":
        if (strArray.Length >= 2)
        {
          string str1 = strArray[1];
          if (str1.Contains(".swf"))
            str1 = str1.Split('.')[0];
          string str2 = "graphicsfurni/" + str1 + ".swf";
          if (File.Exists(str2))
          {
            init.error("Decompiling " + str1 + "...", ConsoleColor.Green);
            SWFimporter.smethod_0(str2);
            init.error("Done!", ConsoleColor.Cyan);
          }
          else
            init.error("File Doesnt exist!", ConsoleColor.Red);
          Console.ReadKey();
          init.console();
          break;
        }
        init.error("You need to use decompile SWFNAME", ConsoleColor.Red);
        break;
      case "compile":
        if ((strArray.Length != 3 ? 1 : (strArray[1].Contains(".swf") ? 1 : 0)) == 0)
        {
          if (int.TryParse(strArray[2], out int _))
          {
            int num;
            if (File.Exists("graphicsfurni/" + strArray[1] + ".swf"))
              num = !File.Exists("graphicsfurni/" + strArray[1] + "-" + strArray[2] + ".bin") ? 1 : 0;
            else
              num = 1;
            if (num == 0)
            {
              init.error("Compiling the swf...", ConsoleColor.Green);
              Compiler.smethod_1(strArray[1], strArray[2]);
              init.error("Done!", ConsoleColor.Cyan);
            }
            else
              init.error("File Doesnt exist!", ConsoleColor.Red);
            Console.ReadKey();
            init.console();
            break;
          }
          init.error("BinId is wrong!");
          break;
        }
        init.error("You need to it like this compile swfname binid", ConsoleColor.Red);
        break;
      case "compileall":
        Compiler.smethod_0();
        break;
      case "download":
        new download().method_0(true);
        break;
      case "downloadn":
        new download().method_0(false);
        break;
      case "generate":
        new generate("sql/furni").method_3();
        break;
      case "reset":
        init.console();
        break;
      default:
        init.error("Wrong command :p", ConsoleColor.Red);
        break;
    }
  }

  public static void error(string string_6, ConsoleColor consoleColor_0 = ConsoleColor.Gray)
  {
    Console.ForegroundColor = consoleColor_0;
    Console.WriteLine(string_6);
    Console.ForegroundColor = ConsoleColor.Gray;
  }

  public static void Graphics()
  {
    foreach (string file in Directory.GetFiles("graphicsfurni/", "*.bin"))
    {
      if (File.Exists(file))
        File.Delete(file);
    }
  }

  internal static int datetime() => (int) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
}
