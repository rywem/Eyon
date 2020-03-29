using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Interfaces
{
    public interface IFile 
    {
        public string FileType { get; set; }
        public string FileName { get; set; }        
    }
}
