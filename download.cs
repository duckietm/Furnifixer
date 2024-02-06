using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml.Linq;

internal class download
{
  private readonly string DownloadFurnidataXML;
  private readonly WebClient DownloadClient;
  private readonly string DownloadFurniData;
  private readonly string DownloadHofFurni;
  private readonly bool AddReversion;
  private readonly XDocument FurniData;
  private int int_0 = 0;
  private readonly generate GenerateSQL = new generate("sql");

  public download()
  {
    try
    {
      this.DownloadFurnidataXML = init.config.dictionary_0["HabboFurniDataXMLURL"];
      this.DownloadHofFurni = init.config.dictionary_0["HabbboHofFurniUrl"];
      this.AddReversion = init.config.dictionary_0["AddReversionToUrl"] == "true";
      this.DownloadClient = new WebClient();
      DownloadClient.Headers.Add("user-agent", "Mozilla/5.0+(Windows+NT+10.0;+Win64;+x64)+AppleWebKit/537.36+(KHTML,+like+Gecko)+Chrome/70.0.3538.102+Safari/537.36+Edge/18.18362;)");
      this.DownloadFurniData = this.DownloadClient.DownloadString(this.DownloadFurnidataXML);
      this.FurniData = XDocument.Parse(this.DownloadFurniData);
    }
    catch (Exception ex)
    {
      init.error("The furnidata didnt load correct: " + ex.ToString());
    }
  }

  public void method_0(bool bool_1)
  {
    download.Class3 class3 = new download.Class3();
    class3.bool_0 = bool_1;
    class3.class2_0 = this;
    try
    {
      Console.Clear();
      init.error("Hi, Im Gona check the furnidata for new furni's for you!", ConsoleColor.DarkGreen);
      init.error("This can take a while because the furnidata has 3000+ furni", ConsoleColor.DarkGreen);
      init.error("Lets start getting the Furnidata from: " + this.DownloadFurnidataXML, ConsoleColor.Cyan);
      init.error("");
      init.bool_0 = true;
      this.int_0 = 0;
      foreach (XElement descendant in this.FurniData.Descendants((XName) "roomitemtypes").Descendants<XElement>((XName) "furnitype"))
      {
        download.Class4 class4 = new download.Class4();
        class4.class3_0 = class3;
        class4.xelement_0 = descendant;
        if (this.int_0 >= 600)
        {
          Thread.Sleep(5000);
          this.int_0 = 0;
        }
        new Thread(new ThreadStart(class4.Method_0)).Start();
      }
      foreach (XElement descendant in this.FurniData.Descendants((XName) "wallitemtypes").Descendants<XElement>((XName) "furnitype"))
      {
        download.Class5 class5 = new download.Class5();
        class5.class3_0 = class3;
        class5.xelement_0 = descendant;
        if (this.int_0 >= 600)
        {
          Thread.Sleep(5000);
          this.int_0 = 0;
        }
        new Thread(new ThreadStart(class5.method_0)).Start();
      }
      init.error("");
      init.error("waiting 5 seconds..", ConsoleColor.DarkCyan);
      Thread.Sleep(5000);
      if ((!class3.bool_0 ? 1 : (this.int_0 < 1 ? 1 : 0)) == 0)
      {
        string string_2 = init.datetime().ToString() + "_Downloaded.txt";
        init.error("Done lets make " + string_2 + " ..", ConsoleColor.DarkGreen);
        this.GenerateSQL.method_2(string_2);
      }
      init.error(Environment.NewLine);
      init.error("Its done yeahh we downloaded " + (object) this.int_0 + " Furnis! and placed it in Yhof_furni and newfurni", ConsoleColor.Cyan);
      Console.ReadKey();
      init.bool_0 = false;
      init.console();
    }
    catch
    {
      init.bool_0 = false;
      init.console();
    }
  }

  private void method_1(XElement xelement_0, bool bool_1, bool bool_2)
  {
    string string_3 = "";
    try
    {
      WebClient webClient = new WebClient();
      webClient.Headers.Add("user-agent", "Mozilla/5.0+(Windows+NT+10.0;+Win64;+x64)+AppleWebKit/537.36+(KHTML,+like+Gecko)+Chrome/70.0.3538.102+Safari/537.36+Edge/18.18362;)");
      uint num1 = 1;
      uint num2 = 1;
      string string_10 = "1";
      string_3 = xelement_0.Attribute((XName) "classname").Value;
      if (string_3.Contains("*"))
        string_3 = string_3.Split('*')[0];
      if (System.IO.File.Exists("Yhof_furni/" + string_3 + ".swf"))
        return;
      string string_4 = "";
      string string_5 = xelement_0.Element((XName) "name").Value;
      string string_6 = xelement_0.Element((XName) "description").Value;
      string string_7 = xelement_0.Attribute((XName) "id").Value;
      if (this.AddReversion)
        string_4 = xelement_0.Element((XName) "revision").Value;
      webClient.DownloadFile(this.DownloadHofFurni + string_4 + "/" + string_3 + ".swf", "Yhof_furni/" + string_3 + ".swf");
      ++this.int_0;
      if ((!bool_1 ? 1 : (this.int_0 < 1 ? 1 : 0)) == 0)
      {
        if (!System.IO.File.Exists("newfurni/" + string_3 + ".swf"))
          System.IO.File.Copy("Yhof_furni/" + string_3 + ".swf", "newfurni/" + string_3 + ".swf");
        SWFimporter.smethod_0("newfurni/" + string_3 + ".swf");
        if (!bool_2)
        {
          foreach (string file in Directory.GetFiles("newfurni/", string_3 + "-*.bin"))
          {
            try
            {
              string text = System.IO.File.ReadAllText(file);
              if ((string.IsNullOrEmpty(text) ? 1 : (!text.Contains("<dimensions") ? 1 : 0)) == 0)
              {
                XElement xelement = XDocument.Parse(text).Root.Element((XName) "model").Element((XName) "dimensions");
                if (xelement != null)
                {
                  num1 = Convert.ToUInt32(xelement.Attribute((XName) "x").Value);
                  num2 = Convert.ToUInt32(xelement.Attribute((XName) "y").Value);
                  string_10 = xelement.Attribute((XName) "z").Value;
                }
              }
              System.IO.File.Delete(file);
            }
            catch (Exception ex)
            {
              System.IO.File.Delete(file);
            }
          }
        }
        this.GenerateSQL.method_0(xelement_0.ToString(), string_3, string_4, string_5, string_6, string_7, Convert.ToString(num1), Convert.ToString(num2), string_10, bool_2);
      }
      init.error("Downloaded " + string_3 + " and generated the sql.", ConsoleColor.Green);
    }
    catch (Exception ex)
    {
      if (!ex.ToString().Contains("404"))
        return;
      init.error("Furni " + string_3 + " niet gevonden!", ConsoleColor.Red);
    }
  }

    internal class Class3
    {
        internal bool bool_0;
        internal download class2_0;
    }

    private class Class4
    {
        internal download.Class3 class3_0;
        internal XElement xelement_0;

        public Class4()
        {
        }

        internal void Method_0()
        {
            throw new NotImplementedException();
        }
    }

    private class Class5
    {
        internal Class3 class3_0;
        internal XElement xelement_0;

        internal void method_0()
        {
            throw new NotImplementedException();
        }
    }
}
