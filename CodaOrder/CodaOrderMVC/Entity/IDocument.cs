using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Entity
{
    public interface IDocuments<T, D> : IRepository<T, D>
        where T : class
        where D : DbContext
    {
        string GetLinesJson();
    }
}