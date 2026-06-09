using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        // Tạo một List giả lập để lưu dữ liệu (Static để không bị mất khi load lại trang)
        private static List<Student> students = new List<Student>();
        private static int nextId = 1;

        // 1. Chức năng List (Danh sách)
        // 1. Chức năng List (Danh sách) CÓ KÈM TÌM KIẾM
        public IActionResult Index(string searchString)
        {
            // Lưu lại từ khóa tìm kiếm vào ViewData để hiển thị lại trên ô nhập (giúp người dùng biết họ vừa tìm gì)
            ViewData["CurrentFilter"] = searchString;

            // Bắt đầu với toàn bộ danh sách sinh viên
            var studentsQuery = students.AsQueryable();

            // Kiểm tra xem người dùng có nhập từ khóa tìm kiếm hay không
            if (!string.IsNullOrEmpty(searchString))
            {
                // Chuyển từ khóa về chữ thường để tìm kiếm không phân biệt hoa/thường
                string searchLower = searchString.ToLower();

                // Lọc danh sách: Lấy những sinh viên có Tên hoặc Email chứa từ khóa
                studentsQuery = studentsQuery.Where(s =>
                    (s.Name != null && s.Name.ToLower().Contains(searchLower)) ||
                    (s.Email != null && s.Email.ToLower().Contains(searchLower))
                );
            }

            // Trả về View danh sách đã được lọc
            return View(studentsQuery.ToList());
        }

        // 2. Chức năng Detail (Xem chi tiết)
        public IActionResult Detail(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            return View(student);
        }

        // 3. Chức năng Create (Thêm mới - Hiển thị Form)
        public IActionResult Create()
        {
            return View();
        }

        // 3. Chức năng Create (Thêm mới - Xử lý lưu dữ liệu)
        [HttpPost]
        public IActionResult Create(Student model)
        {
            if (ModelState.IsValid)
            {
                model.Id = nextId++;
                students.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // 4. Chức năng Edit (Sửa - Hiển thị Form kèm dữ liệu cũ)
        public IActionResult Edit(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            return View(student);
        }

        // 4. Chức năng Edit (Sửa - Xử lý lưu dữ liệu)
        [HttpPost]
        public IActionResult Edit(Student model)
        {
            if (ModelState.IsValid)
            {
                var student = students.FirstOrDefault(s => s.Id == model.Id);
                if (student != null)
                {
                    student.Name = model.Name;
                    student.Email = model.Email;
                    student.Phone = model.Phone;
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // 5. Chức năng Delete (Xóa trực tiếp)
        public IActionResult Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
            }
            return RedirectToAction("Index");
        }
    }
}