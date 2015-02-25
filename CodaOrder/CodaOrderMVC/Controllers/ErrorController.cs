using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iOrder.Controllers
{
    public partial class ErrorController : Controller
    {
        // GET: Error
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult NotFound()
        {
            return HttpNotFound("This url not exist!");
        }
    }
}