using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLFApp.Server.WebApi.Middleware
{
    public class CrossSiteAccessOptions
    {
        public string[] AllowOriginSites { get; set; }
    }
}
