using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLFApp.Server.WebApi.Middleware
{
    public class ApiAuthorizedMiddleware
    {
        private RequestDelegate next;

        public ApiAuthorizedMiddleware(RequestDelegate _next)
        {
            this.next = _next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await next.Invoke(httpContext);
        }

    }
}
