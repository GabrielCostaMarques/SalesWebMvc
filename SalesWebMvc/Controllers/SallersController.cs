using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

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

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var obj = _sallerService.FindById(id.Value);

            if(obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();

            SallerFormViewModel viewModel = new SallerFormViewModel { Saller=obj,Departments=departments};

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Saller saller)
        {
            if (id != saller.Id) {
                return BadRequest();
            }

            try
            {
                _sallerService.Update(saller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {

                return BadRequest();
            }
        }
    }
}
