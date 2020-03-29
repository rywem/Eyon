using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CsvHelper;
using System.Linq;

namespace Eyon.Utilities.API
{
    public class AwsCsvHelper : IDisposable
    {
        private KeyFile _keyFile { get; set; }

        string _pathToFile { get; set; }
        public AwsCsvHelper() : this("config.csv")
        {

        }
        public AwsCsvHelper(string pathToFile)
        {
            _pathToFile = pathToFile;            
        }

        public void Initialize()
        {
            if ( File.Exists(_pathToFile) )
            {
                using ( var reader = new StreamReader(_pathToFile) ) 
                { 

                    using ( var csv = new CsvReader(reader) )
                    {
                        csv.Configuration.HasHeaderRecord = true;

                        var records = csv.GetRecords<KeyFile>().ToList();
                        _keyFile = records[0];
                    }
                }
            }
        }

        public string GetKey()
        {
            if ( _keyFile == null )
                Initialize();
            return _keyFile.key;
        }

        public string GetIV()
        {
            if ( _keyFile == null )
                Initialize();
            return _keyFile.iv;
        }

        public void Dispose()
        {
            _keyFile.key = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            _keyFile.iv = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            _keyFile.key = null;
            _keyFile.iv = null;
            _keyFile = null;
        }

        public class KeyFile 
        {
            public string key { get; set; }
            public string iv { get; set; }


        }
    }
}
