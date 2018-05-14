using GenericRepositoryExam.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepositoryExam.Core.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity> GetByIdAsync(int id);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(int id);
    }
}
