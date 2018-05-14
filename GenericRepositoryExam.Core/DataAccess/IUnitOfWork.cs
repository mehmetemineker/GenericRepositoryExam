using GenericRepositoryExam.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryExam.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IEntityRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IEntity, new();
        Task<int> SaveChangesAsync();
    }
}
