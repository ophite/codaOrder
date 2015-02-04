using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.Entity
{
    public interface IDocumentRepository : IRepository<JournalSale_Documents>
    {
        //string GetLinesJson(string subjectID,
        //    string dateBegin,
        //    string dateEnd,
        //    string docTypeClasses,
        //    int pageSize,
        //    int currentPage,
        //    string fullTextFilter,
        //    string whereText);
        object GetLinesJson(JObject jObject);
        object GetLinesJson(DocumentsParamsViewModel model);
    }
}