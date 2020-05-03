using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using DAL.Entities;

namespace DAL.EF
{
    public class EducationProcessContext : DbContext
    {
        public EducationProcessContext()
            : base("name=EducationProcessContext")
        {
            Database.SetInitializer<EducationProcessContext>(new DataInitializer());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<EducationProcess> Educations { get; set; }
    }

   
}
