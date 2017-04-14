using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace CardDataFinder
{
    internal class Program
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    string metascore = doc.DocumentNode.SelectNodes("//*[@id=\"main\"]/div[3]/div/div[2]/div[1]/div[1]/div/div/div[2]/a/span[1]")[0].InnerText;
        //    string userscore = doc.DocumentNode.SelectNodes("//*[@id=\"main\"]/div[3]/div/div[2]/div[1]/div[2]/div[1]/div/div[2]/a/span[1]")[0].InnerText;
        //    string summary = doc.DocumentNode.SelectNodes("//*[@id=\"main\"]/div[3]/div/div[2]/div[2]/div[1]/ul/li/span[2]/span/span[1]")[0].InnerText;
        //}

        #region Methods

        private static void Main(string[] args)
        {
            string Url = "http://combodeck.net/Card/Mind_Rot";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(Url);
            //HtmlNode node = doc.GetElementbyId("searchResults");
            //XPath   "/html[1]/body[1]/div[2]/div[2]/div[4]/div[1]/div[1]/div[1]/img[1]"

            //*[@id="printingInfo"]/div/div[1]/img

            HtmlNode node = doc.DocumentNode.SelectNodes("//*[@id=\"printingInfo\"]/div/div[1]/img")[0];

            HtmlAttributeCollection a = node.Attributes;
            string imageLink = a.First(x => x.Name == "src").Value;

            string cardName = doc.DocumentNode.SelectNodes("//*[@id=\"moduleCard\"]/h1/span[1]")[0].InnerText;
            HtmlNodeCollection m = doc.DocumentNode.SelectNodes("//*[@id=\"moduleCard\"]/h1/span[2]");
            HtmlNodeCollection manaCostNodes = m;
            List<string> manaStrings = new List<string>();

            foreach (HtmlNode manaNode in manaCostNodes)
            {
                manaStrings.Add(manaNode.Attributes.First(x => x.Name == "class").Value);
            }

            //a = node.Attributes;
            //string cardName = a.First(x => x.Name == "")
        }

        #endregion Methods
    }
}