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
    public class EducationProcessRepository : IRepository<EducationProcess>
    {
        EducationProcessContext context;
        DbSet<EducationProcess> eduSet;

        public EducationProcessRepository(EducationProcessContext context)
        {
            this.context = context;
            eduSet = context.Set<EducationProcess>();
        }

        public IEnumerable<EducationProcess> Get()
        {
            return eduSet
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<EducationProcess> Get(Func<EducationProcess, bool> predicate)
        {
            return eduSet
                .AsNoTracking()
                .Where(predicate)
                .ToList();
        }
        public EducationProcess FindById(int id)
        {
            return eduSet
                .AsNoTracking()
                .Include(p => p.Student)
                .Include(p => p.Subject)
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public void Create(EducationProcess item)
        {
            eduSet.Add(item);
            context.SaveChanges();
        }

        public void Update(EducationProcess item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(EducationProcess item)
        {
            eduSet.Remove(eduSet.Where(e => e.Id == item.Id).FirstOrDefault());
            context.SaveChanges();
        }
    }
}
