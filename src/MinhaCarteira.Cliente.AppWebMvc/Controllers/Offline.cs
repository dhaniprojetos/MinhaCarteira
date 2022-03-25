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
