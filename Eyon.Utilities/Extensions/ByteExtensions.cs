using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Eyon.Utilities.Extensions
{
    public static class ByteExtensions
    {
        public static string ToHex( this byte[] bytes )
        {
            char[] c = new char[bytes.Length * 2];

            byte b;

            for ( int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx )
            {
                b = ( (byte)( bytes[bx] >> 4 ) );
                c[cx] = (char)( b > 9 ? b + 0x37 + 0x20 : b + 0x30 );

                b = ( (byte)( bytes[bx] & 0x0F ) );
                c[++cx] = (char)( b > 9 ? b + 0x37 + 0x20 : b + 0x30 );
            }
            return new string(c);
        }

        /// <summary>
        /// Run key through RIPEMD128 algorithm to ensure proper number of bytes
        /// </summary>
        /// <param name="bytesToHash">The input key, in bytes</param>
        /// <returns>A hashed key</returns>
        public static byte[] GetMD5( this byte[] bytesToHash )
        {
            try
            {
                using ( MD5 md5Hash = MD5.Create() )
                {
                    return md5Hash.ComputeHash(bytesToHash);
                }
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
        public static byte[] GetSHA256( this byte[] bytesToHash )
        {
            SHA256Managed hashstring = new SHA256Managed();
            return hashstring.ComputeHash(bytesToHash);
        }
    }
}
