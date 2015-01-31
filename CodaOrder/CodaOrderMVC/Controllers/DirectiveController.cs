using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public partial class DirectiveController : Controller
    {
        // GET: Directive
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual PartialViewResult DocumentDatepicker()
        {
            return PartialView();
        }

        public PartialViewResult DatesValidation()
        {
            return PartialView();
        }
    }
}