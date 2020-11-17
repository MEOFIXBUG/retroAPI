using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace retroAPI.Commons.Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        void Delete(TEntity entityToDelete);
        void SoftDelete(object id);
        void Delete(object id);

        IEnumerable<TEntity> Get(

            Expression<Func<TEntity, bool>> filter = null,

            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,

            string includeProperties = "");

        TEntity GetByID(object id);

        IEnumerable<TEntity> GetWithRawSql(string query,

            params object[] parameters);

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);
        void Save();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIDAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync(

            Expression<Func<TEntity, bool>> filter = null,

            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,

            string includeProperties = "");
    }
}
