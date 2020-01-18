using Eyon.Models.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class SafeErrorViewModel
    {
        public string SafeMessage { get; set; }

        public SafeErrorViewModel(string safeMessage)
        {
            this.SafeMessage = safeMessage;
        }

        public SafeErrorViewModel(SafeException safeException)
        {
            this.SafeMessage = safeException.SafeMessage;
        }
    }
}
