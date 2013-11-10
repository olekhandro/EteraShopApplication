using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using EteraShopInterractingLibrary.Domain;
using WatiN.Core;

namespace EteraShopInterractingLibrary
{
    public class WebsiteManager
    {
        private const string ETERASHOPADDRESS = "http://www.eterashop.com/";

        private const string SEARCHLINK = "http://www.eterashop.com/main/sub.php?search=";

        private const string ETERASHOPLOGINADDRESS =
            "https://www.eterashop.com/login/login_page.php?preurl=http%3A%2F%2Fwww.eterashop.com%2Fmain%2Findex.php%3F";


        public static List<Good> GetGoodsFromWebsite(string login, string pass, List<string> searchWords)
        {
            var result = new List<Good>();

            var IE = new IE();
             
            LoginToWebPage(IE,login,pass);
            foreach (string category in searchWords)
            {
                var goodLinks = new List<string>();
                IE.GoTo(SEARCHLINK + category);
                var isVisitedDictionary = new Dictionary<string, bool>();
                Thread.Sleep(3000);
                var paginator = IE.TableCells.FirstOrDefault(x => x.Elements.Any(y => y.ClassName == "paginate"));
                var links = new List<Link>();
                if (paginator != null)
                {
                    links = paginator.Links.Where(x => x.Url.Contains("reqPage")).ToList();
                }
                foreach (var link in links)
                {
                    if (!isVisitedDictionary.Keys.Any(x => x.Contains(link.Url.Substring(0, link.Url.IndexOf("&")))))
                    {
                        isVisitedDictionary.Add(link.Url, false);
                    }
                }
                while (isVisitedDictionary.Values.Any(x => x == false))
                {
                    string url = isVisitedDictionary.FirstOrDefault(x => x.Value == false).Key;
                    isVisitedDictionary.Remove(isVisitedDictionary.FirstOrDefault(x => x.Key == url).Key);
                    isVisitedDictionary.Add(url, true);

                    IE.GoTo(url);
                    Thread.Sleep(3000);

                    var urls = IE.Links.Where(x => x.Url.Contains("serial_no") && !x.Url.Contains("Board"));
                    foreach (var foundUrl in urls)
                    {
                        if (!goodLinks.Contains(foundUrl.Url))
                        {
                            goodLinks.Add(foundUrl.Url);
                        }
                    }

                    paginator = IE.TableCells.FirstOrDefault(x => x.Elements.Any(y => y.ClassName == "paginate"));
                    if (paginator != null)
                        links = paginator.Links.Where(x => x.Url.Contains("reqPage")).ToList();
                    foreach (var link in links)
                    {
                        if (!isVisitedDictionary.Keys.Any(x => x.Contains(link.Url.Substring(0, link.Url.IndexOf("&")))))
                        {
                            isVisitedDictionary.Add(link.Url, false);
                        }
                    }
                }
                foreach (string goodLink in goodLinks)
                {
                    result.Add(GetGoodFromWebPage(goodLink, IE));
                }
            }

            return result;
        }

        private static void LoginToWebPage(IE IE, string login , string pass)
        {
            IE.GoTo(ETERASHOPLOGINADDRESS);
            Thread.Sleep(3000);
            var loginTextField = IE.TextFields.FirstOrDefault(x => x.Name == "id");
            if (loginTextField != null) loginTextField.TypeText(login);
            var passwordTextField = IE.TextFields.FirstOrDefault(x => x.Name == "password");
            if (passwordTextField != null) passwordTextField.TypeText(pass);

            var loginImage =
                IE.Images.FirstOrDefault(x => x.Uri == new Uri("https://www.eterashop.com/images/login_btn01.gif"));
            loginImage.Click();
        }

        private static Good GetGoodFromWebPage(string url, IE IE)
        {
            var result = new Good();
            IE.GoTo("http://www.eterashop.com/main/detail.php?serial_no=11997");
            Thread.Sleep(1000);
            var table = IE.TableCell(Find.ByText("판매가격 :")).ContainingTable;
            var tableCell = table.TableCells[2].Text;
            result.Price = tableCell;
            var listView = table.SelectLists.FirstOrDefault();
            if (listView != null)
                if (listView.Options.Count > 0)
                    foreach (var option in listView.Options.Skip(1))
                    {
                        result.Colors.Add(option.Text);
                    }
            var newTableCell = table.Parent as TableCell;
            var title = newTableCell.ContainingTable.TableCells[0].Text;
            result.Title = title;
            var div = IE.Divs.Where(x => x.Id == "detail_content1").FirstOrDefault();


            var imageTable = div.Tables[0];
            var description = imageTable.Images[2].Src.Substring(imageTable.Images[2].Src.LastIndexOf(@"/") + 1,
                (imageTable.Images[2].Src.LastIndexOf(@".") - imageTable.Images[2].Src.LastIndexOf(@"/")));
            result.Description = description;

            return result;
        }
    }
}