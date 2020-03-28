using System;
using System.Collections.Generic;
using System.Text;
using Eyon.Utilities.Extensions;
namespace Eyon.XConsole
{
    public class Encryptor
    {
        public string EncyrptString(string unencryptedValue, string key, string iv)
        {
            string encryptedValue = unencryptedValue.Encrypt(key, iv);
            Console.WriteLine(encryptedValue);
            return encryptedValue;
        }
    }
}
