using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SallersController : Controller
    {
        //se o controller precisa do service, precisamos criar uma dependencia de service
        private readonly SallerService _sallerService;
        public SallersController(SallerService sallerService)
        {
            _sallerService = sallerService;
        }

        public IActionResult Index()
        {
            var list = _sallerService.FindAll();

            return View(list);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]//Previnir que sofra ataque CSRF
        public IActionResult Create(Saller saller)
        {
            _sallerService.Insert(saller);

            //redirecionando para o index usando nameof para caso o string mude, facil manuntencao
            return RedirectToAction(nameof(Index));
        }
    }
}
