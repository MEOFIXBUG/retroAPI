using Microsoft.EntityFrameworkCore;
using retroAPI.Commons.Repository.Interface;
using retroAPI.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace retroAPI.Commons.Repository
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        internal DbSet<TEntity> dbSet;

        //public EntityRepository()
        //{
        //    this.context = new heroku_4f2def07091704cContext();
        //    dbSet = context.Set<TEntity>();
        //}
        //public EntityRepository(heroku_4f2def07091704cContext context)
        //{
        //    this.context = context;
        //    this.dbSet = context.Set<TEntity>();
        //}

        public IEnumerable<TEntity> GetAll()
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                return dbSet.ToList();
            }
        }
        public virtual IEnumerable<TEntity> GetWithRawSql(string query,

            params object[] parameters)
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                return dbSet.FromSqlRaw(query, parameters).ToList();
            }
        }
        public void SoftDelete(object id)
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                TEntity entityToUpdate = dbSet.Find(id);
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).CurrentValues["IsDeleted"] = 1;
                context.Entry(entityToUpdate).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public IEnumerable<TEntity> Get(

            Expression<Func<TEntity, bool>> filter = null,

            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,

            string includeProperties = "")

        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties.Split

                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }

        }

        public TEntity GetByID(object id)
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                return dbSet.Find(id);
            }
        }

        public void Insert(TEntity entity)
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                dbSet.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(object id)
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                TEntity entityToDelete = dbSet.Find(id);
                Delete(entityToDelete);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entityToDelete)
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                if (context.Entry(entityToDelete).State == EntityState.Detached)

                {
                    dbSet.Attach(entityToDelete);

                }
                dbSet.Remove(entityToDelete);
                context.SaveChanges();
            }

        }


        public void Update(TEntity entityToUpdate)
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                dbSet.Attach(entityToUpdate);

                context.Entry(entityToUpdate).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void Save()
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                context.SaveChanges();
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                return await dbSet.ToListAsync();
            }

        }
        public async Task<TEntity> GetByIDAsync(int id)
        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                return await dbSet.FindAsync(id);
            }

        }

        public async Task<IEnumerable<TEntity>> GetAsync(

            Expression<Func<TEntity, bool>> filter = null,

            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,

            string includeProperties = "")

        {
            using (var context = new heroku_4f2def07091704cContext())
            {
                dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties.Split

                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }

        }


        ~EntityRepository()
        {

        }
    }
}
