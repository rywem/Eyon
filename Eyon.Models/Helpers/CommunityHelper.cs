using Eyon.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Helpers
{
    public static class CommunityHelper
    {
        /// <summary>
        /// Formats the communtiy name.
        /// </summary>        
        /// <returns>ex Sacramento, CA (United States)</returns>
        public static string FullNameFormatter(Community community)
        {
            return CommunityFullNameFormatter(community.Name, community.CommunityState != null && community.CommunityState.State != null ? community.CommunityState.State.Name : string.Empty,
                community.CommunityState != null && community.CommunityState.State != null ? community.CommunityState.State.Code : string.Empty, community.Country.Name);
        }

        private static string CommunityFullNameFormatter( string community, string state, string code, string country )
        {
            string stateFormatted = string.Empty;
            if ( !string.IsNullOrEmpty(code) )
                stateFormatted = code;
            else if ( !string.IsNullOrEmpty(state) )
                stateFormatted = state.ToProperCase();

            if ( !string.IsNullOrEmpty(stateFormatted) )
                stateFormatted = string.Format(", {0}", stateFormatted);

            string countryFormatted = string.Empty;
            if ( !string.IsNullOrEmpty(country) )
                countryFormatted = string.Format(" ({0})", country.ToProperCase());

            return string.Format("{0}{1}{2}", community.ToProperCase(), stateFormatted, countryFormatted);
        }
    }
}
