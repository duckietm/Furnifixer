using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

internal class generate
{
  private string string_0;
  private List<string> list_0;
  private List<string> list_1;
  private List<string> list_2;
  private List<string> list_3;
  private List<string> list_4;
  private string PageID;

  public generate(string map)
  {
    this.string_0 = map;
    this.list_0 = new List<string>();
    this.list_1 = new List<string>();
    this.list_2 = new List<string>();
    this.list_3 = new List<string>();
    this.list_4 = new List<string>();
    this.PageID = init.config.dictionary_0["DefaultPageID"];
  }

  public void method_0(
    string string_2,
    string string_3,
    string string_4,
    string string_5,
    string string_6,
    string string_7,
    string string_8,
    string string_9,
    string string_10,
    bool bool_0)
  {
    ++init.catalogItemID;
    ++init.furnitureItemId;
    string oldValue = "DO NOT REPLACE";
    if (string_7 != "")
      oldValue = string_7;
    uint uint0 = init.catalogItemID;
    string string_11 = Convert.ToString(init.furnitureItemId);
    if ((init.useStartBaseItem || string_7 == "" ? 0 : (Convert.ToUInt32(string_7) > 0U ? 1 : 0)) == 0)
    {
      ++init.baseItemId;
      string_7 = Convert.ToString(init.baseItemId);
    }
    string string_7_1 = "s";
    if (!bool_0)
    {
      if (string_2 != "")
        this.list_0.Add(string_2.Replace(oldValue, string_7));
      else
        this.list_0.Add(this.GetFurniData(init.floor, string_7, string_3, string_6, string_5, string_7_1, string_8, string_9, string_10, uint0, string_11));
    }
    else
    {
      string_7_1 = "i";
      if (string_2 != "")
        this.list_1.Add(string_2);
      else
        this.list_1.Add(this.GetFurniData(init.wall, string_7, string_3, string_6, string_5, string_7_1, string_8, string_9, string_10, uint0, string_11));
    }
    this.list_2.Add(this.GetFurniData(init.furnidata_old, string_7, string_3, string_6, string_5, string_7_1, string_8, string_9, string_10, uint0, string_11));
    this.list_3.Add(this.GetFurniData(init.catalogItem, string_7, string_3, string_6, string_5, string_7_1, string_8, string_9, string_10, uint0, string_11));
    this.list_4.Add(this.GetFurniData(init.defaultFurniture, string_7, string_3, string_6, string_5, string_7_1, string_8, string_9, string_10, uint0, string_11));
  }

  public string GetFurniData(
    string Furniture_collection,
    string BaseID,
    string SWFName,
    string Description,
    string PUBName,
    string Type,
    string XScale,
    string YScale,
    string ZScale,
    uint CactaID_int,
    string FurnitureID)
  {
    return Furniture_collection.Replace("%baseid%", BaseID)
            .Replace("%swfname%", SWFName)
            .Replace("%pubname%", PUBName)
            .Replace("%desc%", Description)
            .Replace("%type%", Type)
            .Replace("%x%", XScale)
            .Replace("%y%", YScale)
            .Replace("%z%", ZScale)
            .Replace("%cataid%", Convert.ToString(CactaID_int))
            .Replace("%pageid%", this.PageID)
            .Replace("%furnitureid%", FurnitureID);
  }

