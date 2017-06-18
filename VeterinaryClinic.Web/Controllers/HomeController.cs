using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VeterinaryClinic.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Persons()
        {
            return View();
        }

        public ActionResult CreatePerson()
        {
            return View();
        }

        public ActionResult EditPerson()
        {
            return View();
        }
    }
}