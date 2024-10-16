using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext dbContext = new();

        public IActionResult Index()
        {
            var products = dbContext.Products.Include(e => e.Category).ToList();

            //ViewBag.name = TempData["test"];
            //ViewBag.name = Request.Cookies["success"];

            return View(products);
        }

        public IActionResult Create()
        {
            var categories = dbContext.Categories.ToList().Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() });
            ViewBag.categories = categories;
            Product product = new Product();

            return View(product);
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile ImgUrl)
        {
            if(ModelState.IsValid)
            {
                if (ImgUrl.Length > 0) // 99656
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName); // .png
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        ImgUrl.CopyTo(stream);
                    }

                    product.ImgUrl = fileName;
                }

                dbContext.Products.Add(product);
                dbContext.SaveChanges();

                TempData["success"] = "Add Product Successfully";

                //CookieOptions cookieOptions = new();
                //cookieOptions.Expires = DateTime.Now.AddMinutes(1);

                //Response.Cookies.Append("success", "Add Product Successfully", cookieOptions);

                //HttpContext.Session.ge()

                return RedirectToAction(nameof(Index));
            }

            var categories = dbContext.Categories.ToList();
            ViewBag.categories = categories;
            return View(product);
        }

        public IActionResult Edit(int productId)
        {
            var product = dbContext.Products.Find(productId);
            var categories = dbContext.Categories.ToList();

            //ViewData["categories"] = categories;
            ViewBag.categories = categories;

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile ImgUrl)
        {
            var oldProduct = dbContext.Products.AsNoTracking().FirstOrDefault(e => e.Id == product.Id);
            if (ModelState.IsValid)
            {
                if (ImgUrl != null && ImgUrl.Length > 0) // 99656
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName); // .png
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", oldProduct.ImgUrl);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        ImgUrl.CopyTo(stream);
                    }

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    product.ImgUrl = fileName;
                }
                else
                {
                    product.ImgUrl = oldProduct.ImgUrl;
                }

                dbContext.Products.Update(product);
                dbContext.SaveChanges();

                TempData["success"] = "Update Product Successfully";

                return RedirectToAction(nameof(Index));
            }

            var categories = dbContext.Categories.ToList();

            //ViewData["categories"] = categories;
            ViewBag.categories = categories;
            product.ImgUrl = oldProduct.ImgUrl;
            return View(product);
        }

        public IActionResult Delete(int productId)
        {
            var product = dbContext.Products.Find(productId);

            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", product.ImgUrl);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();

            TempData["success"] = "Delete Product Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
