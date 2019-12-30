using System;
using System.IO;
using System.Net;
using System.Text;
using Eyon.XTests.UnitTests.DataAccess.Data.Orchestator;
using HtmlAgilityPack;
using Eyon.XConsole.Seeding;
using System.Linq;
using Eyon.Utilities;
using Eyon.Models;
using Eyon.DataAccess;
using System.Collections.Generic;
using Eyon.Models.Relationship;
using Eyon.DataAccess.Data;
using Eyon.DataAccess.Data.Repository.IRepository;

namespace Eyon.XConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            string pathToFile = "..\\..\\..\\..\\Eyon.DataAccess\\Seed_Data\\Location\\free-zipcode-database-Primary.csv";

            var zips = LocationHelper.LoadZipcodes(pathToFile);
            //var zipsList = zips.ToList();

            var deduped = zips.DistinctBy(x => x.CityState).ToList();

            string pathToStatesFile = "..\\..\\..\\..\\Eyon.DataAccess\\Seed_Data\\Location\\states_usa.csv";
            var states = LocationHelper.LoadStates(pathToStatesFile);
            List<CommunityState> communityStates = new List<CommunityState>();
            List<Community> communities = new List<Community>();
            long id = 4;            
            //IUnitOfWork unitOfWork = new UnitOfWork();
            foreach ( var item in deduped )
            {
                //Console.WriteLine(string.Format("city {0} \t state: {1} \t zip {2}", item.City, item.State, item.Zipcode));
                long stateId = states.FirstOrDefault(x => x.Code == item.State).Id;
                //TODO get State ID
                Community community = new Community()
                {
                    Id = id,
                    Name = item.City,
                    CountryId = 192,
                    WikipediaURL = Eyon.Utilities.Seeding.CommunityCreator.GetWikipediaURL(item.City, item.State, "United States"),
                    Active = true
                };
                communities.Add(community);

                CommunityState communityState = new CommunityState()
                {
                    CommunityId = id,
                    StateId = stateId
                };

                id++;
            }
            
            
            //string city = "deer river";
            //string state = "MN";

            //string country = "united+states";

            //try
            //{
            //    GetWikipediaURL(city, state, country);
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            Console.WriteLine("Finished");
            Console.ReadLine();
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
