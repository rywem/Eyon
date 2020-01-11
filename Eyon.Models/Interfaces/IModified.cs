using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    public interface IModified
    {
        /// <summary>
        /// Add to migration: defaultValueSql: "GETUTCDATE()"
        /// 
        /// See 20200111135923_AddOwnershipToCookbook.cs
        /// </summary>
        DateTime ModifiedDateTime { get; set; }
    }
}
