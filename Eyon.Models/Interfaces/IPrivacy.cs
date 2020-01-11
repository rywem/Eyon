using Eyon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    public interface IPrivacy : IRecord
    {
        Privacy Privacy { get; set; }
    }
}
