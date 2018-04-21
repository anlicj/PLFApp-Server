using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLFApp.Server.WebApi.Middleware
{
    public static class CrossSiteAccessExtensions
    {

        public static IApplicationBuilder UseCrossSiteAccess(this IApplicationBuilder builder,CrossSiteAccessOptions options)
        {
            if (options==null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            builder.UseMiddleware<CrossSiteAccessMiddleware>(Options.Create(options));
            return builder;
        }
    }
}
