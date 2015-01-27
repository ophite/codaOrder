using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Entity;

namespace WebApplication3.Controllers
{
    public partial class SearchCodaObjectController : Controller
    {
        private IUow _uow;

        public SearchCodaObjectController(IUow uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        public virtual JsonResult Subject(string searchText)
        {
            return Json(_uow.CodaJsonRepository.FindSubject(searchText), JsonRequestBehavior.AllowGet);
        }

        #region IDispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _uow.Dispose();

            base.Dispose(disposing);
        }

        #endregion
    }
}