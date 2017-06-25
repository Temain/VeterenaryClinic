using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinic.Domain.Context;
using VeterinaryClinic.Web.Models;

namespace VeterinaryClinic.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dictionaries()
        {
            return View();
        }

        public ActionResult PetShop()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetScoreByMonth()
        {
            var clinic = db.PetOperations
                .Include(x => x.Operation)
                .Where(x => x.OperationDate <= DateTime.Now)
                .ToList();

            var materialCosts = clinic
                .Select(x => x.Operation.MaterialCosts)
                .Sum();

            var price = clinic
                .Select(x => x.Operation.Price)
                .Sum();

            var results = new List<ScoreByMonthViewModel>();
            var scoreByClinic = new ScoreByMonthViewModel
            {
                Category = "Ветклиника",
                MaterialCosts = materialCosts,
                Price = price
            };
            results.Add(scoreByClinic);

            var scoreByMarket = new ScoreByMonthViewModel
            {
                Category = "Зоомагазин",
                MaterialCosts = 0,
                Price = 0
            };
            results.Add(scoreByMarket);

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}