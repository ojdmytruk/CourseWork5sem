﻿using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Data.Entity;
//using System.Data;

namespace DAL.Repositories
{
    class GroupRepository : IRepository<Group>
    {
        private EducationProcessContext context;
        DbSet<Group> groupSet;

        public GroupRepository(EducationProcessContext context)
        {
            this.context = context;
            groupSet = context.Set<Group>();
        }

        public IEnumerable<Group> Get()
        {
            return groupSet
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Group> Get(Func<Group, bool> predicate)
        {
            return groupSet
                .AsNoTracking()
                .Where(predicate)
                .ToList();
        }
        public Group FindById(int id)
        {
            return groupSet
                .AsNoTracking()
                .Where(p => p.Id == id)
                .AsNoTracking()
                .FirstOrDefault();
        }
        public void Create(Group item)
        {
            groupSet.Add(item);
            context.SaveChanges();
        }
        public void Update(Group item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Remove(Group item)
        {
            groupSet.Remove(groupSet.Where(s => s.Id == item.Id).FirstOrDefault());
            context.SaveChanges();
        }
    }
}
