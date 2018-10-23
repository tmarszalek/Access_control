using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tmarszalek.Controllers
{
    public class RoomsController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Text"] = "Pomieszczenia";
            return View ();
        }
    }
}
