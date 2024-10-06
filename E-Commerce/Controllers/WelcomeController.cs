using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Index()
        {
            //string name = "Mohamed Nabih From C#";
            var result = new List<string>() { "Ahmed", "Mohamed", "Ali", "Mona" };

            return View(model: result);
        }
    }
}
