using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLFApp.Server.Core;

namespace PLFApp.Server.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/GoodsCategory")]
    public class GoodsCategoryController : Controller
    {
        public IEnumerable<GoodsCategory> Get()
        {
            var result = new List<GoodsCategory>();
            result.Add(new GoodsCategory() {
                GoodsCategoryName="天猫",
                CategoryImageSrc=""
            });
            return result;
        }
    }
}