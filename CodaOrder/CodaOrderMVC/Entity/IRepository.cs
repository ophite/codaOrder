using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WebApplication3.Entity
{
    public interface IRepository<T, D>
        where T : class
        where D : DbContext
    {
        void Create(T entityObj);
        void Edit(T entityObj);
        void Delete(long id);
        IQueryable<T> GetAll();
        T GetById(long id);
    }
}