using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

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

            //definindo a regra de campo no lado do servidor
            if (!ModelState.IsValid)
            {

                var departments = _departmentService.FindAll();

                var viewModel = new SallerFormViewModel { Saller = saller, Departments = departments };

                return View(viewModel);
            }

            _sallerService.Insert(saller);



            //redirecionando para o index usando nameof para caso o string mude, facil manuntencao
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new{message="Id Not provided"});
            }
            //colocando o value porque o obj ele pode ser nullable
            var obj = _sallerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
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
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }
            //colocando o value porque o obj ele pode ser nullable
            var obj = _sallerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = _sallerService.FindById(id.Value);

            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            List<Department> departments = _departmentService.FindAll();

            SallerFormViewModel viewModel = new SallerFormViewModel { Saller=obj,Departments=departments};

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Saller saller)
        {

            //definindo a regra de campo no lado do servidor
            if (!ModelState.IsValid)
            {

                var departments= _departmentService.FindAll();

                var viewModel = new SallerFormViewModel { Saller=saller,Departments=departments};

                return View(viewModel);
            }

            if (id != saller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" }); ;
            }

            try
            {
                _sallerService.Update(saller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                //pegando o Id interno do error
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
