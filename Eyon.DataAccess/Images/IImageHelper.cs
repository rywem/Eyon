using Eyon.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eyon.DataAccess.Images
{
    public interface IImageHelper
    {
        Task<IImageFile> ProcessIImageFile( byte[] imageAsBytes, IImageFile image );
        string GetAWSImageUrl( string key );
        Task<bool> TryDeleteAsync( string key );
    }
}
