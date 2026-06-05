using BMB5.Models;
using Microsoft.AspNetCore.Mvc;

namespace BMB5.Controllers
{
    public class BookController : Controller
    {
        private static readonly List<Book> _books =
        [
            new Book { Id = 1, Name = "Clean Code", Price = 20 },
            new Book { Id = 2, Name = "ASP.NET MVC", Price = 15 },
            new Book { Id = 3, Name = "Design Pattern", Price = 25 }
        ];

        public IActionResult Index()
        {
            return View(_books);
        }

        public IActionResult Detail(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            book.Id = _books.Max(b => b.Id) + 1;
            _books.Add(book);
            TempData["SuccessMessage"] = "Thêm sách thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
