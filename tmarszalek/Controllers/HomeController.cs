﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace tmarszalek.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Text"] = "Kontrola dostępu - panel główny";

            return View();
        }
        protected void permissionspage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Users/Index.cshtml");
            Server.Transfer("Users/Index.cshtml");
        }
    }
}
