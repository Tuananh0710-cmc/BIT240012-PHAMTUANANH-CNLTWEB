using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private const string StudentName = "Phạm Tuấn Anh";
        private const string StudentEmail = "panhhh2006@gmail.com";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.StudentName = StudentName;
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.StudentEmail = StudentEmail;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
