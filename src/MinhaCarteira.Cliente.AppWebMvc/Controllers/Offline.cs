using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MinhaCarteira.Cliente.AppWebMvc.Controllers
{
    public class Offline : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
