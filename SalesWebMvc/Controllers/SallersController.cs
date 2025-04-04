using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Controllers
{
    public class SallersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
