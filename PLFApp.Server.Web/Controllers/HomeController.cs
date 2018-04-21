using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PLFApp.Server.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("欢迎使用ASP.NET Core MVC");
        }
    }
}