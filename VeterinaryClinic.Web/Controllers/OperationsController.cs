using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinic.Domain.Context;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Web.Models;

namespace VeterinaryClinic.Web.Controllers
{
    public class OperationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Operations
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetOperation(int id)
        {
            var operation = db.Operations
                .Include(x => x.OperationType)
                .Include(x => x.Positions.Select(p => p.Position))
                .SingleOrDefault(x => x.OperationId == id);
            if (operation == null)
            {
                return HttpNotFound();
            }

            var viewModel = new OperationViewModel
            {
                OperationId = operation.OperationId,
                OperationName = operation.OperationName,
                OperationTypeId = operation.OperationTypeId,
                OperationTypeName = operation.OperationType.OperationTypeName,
                Positions = operation.Positions
                    .Select(p => new PositionViewModel
                    {
                        OperationPositionId = p.OperationPositionId,
                        OperationId = p.OperationId,
                        PositionId = p.PositionId
                    })
                    .ToList()
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOperations()
        {
            var operations = db.Operations
                .Include(x => x.OperationType)
                .Include(x => x.Positions.Select(p => p.Position))
                .Select(x => new OperationViewModel
                {
                    OperationId = x.OperationId,
                    OperationName = x.OperationName,
                    OperationTypeId = x.OperationTypeId,
                    OperationTypeName = x.OperationType.OperationTypeName,
                    Positions = x.Positions
                        .Select(p => new PositionViewModel
                        {
                            OperationPositionId = p.OperationPositionId,
                            PositionId = p.PositionId,
                            PositionName = p.Position.PositionName
                        })
                        .ToList()

                })
                .ToList();

            return Json(operations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var operation = db.Operations.SingleOrDefault(x => x.OperationId == id);
            if (operation == null)
            {
                return HttpNotFound();
            }

            ViewBag.OperationId = id;

            return View();
        }

        public async Task<ActionResult> Save(OperationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var operation = db.Operations
                .Include(x => x.Positions)
                .SingleOrDefault(x => x.OperationId == viewModel.OperationId);
            try
            {
                if (operation == null)
                {
                    operation = new Operation
                    {
                        OperationName = viewModel.OperationName,
                        OperationTypeId = viewModel.OperationTypeId,
                        Positions = viewModel.Positions
                            .Select(x => new OperationPosition
                            {
                                PositionId = x.PositionId,
                            })
                            .ToList()
                    };

                    db.Operations.Add(operation);
                }
                else
                {
                    operation.OperationName = viewModel.OperationName;
                    operation.OperationTypeId = viewModel.OperationTypeId;

                    foreach (var positionViewModel in viewModel.Positions)
                    {
                        var position = operation.Positions.SingleOrDefault(x => x.OperationPositionId == positionViewModel.OperationPositionId && positionViewModel.OperationPositionId != 0);
                        if (position == null)
                        {
                            position = new OperationPosition
                            {
                                PositionId = positionViewModel.PositionId,
                                OperationId = positionViewModel.OperationId
                            };

                            operation.Positions.Add(position);
                        }
                        else
                        {
                            position.PositionId = positionViewModel.PositionId;
                        }
                    }

                    var positionsForRemove = new List<OperationPosition>();
                    foreach (var position in operation.Positions)
                    {
                        if (position.OperationPositionId != 0)
                        {
                            var positionViewModel = viewModel.Positions.SingleOrDefault(x => x.OperationPositionId == position.OperationPositionId);
                            if (positionViewModel == null)
                            {
                                positionsForRemove.Add(position);
                                // db.OperationPositions.Remove(position);
                            }
                        }
                    }

                    db.OperationPositions.RemoveRange(positionsForRemove);
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
            var operationTypes = await db.OperationTypes
                .Select(x => new 
                {
                    OperationTypeId = x.OperationTypeId,
                    OperationTypeName = x.OperationTypeName
                })
                .ToListAsync();

            var positions = await db.Positions
                .Select(x => new
                {
                    x.PositionId,
                    x.PositionName
                })
                .ToListAsync();

            return Json(new { OperationTypes = operationTypes, Positions = positions }, JsonRequestBehavior.AllowGet);
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