  public void method_2(string string_2)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("## Catalog Items ##");
    stringBuilder.Append(Environment.NewLine);
    foreach (string str in this.list_3)
    {
      stringBuilder.Append(str);
      stringBuilder.Append(Environment.NewLine);
    }
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append("## Furniture ##");
    stringBuilder.Append(Environment.NewLine);
    foreach (string str in this.list_4)
    {
      stringBuilder.Append(str);
      stringBuilder.Append(Environment.NewLine);
    }
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append("## Furnidata ROOMITEMS ##");
    stringBuilder.Append(Environment.NewLine);
    foreach (string str in this.list_0)
    {
      stringBuilder.Append(str);
      stringBuilder.Append(Environment.NewLine);
    }
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append("## Furnidata WALLITEMS ##");
    stringBuilder.Append(Environment.NewLine);
    foreach (string str in this.list_1)
    {
      stringBuilder.Append(str);
      stringBuilder.Append(Environment.NewLine);
    }
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append("## Old furnidata for phoenix etc ##");
    stringBuilder.Append(Environment.NewLine);
    foreach (string str in this.list_2)
    {
      stringBuilder.Append(str);
      stringBuilder.Append(Environment.NewLine);
    }
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append(Environment.NewLine);
    stringBuilder.Append("If the SQLS or the furnidata is wrong edit it in the settings map!");
    File.WriteAllText("sql/" + string_2, stringBuilder.ToString());
    string oldValue1 = "StartCatalogItemID=" + init.config.dictionary_0["StartCatalogItemID"];
    string oldValue2 = "StartFurnitureItemID=" + init.config.dictionary_0["StartFurnitureItemID"];
    string oldValue3 = "StartBaseItemID=" + init.config.dictionary_0["StartBaseItemID"];
    File.WriteAllText("settings/config.ini", File.ReadAllText("settings/config.ini").Replace(oldValue1, "StartCatalogItemID=" + (object) init.catalogItemID).Replace(oldValue2, "StartFurnitureItemID=" + (object) init.furnitureItemId).Replace(oldValue3, "StartBaseItemID=" + (object) init.baseItemId));
    init.config = new config("settings/config.ini");
  }

  public void method_3()
  {
    XDocument xdocument = XDocument.Load(init.config.dictionary_0["HabboFurniDataXMLURL"]);
    init.bool_0 = true;
    Console.Clear();
    init.error("Starting Generate Process. from map: " + this.string_0, ConsoleColor.DarkYellow);
    init.error("Checking FurniCount");
    string[] files = Directory.GetFiles(this.string_0, "*.swf");
    int length = files.Length;
    if (length == 0)
    {
      init.bool_0 = false;
      init.error("No furni's found!", ConsoleColor.Red);
      Console.ReadKey();
      init.console();
    }
    else
    {
      init.error("Found " + (object) length + " furni's to work with", ConsoleColor.Green);
      init.error("");
      foreach (string str in files)
      {
        try
        {
          generate.Class9 class9 = new generate.Class9();
          class9.string_0 = Path.GetFileNameWithoutExtension(str);
          uint num1 = 1;
          uint num2 = 1;
          string string_10 = "1";
          string string_2 = "";
          string SetDesription = "no desc";
          string SetName = class9.string_0;
          string SetID = "";
          bool bool_0 = false;
          if (xdocument != null)
          {
            try
            {
              XElement WallItemTypes = xdocument.Descendants((XName) "wallitemtypes").Descendants<XElement>().Where<XElement>(new Func<XElement, bool>(class9.method_0)).FirstOrDefault<XElement>();
              if (WallItemTypes != null)
              {
                string_2 = WallItemTypes.ToString();
                bool_0 = true;
                SetName = WallItemTypes.Element((XName) "name").Value;
                SetDesription = WallItemTypes.Element((XName) "description").Value;
                SetID = WallItemTypes.Attribute((XName) "id").Value;
              }
              XElement RoomItemsType = xdocument.Descendants((XName) "roomitemtypes").Descendants<XElement>().Where<XElement>(new Func<XElement, bool>(class9.method_1)).FirstOrDefault<XElement>();
              if (RoomItemsType != null)
              {
                string_2 = RoomItemsType.ToString();
                bool_0 = false;
                SetName = RoomItemsType.Element((XName) "name").Value;
                SetDesription = RoomItemsType.Element((XName) "description").Value;
                SetID = RoomItemsType.Attribute((XName) "id").Value;
              }
            }
            catch
            {
              init.error("Error with parsing furnidata ", ConsoleColor.Red);
            }
          }
          SWFimporter.smethod_0(str);
          // ISSUE: reference to a compiler-generated field
          foreach (string file in Directory.GetFiles("sql/furni/", class9.string_0 + "-*.bin"))
          {
            try
            {
              string text = File.ReadAllText(file);
              if ((string.IsNullOrEmpty(text) ? 1 : (!text.Contains("<dimensions") ? 1 : 0)) == 0)
              {
                XElement xelement = XDocument.Parse(text).Root.Element((XName) "model").Element((XName) "dimensions");
                if (xelement != null)
                {
                  int num3 = xelement.Attribute((XName) "centerZ") != null ? 1 : 0;
                  num1 = Convert.ToUInt32(xelement.Attribute((XName) "x").Value);
                  num2 = Convert.ToUInt32(xelement.Attribute((XName) "y").Value);
                  string_10 = xelement.Attribute((XName) "z").Value;
                }
              }
              File.Delete(file);
            }
            catch (FormatException ex)
            {
              // ISSUE: reference to a compiler-generated field
              bool_0 = class9.string_0.Contains("wall");
              File.Delete(file);
            }
          }
          // ISSUE: reference to a compiler-generated field
          this.method_0(string_2, class9.string_0, "", SetName, SetDesription, SetID, Convert.ToString(num1), Convert.ToString(num2), string_10, bool_0);
          // ISSUE: reference to a compiler-generated field
          init.error("Generated SQLS , furnidata for " + class9.string_0, ConsoleColor.Green);
        }
        catch
        {
        }
      }
      init.error("");
      string string_2_1 = init.datetime().ToString() + "_generated.txt";
      init.error("Done lets make " + string_2_1 + " ..", ConsoleColor.DarkGreen);
      this.method_2(string_2_1);
      init.error("Its DONE! Yeahh we generated evrything!", ConsoleColor.Cyan);
      init.bool_0 = false;
      Console.ReadKey();
      init.console();
    }
  }

    private class Class9
    {
        internal string string_0;

        public Class9()
        {
        }

        internal bool method_0(XElement arg)
        {
            throw new NotImplementedException();
        }

        internal bool method_1(XElement arg)
        {
            throw new NotImplementedException();
        }
    }
}
