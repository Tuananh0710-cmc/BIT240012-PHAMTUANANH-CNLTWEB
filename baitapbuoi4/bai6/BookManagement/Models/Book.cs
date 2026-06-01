using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string Name { get; set; }

        [Range(1, 1000, ErrorMessage = "Giá phải lớn hơn 0")]
        public int Price { get; set; }
    }
}