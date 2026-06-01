using Microsoft.AspNetCore.Mvc;
using BookManagement.Models;

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

        // Chi tiết sách
        public IActionResult Detail(int id)
        {
            var book = new
            {
                Id = id,
                Name = "Clean Code",
                Price = 20
            };

            return View(book);
        }

        // Hiển thị form thêm sách
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Xử lý thêm sách
        [HttpPost]
        public IActionResult Add(Book book)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Thêm sách thành công!";
            }

            return View(book);
        }
    }
}