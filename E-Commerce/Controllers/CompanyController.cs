using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class CompanyController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public IActionResult Index()
        {
            var companies = dbContext.Companies.Include(e=>e.Products).ToList();

            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                //Company company = new Company() { Name = Name };
                dbContext.Companies.Add(company);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            //
            return View(company);
        }

        public IActionResult Edit(int companyId)
        {
            var company = dbContext.Companies.Find(companyId);
            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                //Category company = new Category() { Name = Name };
                dbContext.Companies.Update(company);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }
    }
}
