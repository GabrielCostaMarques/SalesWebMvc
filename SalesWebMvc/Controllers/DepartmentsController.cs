﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {

            List<Department> departments = new List<Department>();

            departments.Add(new Department { Id = 1, Name = "Eletronicos" });
            departments.Add(new Department { Id = 2, Name = "Fashion" });

            //enviar a lista do controller para a View
            return View(departments);
        }
    }
}
