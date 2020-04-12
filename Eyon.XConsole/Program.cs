using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Eyon.Utilities;
using System.Collections.Generic;

namespace Eyon.XConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using ( Eyon.Utilities.API.AwsCsvHelper helper = new Utilities.API.AwsCsvHelper() )
            {
                var key = helper.GetKey();
                Console.WriteLine(key);
                var iv = helper.GetIV();
                Console.WriteLine(iv);
            }
        }



        //public static string GetWikipediaURL(string city, string state, string country)
        //{
        //    string cityUrl = city.Replace(" ", "_").ToUpper();
        //    Console.WriteLine(cityUrl);
        //    //listBox1.Items.Clear();
        //    StringBuilder sb = new StringBuilder();
        //    byte[] ResultsBuffer = new byte[8192];
        //    string wikipedia = "wikipedia";
        //    string searchString = string.Format("{0}+{1}+{2}+{3}", city.Replace(" ", "+"), state.Replace(" ", "+"), country.Replace(" ", "+"), wikipedia);
        //    string SearchResults = "http://google.com/search?q=" + searchString.Trim();
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SearchResults);
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    Stream resStream = response.GetResponseStream();
        //    string tempString = null;
        //    int count = 0;
        //    do
        //    {
        //        count = resStream.Read(ResultsBuffer, 0, ResultsBuffer.Length);
        //        if ( count != 0 )
        //        {
        //            tempString = Encoding.ASCII.GetString(ResultsBuffer, 0, count);
        //            sb.Append(tempString);
        //        }
        //    }

        //    while ( count > 0 );
        //    string sbb = sb.ToString();

        //    HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
        //    html.OptionOutputAsXml = true;
        //    html.LoadHtml(sbb);
        //    HtmlNode doc = html.DocumentNode;
        //    foreach ( HtmlNode link in doc.SelectNodes("//a[@href]") )
        //    {                
        //        string hrefValue = link.GetAttributeValue("href", string.Empty);
                
        //        if(hrefValue.ToString().ToUpper().StartsWith(@"/url?q=https://en.wikipedia.org/wiki/".ToUpper()))
        //        {
                    
        //            if ( hrefValue.ToString().ToUpper().Contains(cityUrl) )
        //            {                        
        //                int index = hrefValue.IndexOf("&");
        //                if ( index > 0 )
        //                {
        //                    string newHrefValue = hrefValue.Substring(0, index);
        //                    string toRemove = "/url?q=";
        //                    newHrefValue = newHrefValue.Substring(toRemove.Length, newHrefValue.Length - toRemove.Length);

        //                    Console.WriteLine(newHrefValue);

        //                    if ( newHrefValue.Contains("%") )
        //                    {
        //                        index = newHrefValue.IndexOf("%");
        //                        newHrefValue = hrefValue.Substring(0, index);
        //                    }
        //                    return newHrefValue;
        //                }
        //            }
        //        }                
        //    }
        //    return "___";
        //}
    }
}
