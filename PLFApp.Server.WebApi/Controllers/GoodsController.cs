using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLFApp.Service;

namespace PLFApp.Server.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Goods")]
    public class GoodsController : Controller
    {
        private readonly IGoodsService goodsService;
        public GoodsController(IGoodsService _goodsService)
        {
            goodsService = _goodsService;
        }
        // GET: api/Goods
        [HttpGet]
        public IEnumerable<string> Get()
        {            
            return new string[] { "value1", "value2" };
        }

        // GET: api/Goods/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Goods
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Goods/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Goods/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("TestService")]
        public string TestService()
        {
            if (goodsService != null)
            {
                return "Service Inject success";
            }
            else
            {
                return "Service Inject fail";
            }
        }
    }
}
