using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Helpers
{
    public class ConstantDocument
    {
        // helpers
        public static string IsResponseError = "isResponseError";
        public static string ResponseErrorMessage = "responseErrorMessage";
        public static string GridData = "gridData";
        public static string IsEditable = "isEditable";
        public static string SpinnerID = "spinner-1";

        public static string ParamSubjectID = "subjectID";
        public static string ParamDateBegin = "dateBegin";
        public static string ParamDateEnd = "dateEnd";
        public static string ParamDocTypeClasses = "docTypeClasses";
        // paging
        public static string ParamPageNumberCount = "pageNumberCount";
        public static string ParamPagesCount = "pagesCount";
        public static string ParamPageSize = "pageSize";
        public static string ParamCurrentPage = "currentPage";
        public static string ParamTotalRows = "totalRows";
        // filtering
        public static string ParamFullTextFilter = "fullTextFilter";
        public static string ParamWhereText = "whereText";

        public static string[] GetParams()
        {
            return new string[] {
                    ParamSubjectID,
                    ParamDateBegin,
                    ParamDateEnd,
                    ParamDocTypeClasses,
                    // paging
                    ParamPageNumberCount,
                    ParamPagesCount,
                    ParamPageSize,
                    ParamCurrentPage,
                    ParamTotalRows,
                    // filtering
                    ParamFullTextFilter,
                    ParamWhereText
                };
        }

        // message
        public static string ErrorLogin = "User name or password is invalid";
        public static string ErrorChangePassword = "Check your old password and user";
    }
}