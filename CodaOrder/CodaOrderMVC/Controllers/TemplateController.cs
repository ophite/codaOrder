﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public partial class TemplateController : Controller
    {
        // GET: Template
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult FilterHeaderTemplate()
        {
            return View();
        }
    }
}