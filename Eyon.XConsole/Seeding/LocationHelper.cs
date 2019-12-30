using CsvHelper;
using Eyon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Eyon.XConsole.Seeding
{
    public class LocationHelper
    {
        public static List<ZipCodeFile> LoadZipcodes(string pathToFile)
        {
            if ( File.Exists(pathToFile) )
            {
                using ( var reader = new StreamReader(pathToFile) )
                using ( var csv = new CsvReader(reader) )
                {
                    csv.Configuration.HasHeaderRecord = true;

                    var records = csv.GetRecords<ZipCodeFile>().ToList();
                    return records;
                }
            }
            return null;
        }

        public static List<State> LoadStates( string pathToFile )
        {
            if ( File.Exists(pathToFile) )
            {
                using ( var reader = new StreamReader(pathToFile) )
                using ( var csv = new CsvReader(reader) )
                {
                    csv.Configuration.HasHeaderRecord = true;

                    var records = csv.GetRecords<State>().ToList();
                    return records;
                }
            }
            return null;
        }
    }
}
