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
    public class SaleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSales()
        {
            var products = db.Sales
                .Include(x => x.Product)
                .OrderByDescending(x => x.SaleDate)
                .Select(x => new SaleViewModel
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
                    SaleCount = x.SaleCount,
                    SaleId = x.SaleId,
                    SaleDate = x.SaleDate,
                    SalePrice = x.SaleCount * x.Product.PurchasePrice
                })
                .ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSale(int id)
        {
            var procurement = db.Sales
                .Include(x => x.Product)
                .SingleOrDefault(x => x.SaleId == id);
            if (procurement == null)
            {
                return HttpNotFound();
            }

            var viewModel = new SaleViewModel
            {
                ProductId = procurement.ProductId,
                SaleCount = procurement.SaleCount,
                SaleDate = procurement.SaleDate,
                SaleId = procurement.SaleId,
                SalePrice = procurement.SaleCount * procurement.Product.PurchasePrice
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var product = db.Sales.SingleOrDefault(x => x.SaleId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.SaleId = id;

            return View();
        }

        public async Task<ActionResult> Save(SaleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var procurement = db.Sales
                .Include(x => x.Product)
                .SingleOrDefault(x => x.SaleId == viewModel.SaleId);
            try
            {
                if (procurement == null)
                {
                    procurement = new Sale
                    {
                        ProductId = viewModel.ProductId,
                        SaleCount = viewModel.SaleCount,
                        SaleDate = viewModel.SaleDate
                    };

                    db.Sales.Add(procurement);
                }
                else
                {
                    procurement.ProductId = viewModel.ProductId;
                    procurement.SaleCount = viewModel.SaleCount;
                    procurement.SaleDate = viewModel.SaleDate;
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