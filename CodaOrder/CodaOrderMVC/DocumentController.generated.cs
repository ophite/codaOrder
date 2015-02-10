// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
#pragma warning disable 1591, 3008, 3009
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace WebApplication3.Controllers
{
    public partial class DocumentController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected DocumentController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetDocumentsPost()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetDocumentsPost);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetLines()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetLines);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult SaveLines()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SaveLines);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Details()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Details);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Delete()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DeleteConfirmed()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteConfirmed);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DocumentController Actions { get { return MVC.Document; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Document";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Document";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Documents = "Documents";
            public readonly string Lines = "Lines";
            public readonly string GetDocumentsPost = "GetDocumentsPost";
            public readonly string GetLines = "GetLines";
            public readonly string SaveLines = "SaveLines";
            public readonly string NewOrder = "NewOrder";
            public readonly string OrdersDraft = "OrdersDraft";
            public readonly string OrdersHistory = "OrdersHistory";
            public readonly string Details = "Details";
            public readonly string Create = "Create";
            public readonly string Edit = "Edit";
            public readonly string Delete = "Delete";
            public readonly string DeleteConfirmed = "Delete";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Documents = "Documents";
            public const string Lines = "Lines";
            public const string GetDocumentsPost = "GetDocumentsPost";
            public const string GetLines = "GetLines";
            public const string SaveLines = "SaveLines";
            public const string NewOrder = "NewOrder";
            public const string OrdersDraft = "OrdersDraft";
            public const string OrdersHistory = "OrdersHistory";
            public const string Details = "Details";
            public const string Create = "Create";
            public const string Edit = "Edit";
            public const string Delete = "Delete";
            public const string DeleteConfirmed = "Delete";
        }


        static readonly ActionParamsClass_GetDocumentsPost s_params_GetDocumentsPost = new ActionParamsClass_GetDocumentsPost();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetDocumentsPost GetDocumentsPostParams { get { return s_params_GetDocumentsPost; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetDocumentsPost
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_GetLines s_params_GetLines = new ActionParamsClass_GetLines();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetLines GetLinesParams { get { return s_params_GetLines; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetLines
        {
            public readonly string documentID = "documentID";
        }
        static readonly ActionParamsClass_SaveLines s_params_SaveLines = new ActionParamsClass_SaveLines();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveLines SaveLinesParams { get { return s_params_SaveLines; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveLines
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_NewOrder s_params_NewOrder = new ActionParamsClass_NewOrder();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NewOrder NewOrderParams { get { return s_params_NewOrder; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NewOrder
        {
            public readonly string form = "form";
        }
        static readonly ActionParamsClass_Details s_params_Details = new ActionParamsClass_Details();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Details DetailsParams { get { return s_params_Details; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Details
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Create s_params_Create = new ActionParamsClass_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Create CreateParams { get { return s_params_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Create
        {
            public readonly string docs = "docs";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
            public readonly string docs = "docs";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_DeleteConfirmed s_params_DeleteConfirmed = new ActionParamsClass_DeleteConfirmed();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteConfirmed DeleteConfirmedParams { get { return s_params_DeleteConfirmed; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteConfirmed
        {
            public readonly string id = "id";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Create = "Create";
                public readonly string Delete = "Delete";
                public readonly string Details = "Details";
                public readonly string Documents = "Documents";
                public readonly string Edit = "Edit";
                public readonly string Index = "Index";
                public readonly string Lines = "Lines";
                public readonly string NewOrder = "NewOrder";
                public readonly string OrdersDraft = "OrdersDraft";
                public readonly string OrdersHistory = "OrdersHistory";
            }
            public readonly string Create = "~/Views/Document/Create.cshtml";
            public readonly string Delete = "~/Views/Document/Delete.cshtml";
            public readonly string Details = "~/Views/Document/Details.cshtml";
            public readonly string Documents = "~/Views/Document/Documents.cshtml";
            public readonly string Edit = "~/Views/Document/Edit.cshtml";
            public readonly string Index = "~/Views/Document/Index.cshtml";
            public readonly string Lines = "~/Views/Document/Lines.cshtml";
            public readonly string NewOrder = "~/Views/Document/NewOrder.cshtml";
            public readonly string OrdersDraft = "~/Views/Document/OrdersDraft.cshtml";
            public readonly string OrdersHistory = "~/Views/Document/OrdersHistory.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_DocumentController : WebApplication3.Controllers.DocumentController
    {
        public T4MVC_DocumentController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DocumentsOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult Documents()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Documents);
            DocumentsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void LinesOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult Lines()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Lines);
            LinesOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetDocumentsPostOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, WebApplication3.Models.DocumentsParamsViewModel model);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetDocumentsPost(WebApplication3.Models.DocumentsParamsViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetDocumentsPost);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            GetDocumentsPostOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void GetLinesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string documentID);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetLines(string documentID)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetLines);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "documentID", documentID);
            GetLinesOverride(callInfo, documentID);
            return callInfo;
        }

        [NonAction]
        partial void SaveLinesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, WebApplication3.Models.LinesViewModel model);

        [NonAction]
        public override System.Web.Mvc.JsonResult SaveLines(WebApplication3.Models.LinesViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SaveLines);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SaveLinesOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void NewOrderOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult NewOrder()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.NewOrder);
            NewOrderOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void OrdersDraftOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult OrdersDraft()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.OrdersDraft);
            OrdersDraftOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void OrdersHistoryOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult OrdersHistory()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.OrdersHistory);
            OrdersHistoryOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void NewOrderOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Web.Mvc.FormCollection form);

        [NonAction]
        public override System.Web.Mvc.ActionResult NewOrder(System.Web.Mvc.FormCollection form)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NewOrder);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "form", form);
            NewOrderOverride(callInfo, form);
            return callInfo;
        }

        [NonAction]
        partial void DetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Details(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Details);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DetailsOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Create()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            CreateOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, WebApplication3.Entity.JournalSale_Documents docs);

        [NonAction]
        public override System.Web.Mvc.ActionResult Create(WebApplication3.Entity.JournalSale_Documents docs)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "docs", docs);
            CreateOverride(callInfo, docs);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, WebApplication3.Entity.JournalSale_Documents docs);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(WebApplication3.Entity.JournalSale_Documents docs)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "docs", docs);
            EditOverride(callInfo, docs);
            return callInfo;
        }

        [NonAction]
        partial void DeleteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Delete(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void DeleteConfirmedOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id);

        [NonAction]
        public override System.Web.Mvc.ActionResult DeleteConfirmed(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteConfirmed);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteConfirmedOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
