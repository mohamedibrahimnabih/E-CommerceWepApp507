﻿using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public IActionResult Index()
        {
            var categories = dbContext.Categories.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            //Category category = new Category() { Name = Name };
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int categoryId)
        {
            var category = dbContext.Categories.Find(categoryId);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            //Category category = new Category() { Name = Name };
            dbContext.Categories.Update(category);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int categoryId)
        {
            Category category = new Category() { Id = categoryId };
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
