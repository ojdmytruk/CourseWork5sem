using System.ComponentModel.DataAnnotations;


namespace DAL.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        [MaxLength(256)]
        [Required(ErrorMessage = "Пусте значення неприпустиме")]
        [Display(Name = "Назва предмету")]
        public string Name { get; set; }

        [Display(Name = "Середній бал по предмету")]
        public double SubjectAvg { get; set; }             
    }
}
