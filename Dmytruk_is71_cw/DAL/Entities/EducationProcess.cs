using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class EducationProcess
    {
        public int Id { get; set; }

        public Student Student { get; set; }
        [Required]
        public int IdStudent { get; set; }

        public Subject Subject { get; set; }
        [Required]
        public int IdSubject { get; set; }

        public string GroupName { get; set; }

        [Display(Name = "Оцінка")]
        [Range(0, 100, ErrorMessage = "Оцінка повинна бути в діапазоні 0-100")]
        public int SubjectResult { get; set; }
    }
}
