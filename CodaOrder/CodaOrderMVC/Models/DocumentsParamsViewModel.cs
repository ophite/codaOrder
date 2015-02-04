using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class DocumentsParamsViewModel
    {
        public string subjectID { get; set; }
        public string dateBegin { get; set; }
        public string dateEnd { get; set; }
        public string docTypeClasses { get; set; }
        public string pageNumberCount { get; set; }
        public string pagesCount { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public string totalRows { get; set; }
        public string fullTextFilter { get; set; }
        public string whereText { get; set; }
    }
}