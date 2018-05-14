using GenericRepositoryExam.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryExam.Core.DataAccess.EntityFramework
{
    public class EfEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> dbSet;

        public EfEntityRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate).AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ?
                await dbSet.AsNoTracking().ToListAsync() :
                await dbSet.AsNoTracking().Where(filter).ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbSet.AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (entity.GetType().GetProperty("IsDelete") != null)
            {
                TEntity _entity = entity;

                entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                Update(_entity);
            }
            else
            {
                if (context.Entry(entity).State != EntityState.Deleted)
                {
                    context.Entry(entity).State = EntityState.Deleted;
                }
                else
                {
                    dbSet.Attach(entity);
                    dbSet.Remove(entity);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                return;
            }
            else
            {
                if (entity.GetType().GetProperty("IsDelete") != null)
                {
                    TEntity _entity = entity;
                    _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                    Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
            }
        }
    }
}
