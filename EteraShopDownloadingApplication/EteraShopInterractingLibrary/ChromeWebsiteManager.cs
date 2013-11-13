using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EteraShopInterractingLibrary.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WatiN.Core;

namespace EteraShopInterractingLibrary
{
    public class ChromeWebsiteManager
    {
        private const string ETERASHOPADDRESS = "http://www.eterashop.com/main/index.php";

        private const string SEARCHLINK = "http://www.eterashop.com/main/sub.php?search=";

        private const string ETERASHOPLOGINADDRESS =
            "https://www.eterashop.com/login/login_page.php?preurl=http%3A%2F%2Fwww.eterashop.com%2Fmain%2Findex.php%3F";

        public static List<Good> GetGoodsFromWebsite(string login, string pass, List<string> searchWords)
        {
            var result = new List<Good>();

            var webDriver = new ChromeDriver();

            LoginToWebPage(webDriver,login,pass);

            Thread.Sleep(15000);

            

            foreach (var category in searchWords)
            {
                List<string>goodLinks = new List<string>();
                SearchCategory(webDriver, category);

                var isVisitedDictionary = new Dictionary<string, bool>();

                var paginator =
                    webDriver.FindElementByXPath(
                        "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[5]/td[3]/table/tbody/tr[9]/td");

                var linksElements = paginator.FindElements(By.TagName("a")).ToList();
                List<string> links = linksElements.Select(link => link.GetAttribute("href")).ToList();
                foreach (var link in links)
                {
                    if (!isVisitedDictionary.Keys.Any(x => x.Contains(link)))
                    {
                        isVisitedDictionary.Add(link, false);
                    }
                }
                while (isVisitedDictionary.Values.Any(x => x == false))
                {
                    string url = isVisitedDictionary.FirstOrDefault(x => x.Value == false).Key;
                    isVisitedDictionary.Remove(isVisitedDictionary.FirstOrDefault(x => x.Key == url).Key);
                    isVisitedDictionary.Add(url, true);
                    webDriver.Url = url;
                    Thread.Sleep(5000);



                    paginator =
                        webDriver.FindElementByXPath(
                            "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[5]/td[3]/table/tbody/tr[9]/td");

                    linksElements = paginator.FindElements(By.TagName("a")).ToList();
                    links = new List<string>();
                    foreach (var linkElement in linksElements)
                    {
                        links.Add(linkElement.GetAttribute("href"));
                    }
                    foreach (var link in links)
                    {
                        if (
                            !isVisitedDictionary.Keys.Any(
                                x => x.Contains(link)))
                        {
                            isVisitedDictionary.Add(link, false);
                        }
                    }

                    var goodsTable =
                        webDriver.FindElementByXPath(
                            "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[5]/td[3]/table/tbody/tr[8]/td/table");
                    var tags = goodsTable.FindElements(By.TagName("a")).ToList();
                    foreach (var tag in tags)
                    {
                        var href = tag.GetAttribute("href");
                        if (href.Contains("serial_no"))
                            if (!goodLinks.Contains(href))
                                goodLinks.Add(tag.GetAttribute("href"));
                    }
                }

                foreach (var goodLink in goodLinks)
                {
                 result.Add(GetGoodFromWebPage(webDriver, goodLink));   
                }
            }

            return result;
        }

        private static void LoginToWebPage(ChromeDriver chromeDriver, string login, string pass)
        {
            chromeDriver.Url = ETERASHOPLOGINADDRESS;

            var loginTBox = chromeDriver.FindElementByName("id");
            loginTBox.SendKeys(login);
            var passwordTBox= chromeDriver.FindElementByName("password");
            passwordTBox.SendKeys(pass);

            var loginBtn =
                chromeDriver.FindElementByXPath(
                    "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[4]/td[3]/table/tbody/tr[3]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/form/table/tbody/tr[2]/td/table/tbody/tr/td[3]/img");

            loginBtn.Click();
        }

        private static void SearchCategory(ChromeDriver chromeDriver, string searchCategory)
        {
            var categoryTBox = chromeDriver.FindElementByName("search");
            categoryTBox.SendKeys(searchCategory);
            var searchBtn =
                chromeDriver.FindElementByXPath("/html/body/table[1]/tbody/tr/td/table/tbody/tr/td/div/div[5]/a");
            searchBtn.Click();
        }

        private static Good GetGoodFromWebPage(ChromeDriver chromeDriver, string url)
        {
            var result = new Good();
            chromeDriver.Url = url;

            result.Title =
                chromeDriver.FindElementByXPath(
                    "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[4]/td[3]/table/tbody/tr[2]/td/table/tbody/tr/td[3]/table/tbody/tr/td/table/tbody/tr[1]/td")
                    .Text;

            result.Price =
                chromeDriver.FindElementByXPath(
                    "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[4]/td[3]/table/tbody/tr[2]/td/table/tbody/tr/td[3]/table/tbody/tr/td/table/tbody/tr[2]/td[3]/table/tbody/tr[4]/td[2]")
                    .Text;

            var colorsSelect =
                chromeDriver.FindElementByXPath(
                    "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[4]/td[3]/table/tbody/tr[2]/td/table/tbody/tr/td[3]/table/tbody/tr/td/table/tbody/tr[2]/td[3]/table/tbody/tr[6]/td[2]/select");
            var options = colorsSelect.FindElements(By.TagName("option")).ToList();
            foreach (var option in options.Skip(1))
            {
                result.Colors.Add(option.Text);
            }

            var src =
                chromeDriver.FindElementByXPath("//*[@id=\"detail_content1\"]/table/tbody/tr[4]/td/img")
                    .GetAttribute("src");
            result.Description = src.Substring(src.LastIndexOf(@"/") + 1,
                (src.LastIndexOf(@".") - src.LastIndexOf(@"/")));


            return result;
        }
    }
}
