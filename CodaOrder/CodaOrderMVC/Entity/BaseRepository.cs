using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace WebApplication3.Entity
{

    public class BaseRepository<T, D> : IRepository<T, D>
        where T : class
        where D : DbContext
    {
        #region Properties

        protected D dbContext { get; set; }
        protected DbSet<T> dbSet { get; set; }

        #endregion
        #region Methods

        public BaseRepository() { }
        public BaseRepository(D dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");

            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual T GetById(long id)
        {
            return dbSet.Find(id);
        }

        public virtual void Create(T entityObject)
        {
            DbEntityEntry dbEntityEntry = dbContext.Entry(entityObject);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                dbSet.Add(entityObject);

            dbContext.SaveChanges();
        }

        public virtual void Edit(T entityObject)
        {
            dbContext.Entry(entityObject).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public virtual void Delete(long id)
        {
            T entityObject = GetById(id);
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
    }
}