using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.Repository.IRepository;
using E_Commerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Authorize(Roles = $"{SD.adminRole},{SD.CompanyRole}")]
    public class CategoryController : Controller
    {
        //ApplicationDbContext dbContext = new ApplicationDbContext();
        ICategoryRepository categoryRepository;// = new CategoryRepository();

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            //var categories = dbContext.Categories.Include("Products").ToList();
            var categories = categoryRepository.GetAll([e => e.Products]);

            return View(categories.ToList());
        }

        public IActionResult Create()
        {
            Category category = new Category();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                //Category category = new Category() { Name = Name };
                categoryRepository.Add(category);
                categoryRepository.Commit();

                return RedirectToAction(nameof(Index));
            }
            //
            return View(category);
        }


        public IActionResult Edit(int categoryId)
        {
            var category = categoryRepository.GetOne([e=>e.Id == categoryId]);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                //Category category = new Category() { Name = Name };
                categoryRepository.Edit(category);
                categoryRepository.Commit();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public IActionResult Delete(int categoryId)
        {
            Category category = new Category() { Id = categoryId };
            categoryRepository.Delete(category);
            categoryRepository.Commit();

            return RedirectToAction(nameof(Index));
        }
    }
}
