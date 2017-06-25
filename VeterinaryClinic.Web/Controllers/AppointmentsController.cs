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
    [Authorize]
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            return View();
        }

        // GET: api/Appointments/5
        [HttpGet]
        public ActionResult GetAppointment(int id)
        {
            var appointment = db.Appointments
                .Include(x => x.Pet.PetType)
                .Include(x => x.Pet.Person)
                .Include(x => x.Operations)
                .Include(x => x.Operations.Select(e => e.Operation.OperationType))
                .Include(x => x.Operations.Select(e => e.Employee.Person))
                .SingleOrDefault(x => x.AppointmentId == id && x.DeletedAt == null);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            var viewModel = new AppointmentViewModel
            {
                AppointmentId = appointment.AppointmentId,
                PetId = appointment.Pet.PetId,
                PetName = appointment.Pet.PetName,
                PetTypeId = appointment.Pet.PetTypeId,
                PetTypeName = appointment.Pet.PetType.PetTypeName,
                PersonId = appointment.Pet.PersonId,
                PersonFullName = appointment.Pet.Person.FullName,
                HaveOperations = appointment.Pet.HaveOperations,
                Allergies = appointment.Pet.Allergies,
                ChronicDiseases = appointment.Pet.ChronicDiseases,
                Phone = appointment.Pet.Person.Phone,
                AppointmentDate = appointment.AppointmentDate,
                Age = appointment.Age,
                Anamnesis = appointment.Anamnesis,
                Comment = appointment.Comment,
                Complaints = appointment.Complaints,
                ParasiteTreatmentDate = appointment.ParasiteTreatmentDate,
                Temperature = appointment.Temperature,
                Therapy = appointment.Therapy,
                Weight = appointment.Weight,
                Operations = appointment.Operations != null
                    ? appointment.Operations
                        .Select(x => new PetOperationViewModel
                        {
                            PetOperationId = x.PetOperationId,
                            AppointmentId = x.AppointmentId,
                            EmployeeId = x.EmployeeId,
                            EmployeeFullName = x.Employee.Person.FullName,
                            OperationDate = x.OperationDate,
                            OperationId = x.OperationId,
                            OperationName = x.Operation.OperationName,
                            OperationPrice = x.Operation.Price,
                            OperationMaterialCosts = x.Operation.MaterialCosts,
                            OperationTypeId = x.Operation.OperationTypeId,
                            OperationTypeName = x.Operation.OperationType.OperationTypeName
                        })
                        .ToList()
                    : new List<PetOperationViewModel>()
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNewAppointment(int id)
        {
            var pet = db.Pets
                .Include(x => x.Person)
                .Include(x => x.PetType)
                .SingleOrDefault(x => x.PetId == id && x.DeletedAt == null);
            if (pet == null)
            {
                return new HttpNotFoundResult();
            }

            var viewModel = new AppointmentViewModel
            {
                PetId = pet.PetId,
                PetName = pet.PetName,
                HaveOperations = pet.HaveOperations,
                Allergies = pet.Allergies,
                ChronicDiseases = pet.ChronicDiseases,
                PetTypeId = pet.PetTypeId,
                PetTypeName = pet.PetType.PetTypeName,
                PersonId = pet.PersonId,
                PersonFullName = pet.Person.FullName,
                Phone = pet.Person.Phone,
                AppointmentDate = DateTime.Now
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Appointments/Create
        public ActionResult Create(int id)
        {
            ViewBag.PetId = id;

            return View();
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int id)
        {
            var appointment = db.Appointments.SingleOrDefault(x => x.AppointmentId == id && x.DeletedAt == null);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            ViewBag.AppointmentId = appointment.AppointmentId;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Save(AppointmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var appointment = db.Appointments.SingleOrDefault(x => x.AppointmentId == viewModel.AppointmentId && x.DeletedAt == null);
            try
            {
                if (appointment == null)
                {
                    appointment = new Appointment
                    {
                        AppointmentDate = viewModel.AppointmentDate,
                        CreatedAt = DateTime.Now,
                        Complaints = viewModel.Complaints,
                        Comment = viewModel.Comment,
                        Age = viewModel.Age,
                        PetId = viewModel.PetId,
                        Temperature = viewModel.Temperature,
                        Weight = viewModel.Weight,
                        ParasiteTreatmentDate = viewModel.ParasiteTreatmentDate,
                        Therapy = viewModel.Therapy,
                        Anamnesis = viewModel.Anamnesis,
                        Operations = viewModel.Operations
                            .Select(x => new PetOperation
                            {
                                OperationId = x.OperationId,
                                OperationDate = x.OperationDate,
                                EmployeeId = x.EmployeeId
                            })
                            .ToList()
                    };

                    db.Appointments.Add(appointment);
                }
                else
                {
                    appointment.UpdatedAt = DateTime.Now;
                    appointment.AppointmentDate = viewModel.AppointmentDate;
                    appointment.Complaints = viewModel.Complaints;
                    appointment.Comment = viewModel.Comment;
                    appointment.Age = viewModel.Age;
                    appointment.Temperature = viewModel.Temperature;
                    appointment.Weight = viewModel.Weight;
                    appointment.ParasiteTreatmentDate = viewModel.ParasiteTreatmentDate;
                    appointment.Therapy = viewModel.Therapy;
                    appointment.Anamnesis = viewModel.Anamnesis;

                    if (appointment.Operations == null)
                    {
                        appointment.Operations = new List<PetOperation>();
                    }

                    foreach (var petOperationViewModel in viewModel.Operations)
                    {
                        var petOperation = appointment.Operations.SingleOrDefault(x => x.PetOperationId == petOperationViewModel.PetOperationId && x.PetOperationId != 0 && x.DeletedAt == null);
                        if (petOperation == null)
                        {
                            petOperation = new PetOperation
                            {
                                OperationId = petOperationViewModel.OperationId,
                                EmployeeId = petOperationViewModel.EmployeeId,
                                OperationDate = petOperationViewModel.OperationDate,
                                CreatedAt = DateTime.Now
                            };

                            appointment.Operations.Add(petOperation);
                        }
                        else
                        {
                            petOperation.OperationId = petOperationViewModel.OperationId;
                            petOperation.EmployeeId = petOperationViewModel.EmployeeId;
                            petOperation.OperationDate = petOperationViewModel.OperationDate;
                            petOperation.UpdatedAt = DateTime.Now;
                        }
                    }

                    foreach (var petOperation in appointment.Operations)
                    {
                        if (petOperation.PetOperationId != 0 && petOperation.DeletedAt == null)
                        {
                            var petOperationViewModel = viewModel.Operations.SingleOrDefault(x => x.PetOperationId == petOperation.PetOperationId);
                            if (petOperationViewModel == null)
                            {
                                petOperation.DeletedAt = DateTime.Now;
                            }
                        }
                    }
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
        public ActionResult GetDictionaries()
        {
            var operations = db.Operations
                .Select(x => new
                {
                    OperationId = x.OperationId,
                    OperationName = x.OperationName,
                    OperationTypeId = x.OperationTypeId,
                    Price = x.Price,
                    MaterialCosts = x.MaterialCosts
                })
                .ToList();

            return Json(new { Operations = operations }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEmployees(int? operationId)
        {
            var employees = db.Employees
                .Include(x => x.Position.Operations)
                .Include(x => x.Person)
                .Where(x => x.Position.Operations.Any(p => p.OperationId == operationId))
                .ToList()
                .Select(x => new
                {
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Person.FullName
                })
                .ToList();

            return Json(new { Employees = employees }, JsonRequestBehavior.AllowGet);
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
