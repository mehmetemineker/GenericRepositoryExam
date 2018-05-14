using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GenericRepositoryExam.WebUI.Models;
using GenericRepositoryExam.Core.DataAccess;
using GenericRepositoryExam.Entity;
using Microsoft.EntityFrameworkCore;

namespace GenericRepositoryExam.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork uow;
        private IEntityRepository<Product> productRepository;

        public HomeController(IUnitOfWork uow, IEntityRepository<Product> productRepository)
        {
            this.uow = uow;
            this.productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var test = await productRepository.GetListAsync();

            

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
