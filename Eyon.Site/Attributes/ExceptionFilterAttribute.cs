using Eyon.Models.Errors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eyon.Site.Attributes
{
    public class ExceptionFilterAttribute : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public ExceptionFilterAttribute( IWebHostEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider )
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }
        public void OnException( ExceptionContext filterContext)
        {            
            if ( _hostingEnvironment.IsDevelopment() )
            {
                return;
            }

            if ( filterContext.Exception.GetType() == typeof(SafeException) )
            {
                //Do stuff
                //You'll probably want to change the 
                //value of 'filterContext.Result'
                filterContext.ExceptionHandled = true;
            }
        }
    }

    public class ExceptionFilterAsyncAttribute : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync( ExceptionContext context )
        {
            throw new NotImplementedException();
        }
    }
}
