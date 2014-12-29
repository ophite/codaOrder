using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FilterHeaderTemplate()
        {
            return View();
        }


        public ActionResult FilterHeaderTemplateH()
        {
            return View();
        }

        public ActionResult Pagination()
        {
            return View();
        }

        public ActionResult Pager()
        {
            return View();
        }
    }
}