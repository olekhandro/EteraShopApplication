using System.Collections.Generic;
using System.IO;
using EteraShopInterractingLibrary.Domain;

namespace EteraShopInterractingLibrary
{
    public class CSVWriter
    {
        public static void MakeCSVFile(string filename, List<Good> goods)
        {
            var writer = new StreamWriter(filename);
            writer.WriteLine(";Product Title;Price(Wholesale);Color;ProductDescription");

            foreach (Good good in goods)
            {
                bool isFirst = true;
                string colorsString = "";
                foreach (string color in good.Colors)
                {
                    if (isFirst)
                    {
                        colorsString = color;
                    }
                    else
                    {
                        colorsString = colorsString + " , " + color;
                    }
                    isFirst = false;
                }
                writer.WriteLine(";{0};{1};{2};{3}", good.Title, good.Price, colorsString, good.Description);
            }
            writer.Close();
        }
    }
}