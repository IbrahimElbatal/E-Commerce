using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace UsingRepository.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IQueryable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);


        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);

        DbEntityEntry<TEntity> Entry(TEntity entity);
    }
}