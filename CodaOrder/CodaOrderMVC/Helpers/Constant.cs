using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Helpers
{
    public class ConstantDocument
    {
        public static string ParamSubjectID = "subjectID";
        public static string ParamDateBegin = "dateBegin";
        public static string ParamDateEnd = "dateEnd";
        public static string ParamDocTypeClasses = "docTypeClasses";
        public static string ParamPageNumberCount = "pageNumberCount";
        public static string ParamPagesCount = "pagesCount";
        public static string ParamPageSize = "pageSize";
        public static string ParamTotalRows = "totalRows";
        public static string ParamCurrentPage = "currentPage";
        public static string ParamFullTextFilter = "fullTextFilter";
        public static string ParamWhereText = "whereText";

        public static string[] GetParams()
        {
            return new string[] {
                    ParamSubjectID,
                    ParamDateBegin,
                    ParamDateEnd,
                    ParamDocTypeClasses,
                    ParamPageNumberCount,
                    ParamPagesCount,
                    ParamPageSize,
                    ParamCurrentPage,
                    ParamTotalRows,
                    ParamFullTextFilter,
                    ParamWhereText
                };
        }
    }
}