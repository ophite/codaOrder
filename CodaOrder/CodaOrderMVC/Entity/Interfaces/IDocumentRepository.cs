using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Entity
{
    public interface IDocumentRepository: IRepository<JournalSale_Documents>
    {
        string GetLinesJson();
    }
}