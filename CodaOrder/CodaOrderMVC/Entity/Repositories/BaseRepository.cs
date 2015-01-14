using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebApplication3.Entity
{
    public class BaseRepository<TEntity> : IRepository<TEntity>//, IDisposable
        where TEntity : class
    {
        #region Properties

        protected DbContext dbContext { get; set; }
        protected DbSet<TEntity> dbSet { get; set; }

        #endregion
        #region Methods

        public BaseRepository()
        {
        }

        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");

            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        #endregion
        #region IRepository

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public virtual TEntity GetById(long id)
        {
            return dbSet.Find(id);
        }

        public virtual void Create(TEntity entityObject)
        {
            DbEntityEntry dbEntityEntry = dbContext.Entry(entityObject);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                dbSet.Add(entityObject);

            dbContext.SaveChanges();
        }

        public virtual void Edit(TEntity entityObject)
        {
            dbContext.Entry(entityObject).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public virtual void Delete(long id)
        {
            TEntity entityObject = GetById(id);
            DbEntityEntry entityObjectEntry = dbContext.Entry(entityObject);

            if (entityObjectEntry.State != EntityState.Deleted)
                entityObjectEntry.State = EntityState.Deleted;
            else
            {
                dbSet.Attach(entityObject);
                dbSet.Remove(entityObject);
            }

            dbContext.SaveChanges();
        }

        #endregion
        #region IDisposable

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            if (this.dbContext != null)
        //                dbContext.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        #endregion
    }
}