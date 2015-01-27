using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Entity
{
    public interface ICodaJsonRepository : IRepository<CodaJson>
    {
        object FindSubject(string searchText);
    }
}