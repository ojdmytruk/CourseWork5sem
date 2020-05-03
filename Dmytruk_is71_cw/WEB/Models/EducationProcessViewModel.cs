using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class EducationProcessViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Оберіть студента")]
        public int IdStudent { get; set; }

        [Required(ErrorMessage = "Оберіть предмет")]
        public int IdSubject { get; set; }

        [Required(ErrorMessage = "Оберіть групу")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Введіть оцінку")]
        [Range(0, 100, ErrorMessage = "Значення {0} повинно бути в діапазоні між {1} та {2}.")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "Оцінка некоректна")]
        public int SubjectResult { get; set; }
    }
}