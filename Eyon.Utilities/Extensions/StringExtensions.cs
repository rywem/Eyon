﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Eyon.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string ToProperCase(this string s, CultureInfo culture = null)
        {
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
    }
}
