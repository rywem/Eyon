using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.XConsole.Seeding
{
    public class ZipCodeFile
    {
        public string Zipcode { get; set; }
        public string ZipCodeType { get; set; } 
        public string City { get; set; } 
        public string State { get; set; } 
        public string LocationType { get; set; } 
        public string Lat { get; set; } 
        public string Long { get; set; } 
        public string Location { get; set; } 
        public string Decommisioned { get; set; } 
        public string TaxReturnsFiled { get; set; } 
        public string EstimatedPopulation { get; set; } 
        public string TotalWages { get; set; }

        public string CityState
        {
            get
            {
                return City + State;
            }
        }
    }
}
