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

        // Filtering
        public ActionResult FilterHeaderTemplateH()
        {
            return View();
        }

        // Pagination
        public ActionResult Pagination()
        {
            return View();
        }

        public ActionResult Pager()
        {
            return View();
        }

        // Datepicker
        public ActionResult Datepicker()
        {
            return View("~/views/template/datepicker/Datepicker.cshtml");
        }

        public ActionResult Popup()
        {
            return View("~/views/template/datepicker/Popup.cshtml");
        }

        public ActionResult Day()
        {
            return View("~/views/template/datepicker/Day.cshtml");
        }

        public ActionResult Month()
        {
            return View("~/views/template/datepicker/Month.cshtml");
        }

        public ActionResult Year()
        {
            return View("~/views/template/datepicker/Year.cshtml");
        }
    }
}