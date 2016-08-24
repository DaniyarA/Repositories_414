using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class Book
    {
        // ID книги
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        // название книги

        [Required (ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        // автор книги

        [Required]
        [StringLength(50)]
        [Display(Name = "Автор")]
        public string Author { get; set; }
        // цена

        [Required]
        [RegularExpression(@"[0-9]{1-6}", ErrorMessage = "Некорректная цена")]
        [Display(Name = "Цена")]
        public int Price { get; set; }
    }
}