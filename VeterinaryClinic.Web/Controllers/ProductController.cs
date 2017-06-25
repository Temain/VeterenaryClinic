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
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            var products = db.Products
                .Select(x => new ProductViewModel
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    PurchasePrice = x.PurchasePrice,
                    SellingPrice = x.SellingPrice
                })
                .ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProduct(int id)
        {
            var product = db.Products
                .SingleOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                PurchasePrice = product.PurchasePrice,
                SellingPrice = product.SellingPrice
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var product = db.Products.SingleOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductId = id;

            return View();
        }

        public async Task<ActionResult> Save(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products
                .SingleOrDefault(x => x.ProductId == viewModel.ProductId);
            try
            {
                if (product == null)
                {
                    product = new Product
                    {
                        ProductName = viewModel.ProductName,
                        PurchasePrice = viewModel.PurchasePrice,
                        SellingPrice = viewModel.SellingPrice
                    };

                    db.Products.Add(product);
                }
                else
                {
                    product.ProductName = viewModel.ProductName;
                    product.PurchasePrice = viewModel.PurchasePrice;
                    product.SellingPrice = viewModel.SellingPrice;
                }

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, string.Format("ErrorMessage: {0}, StackTrace: {1}", ex.Message, ex.StackTrace));
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
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