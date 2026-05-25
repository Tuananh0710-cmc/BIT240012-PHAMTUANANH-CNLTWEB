using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Detail(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.ErrorMessage = "Thiếu tham số id. Vui lòng truy cập URL dạng /Product/Detail/5";
                return View();
            }

            ViewBag.ProductId = id.Value;
            return View();
        }

        public IActionResult Category(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ViewBag.ErrorMessage = "Thiếu tham số name. Vui lòng truy cập URL dạng /Product/Category?name=Laptop";
                return View();
            }

            ViewBag.CategoryName = name;
            return View();
        }
    }
}
