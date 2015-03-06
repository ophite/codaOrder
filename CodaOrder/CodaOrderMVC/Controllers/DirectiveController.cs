using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iOrder.Controllers
{
    public partial class DirectiveController : Controller
    {
        // GET: Directive
        public virtual ActionResult Index()
        {
            return View();
        }
        
        public virtual PartialViewResult DirDatesValidation()
        {
            return PartialView();
        }

        public virtual PartialViewResult DirShowError()
        {
            return PartialView();
        }
    }
}