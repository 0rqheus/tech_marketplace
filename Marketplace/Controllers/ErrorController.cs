using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class ErrorController : Controller
    {
        [Route("500")]
        public IActionResult Index()
        {
            Response.StatusCode = 500;
            return View();
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        [Route("403")]
        public IActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }

        public IActionResult Banned()
        {
            return View();
        }
    }
}