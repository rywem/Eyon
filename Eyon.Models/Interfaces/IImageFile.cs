using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    public interface IImageFile : IRecord, IFile, IDescription
    {
        public string FileNameThumb { get; set; }
        public string Image { get; set; }
        public string Thumb { get; set; }
    }
}
