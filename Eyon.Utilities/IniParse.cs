using IniParser;
using IniParser.Model;

namespace Eyon.Utilities
{
    /// <summary>
    /// https://github.com/rickyah/ini-parser
    /// </summary>
    public class IniParse
    {
        private FileIniDataParser _parser;
        private IniData _data;
        public IniParse() : this("config.ini")
        {
            
        }
        public IniParse(string fileName)
        {
            _parser = new FileIniDataParser();
            _data = _parser.ReadFile(fileName);
        }

        public string Get(string section, string key)
        {
            return _data[section][key];
        }
    }
}
