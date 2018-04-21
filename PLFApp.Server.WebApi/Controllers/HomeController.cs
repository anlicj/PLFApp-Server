using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLFApp.Service.Dto;

namespace PLFApp.Server.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : Controller
    {
        [HttpGet("GetTopScrollImage")]
        public JsonResult GetTopScrollImage()
        {
            var result = new List<HomeTopScrollImage>();
            result.Add(new HomeTopScrollImage() {
                ImageSrc = @"images/10225357963.jpg",
                ImageUrl = @"https://www.baidu.com/",
                ImageTitle="百度",
                SortCode=1
            });
            result.Add(new HomeTopScrollImage()
            {
                ImageSrc = @"images/10250290397.png",
                ImageUrl = @"https://www.taobao.com/",
                ImageTitle = "淘宝",
                SortCode = 2
            });
            return Json(result);
        }
    }
}