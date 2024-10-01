using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAcces.EntityFremework
{
    public class EfEntityRepositoryBase<TEntitiy, TContext> : IEntityRepository<TEntitiy>
         where TEntitiy : class, IEntitiy, new()
       where TContext : DbContext, new()
    {
        public void Add(TEntitiy entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntitiy entity)
        {
            using (TContext context = new TContext())
            {
                var deledetEntity = context.Entry(entity);
                deledetEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntitiy Get(Expression<Func<TEntitiy, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntitiy>().SingleOrDefault(filter);
            }
        }

        public List<TEntitiy> GetAll(Expression<Func<TEntitiy, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntitiy>().ToList() : context.Set<TEntitiy>().Where(filter).ToList();
            }
        }

        public void Update(TEntitiy entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
