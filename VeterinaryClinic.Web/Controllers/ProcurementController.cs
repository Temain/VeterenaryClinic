using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinic.Domain.Context;
using VeterinaryClinic.Web.Models;
using System.Threading.Tasks;
using System.Net;
using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Web.Controllers
{
    public class ProcurementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProcurements()
        {
            var products = db.Procurements
                .Include(x => x.Product)
                .Select(x => new ProcurementViewModel
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
                    ProcurementAmount = x.ProcurementAmount,
                    ProcurementId = x.ProcurementId,
                    ProcurementDate = x.ProcurementDate,
                    ProcurementPrice = x.ProcurementAmount * x.Product.PurchasePrice
                })
                .ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProcurement(int id)
        {
            var procurement = db.Procurements
                .Include(x => x.Product)
                .SingleOrDefault(x => x.ProcurementId == id);
            if (procurement == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ProcurementViewModel
            {
                ProductId = procurement.ProductId,
                ProductName = procurement.Product.ProductName,
                ProcurementAmount = procurement.ProcurementAmount,
                ProcurementDate = procurement.ProcurementDate,
                ProcurementId = procurement.ProcurementId,
                ProcurementPrice = procurement.ProcurementAmount * procurement.Product.PurchasePrice
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var product = db.Procurements.SingleOrDefault(x => x.ProcurementId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProcurementId = id;

            return View();
        }

        public async Task<ActionResult> Save(ProcurementViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var procurement = db.Procurements
                .Include(x => x.Product)
                .SingleOrDefault(x => x.ProcurementId == viewModel.ProcurementId);
            try
            {
                if (procurement == null)
                {
                    procurement = new Procurement
                    {
                        ProductId = viewModel.ProductId,
                        ProcurementAmount = viewModel.ProcurementAmount,
                        ProcurementDate = viewModel.ProcurementDate
                    };

                    db.Procurements.Add(procurement);
                }
                else
                {
                    procurement.ProductId = viewModel.ProductId;
                    procurement.ProcurementAmount = viewModel.ProcurementAmount;
                    procurement.ProcurementDate = viewModel.ProcurementDate;
                }

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, string.Format("ErrorMessage: {0}, StackTrace: {1}", ex.Message, ex.StackTrace));
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public async Task<ActionResult> GetDictionaries()
        {
            var products = await db.Products
                .Select(x => new ProductViewModel
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    PurchasePrice = x.PurchasePrice,
                    SellingPrice = x.SellingPrice
                })
                .ToListAsync();

            return Json(new { Products = products }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}