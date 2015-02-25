using iOrder.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace iOrder.Entity
{
    public interface ICodaJsonRepository : IRepository<CodaJson>
    {
        object FindSubject(string searchText);
    }
}