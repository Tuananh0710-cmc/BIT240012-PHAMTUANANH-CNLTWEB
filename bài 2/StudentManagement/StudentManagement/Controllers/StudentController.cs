using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Info()
        {
            ViewBag.Name = "Phạm Tuấn Anh";
            ViewData["Age"] = 19;

            var model = new StudentInfo
            {
                Major = "CNTT"
            };

            return View(model);
        }
    }
}
