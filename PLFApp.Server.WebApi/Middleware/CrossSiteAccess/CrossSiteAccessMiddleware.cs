using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLFApp.Server.WebApi.Middleware
{
    public class CrossSiteAccessMiddleware
    {
        private RequestDelegate next;
        private IOptions<CrossSiteAccessOptions> options;
        public CrossSiteAccessMiddleware(RequestDelegate _next, IOptions<CrossSiteAccessOptions> _options)
        {
            next = _next;
            options = _options;
        }
        public async Task Invoke(HttpContext context)
        {
            if (options.Value.AllowOriginSites != null && options.Value.AllowOriginSites.Length > 0)
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", string.Join(',', options.Value.AllowOriginSites));
                context.Response.Headers.Add("Access-Control-Allow-Methods","*");
            }
            await next.Invoke(context);
        }
    }
}
