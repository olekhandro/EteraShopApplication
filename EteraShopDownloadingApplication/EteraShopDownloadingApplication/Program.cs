using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EteraShopInterractingLibrary;

namespace EteraShopDownloadingApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var categories = new List<string>();

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //once you have the path you get the directory with:
            var directory = System.IO.Path.GetDirectoryName(path);

            var reader = new StreamReader(directory + @"\Settings.cfg");

            var outputFilename = reader.ReadLine().Replace("OutputFilename = ", "");
            var login = reader.ReadLine().Replace("Login:", "");
            var password = reader.ReadLine().Replace("Password:", "");
            reader.ReadLine();
            string category = "";
            while ((category = reader.ReadLine()) != null)
            {
                categories.Add(category);
            }
            var goods = WebsiteManager.GetGoodsFromWebsite(login, password, categories);
            CSVWriter.MakeCSVFile(outputFilename,goods);
        }
    }
}
