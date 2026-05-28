using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    public class BookController : Controller
    {
        // Danh sách sách
        public IActionResult Index()
        {
            var books = new List<dynamic>()
            {
                new { Id = 1, Name = "Clean Code", Price = 20 },
                new { Id = 2, Name = "ASP.NET MVC", Price = 15 },
                new { Id = 3, Name = "Design Pattern", Price = 25 }
            };

            return View(books);
        }
    }
}