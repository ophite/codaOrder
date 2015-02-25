using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace iOrder.Entity
{
    public interface IRepository<TEntity>// : IDisposable
        where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        IQueryable<TEntity> GetAll();
        TEntity GetById(long id);
        void Create(TEntity entityObj);
        void Edit(TEntity entityObj);
        void Delete(long id);
    }
}