using Eyon.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
namespace Eyon.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string ToHtmlEncode(this string value)
        {
            return HttpUtility.HtmlEncode(value);
        }

        public static string ToHtmlDecode(this string value )
        {
            return HttpUtility.HtmlDecode(value); 
        }
        public static string ToProperCase(this string s, CultureInfo culture = null)
        {
            if ( string.IsNullOrEmpty(s) )
                return String.Empty;

            TextInfo ti = null;
            if ( culture == null )
                ti = CultureInfo.CurrentCulture.TextInfo;
            else
                ti = culture.TextInfo;

            return ti.ToTitleCase(s.ToLower());
        }

        public static bool ContainsAny( this string str, params string[] values )
        {
            if ( !string.IsNullOrEmpty(str) || values.Length > 0 )
            {
                foreach ( string value in values )
                {
                    if ( str.Contains(value) )
                        return true;
                }
            }
            return false;
        }


        public static string Encrypt( this string unencryptedString, string key, string iv )
        {
            if ( string.IsNullOrEmpty(unencryptedString) )
                throw new ArgumentNullException("unencryptedString cannot be null or empty");
            if ( key == null )
                throw new ArgumentNullException("key cannot be null or empty");
            string result = null;
            byte[] plaintextBytes = Encoding.ASCII.GetBytes(unencryptedString);
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] ivBytes = Encoding.ASCII.GetBytes(iv);
            result = Encryption.Encrypt(unencryptedString, keyBytes, ivBytes).ToHex();
            return result;
        }

        public static string Decrypt( this string encryptedString, string key, string iv )
        {
            if ( string.IsNullOrEmpty(encryptedString) )
                throw new ArgumentNullException("unencryptedString cannot be null or empty");
            if ( string.IsNullOrEmpty(key) )
                throw new ArgumentNullException("key cannot be null or empty");
            string result = null;

            if ( !String.IsNullOrEmpty(encryptedString) )
            {
                byte[] encryptedBytes = encryptedString.FromHex();
                byte[] keyBytes = Encoding.ASCII.GetBytes(key);
                byte[] ivBytes = Encoding.ASCII.GetBytes(iv);
                result = Encryption.Decrypt(encryptedBytes, keyBytes, ivBytes);
            }
            return result;
        }

        public static byte[] FromHex( this string hex )
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for ( int i = 0; i < raw.Length; i++ )
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
    }
}
