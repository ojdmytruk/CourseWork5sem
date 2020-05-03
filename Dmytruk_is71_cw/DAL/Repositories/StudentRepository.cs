using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Data.Entity;
//using System.Data;

namespace DAL.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private EducationProcessContext context;
        DbSet<Student> studentSet;

        public StudentRepository(EducationProcessContext context)
        {
            this.context = context;
            studentSet = context.Set<Student>();
        }

        public IEnumerable<Student> Get()
        {
            return studentSet
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Student> Get(Func<Student, bool> predicate)
        {
            return studentSet
                .AsNoTracking()
                .Where(predicate)
                .ToList();
        }
        public Student FindById(int id)
        {
            return studentSet
                .AsNoTracking()
                .Include(p => p.Group)
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public void Create(Student item)
        {
            studentSet.Add(item);
            context.SaveChanges();
        }
        public void Update(Student item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Remove(Student item)
        {
            studentSet.Remove(studentSet.Where(s => s.Id == item.Id).FirstOrDefault());
            context.SaveChanges();
        }
    }
}
