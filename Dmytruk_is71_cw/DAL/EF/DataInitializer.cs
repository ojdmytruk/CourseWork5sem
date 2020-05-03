using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using DAL.Entities;

namespace DAL.EF
{
    class DataInitializer : DropCreateDatabaseAlways<EducationProcessContext>
    {
        protected override void Seed(EducationProcessContext context)
        {
            base.Seed(context);


            context.Groups.Add(new Group { Name = "ІC-71" });
            context.Groups.Add(new Group { Name = "ІC-72" });
            context.Groups.Add(new Group { Name = "ІC-73" });

            context.Students.Add(new Student { Name = "Алпаєва Юлія", IdGroup = 1 });
            context.Students.Add(new Student { Name = "Андрощук Юрій", IdGroup = 1 });
            context.Students.Add(new Student { Name = "Большой Олександр", IdGroup = 1 });
            context.Students.Add(new Student { Name = "Березянко Катерина", IdGroup = 2 });
            context.Students.Add(new Student { Name = "Кашич Дмитрій", IdGroup = 3 });

            context.Subjects.Add(new Subject { Name = "Технології програмування" });
            context.Subjects.Add(new Subject { Name = "Бази даних" });
            context.Subjects.Add(new Subject { Name = "Архітектура комп'ютера" });

            context.Educations.Add(new EducationProcess { IdStudent = 1, IdSubject = 1, GroupName = "ІП-71", SubjectResult = 95 });
            context.Educations.Add(new EducationProcess { IdStudent = 2, IdSubject = 2, GroupName = "ІП-72", SubjectResult = 98 });
            context.Educations.Add(new EducationProcess { IdStudent = 3, IdSubject = 3, GroupName = "ІП-73", SubjectResult = 97 });
            context.Educations.Add(new EducationProcess { IdStudent = 4, IdSubject = 2, GroupName = "ІС-72", SubjectResult = 98 });
            context.Educations.Add(new EducationProcess { IdStudent = 5, IdSubject = 3, GroupName = "ІС-73", SubjectResult = 97 });

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

    }
}
