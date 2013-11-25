using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading;
using EteraShopInterractingLibrary.Domain;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using WatiN.Core;

namespace EteraShopInterractingLibrary
{
    public class FirefoxWebsiteManager
    {
        private const string ETERASHOPADDRESS = "http://www.eterashop.com/main/index.php";

        private const string SEARCHLINK = "http://www.eterashop.com/main/sub.php?search=";

        private const string ETERASHOPLOGINADDRESS =
            "https://www.eterashop.com/login/login_page.php?preurl=http%3A%2F%2Fwww.eterashop.com%2Fmain%2Findex.php%3F";

        public static List<Good> GetGoodsFromWebsite(string login, string pass, List<string> searchWords, string startupFolder)
        {
            var result = new List<Good>();

            //var options = new ChromeOptionsWithPrefs();
            //options.prefs = new Dictionary<string, object>();
            //var imagesDictionary = new Dictionary<string, object>();
            //imagesDictionary.Add("images", 2);
            //options.prefs.Add("profile.default_content_settings", imagesDictionary);

            var webDriver = new FirefoxDriver();


            LoginToWebPage(webDriver,login,pass);

            Thread.Sleep(3000);

            foreach (var category in searchWords)
            {
                List<string> goodLinks = new List<string>();
                SearchCategory(webDriver, category);
                Thread.Sleep(3000);
                var isVisitedDictionary = new Dictionary<string, bool>();
                IWebElement paginator = null;
                try
                {
                    paginator =
                        webDriver.FindElementByXPath(
                            "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[5]/td[3]/table/tbody/tr[9]/td");
                }
                catch (Exception)
                {

                }
                if (paginator != null)
                {
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
                        result.Add(GetGoodFromWebPage(webDriver, goodLink, startupFolder));
                    }
                }
            }
            webDriver.Close();
            return result;
        }

        public static void AddGoodsToCart(string login, string pass, List<GoodCart> goodCarts)
        {
            
        }

        private static void LoginToWebPage(FirefoxDriver firefoxDriver, string login, string pass)
        {
            firefoxDriver.Url = ETERASHOPLOGINADDRESS;

            var loginTBox = firefoxDriver.FindElementByName("id");
            loginTBox.SendKeys(login);
            var passwordTBox= firefoxDriver.FindElementByName("password");
            passwordTBox.SendKeys(pass);

            var loginBtn =
                firefoxDriver.FindElementByXPath(
                    "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[4]/td[3]/table/tbody/tr[3]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/form/table/tbody/tr[2]/td/table/tbody/tr/td[3]/img");

            loginBtn.Click();
        }

        private static void SearchCategory(FirefoxDriver firefoxDriver, string searchCategory)
        {
            var categoryTBox = firefoxDriver.FindElementByName("search");

            string strOut = "";

            int euckrCodepage = 949;//949;//51949;

            System.Text.Encoding originalEncoding = System.Text.Encoding.GetEncoding(1252);


            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(euckrCodepage);
            StringBuilder sbEncoding = new StringBuilder();


            sbEncoding.Append(searchCategory);


            byte[] rawbytes = originalEncoding.GetBytes(searchCategory);


            string s = euckr.GetString(rawbytes);

            strOut = sbEncoding.ToString();

            categoryTBox.SendKeys(strOut);
            var searchBtn =
                firefoxDriver.FindElementByXPath("/html/body/table[1]/tbody/tr/td/table/tbody/tr/td/div/div[5]/a");
            searchBtn.Click();
        }

        private static Good GetGoodFromWebPage(FirefoxDriver firefoxDriver, string url, string startupFolder)
        {
            var result = new Good();
            firefoxDriver.Url = url;
            Thread.Sleep(3000);

            result.Title =
                firefoxDriver.FindElementByXPath(
                    "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[4]/td[3]/table/tbody/tr[2]/td/table/tbody/tr/td[3]/table/tbody/tr/td/table/tbody/tr[1]/td")
                    .Text;

            result.Price =
                firefoxDriver.FindElementByXPath(
                    "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[4]/td[3]/table/tbody/tr[2]/td/table/tbody/tr/td[3]/table/tbody/tr/td/table/tbody/tr[2]/td[3]/table/tbody/tr[4]/td[2]")
                    .Text;

            var colorsSelect =
                firefoxDriver.FindElementByXPath(
                    "/html/body/table[2]/tbody/tr[1]/td/table/tbody/tr[4]/td[3]/table/tbody/tr[2]/td/table/tbody/tr/td[3]/table/tbody/tr/td/table/tbody/tr[2]/td[3]/table/tbody/tr[6]/td[2]/select");
            var options = colorsSelect.FindElements(By.TagName("option")).ToList();
            foreach (var option in options.Skip(1))
            {
                result.Colors.Add(option.Text);
            }

            var src =
                firefoxDriver.FindElementByXPath("//*[@id=\"detail_content1\"]/table/tbody/tr[4]/td/img")
                    .GetAttribute("src");
            result.Description = src.Substring(src.LastIndexOf(@"/") + 1,
                (src.LastIndexOf(@".") - src.LastIndexOf(@"/")));

            WebRequest req = WebRequest.Create(src);
            WebResponse response = req.GetResponse();
            Stream stream = response.GetResponseStream();
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            image.Save(startupFolder+@"\Images\"+ result.Description+".jpg");
            return result;
        }
    
    }
    public class ChromeOptionsWithPrefs : ChromeOptions
    {
        public Dictionary<string, object> prefs { get; set; }
    }
}
