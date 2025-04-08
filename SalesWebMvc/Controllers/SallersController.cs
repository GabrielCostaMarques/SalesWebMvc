using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SallersController : Controller
    {
        //se o controller precisa do service, precisamos criar uma dependencia de service
        private readonly SallerService _sallerService;
        private readonly DepartmentService _departmentService;

        public SallersController(SallerService sallerService, DepartmentService departmentService)
        {
            _sallerService = sallerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sallerService.FindAll();

            return View(list);
        }


        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();

            var viewModel = new SallerFormViewModel { Departments = departments };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]//Previnir que sofra ataque CSRF
        public IActionResult Create(Saller saller)
        {
            _sallerService.Insert(saller);

            //redirecionando para o index usando nameof para caso o string mude, facil manuntencao
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //colocando o value porque o obj ele pode ser nullable
            var obj = _sallerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sallerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //colocando o value porque o obj ele pode ser nullable
            var obj = _sallerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
    }
}
