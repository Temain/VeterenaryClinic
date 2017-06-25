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
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEmployees()
        {
            var employees = db.Employees
                .Include(x => x.Person)
                .Include(x => x.Position)
                .Select(x => new EmployeeViewModel
                {
                    EmployeeId = x.EmployeeId,
                    PersonId = x.PersonId,
                    LastName = x.Person.LastName,
                    FirstName = x.Person.FirstName,
                    MiddleName = x.Person.MiddleName,
                    Birthday = x.Person.Birthday,
                    Address = x.Person.Address,
                    Phone = x.Person.Phone,
                    SexId = x.Person.SexId,
                    HireDate = x.HireDate,
                    PositionId = x.PositionId,
                    PositionName = x.Position.PositionName
                })
                .ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        // GET: api/Person/5
        [HttpGet]
        public ActionResult GetEmployee(int id)
        {
            var employee = db.Employees
                .Include(x => x.Person)
                .Include(x => x.Position)
                .SingleOrDefault(x => x.EmployeeId == id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                PersonId = employee.PersonId,
                LastName = employee.Person.LastName,
                FirstName = employee.Person.FirstName,
                MiddleName = employee.Person.MiddleName,
                Birthday = employee.Person.Birthday,
                Address = employee.Person.Address,
                Phone = employee.Person.Phone,
                SexId = employee.Person.SexId,
                HireDate = employee.HireDate,
                PositionId = employee.PositionId,
                PositionName = employee.Position.PositionName
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            var person = db.Employees.SingleOrDefault(x => x.EmployeeId == id);
            if (person == null)
            {
                return HttpNotFound();
            }

            ViewBag.EmployeeId = id;

            return View();
        }

        public async Task<ActionResult> Save(EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = db.Employees
                .Include(x => x.Person)
                .SingleOrDefault(x => x.EmployeeId == viewModel.EmployeeId);
            try
            {
                if (employee == null)
                {
                    var person = new Person
                    {
                        CreatedAt = DateTime.Now,
                        LastName = viewModel.LastName,
                        FirstName = viewModel.FirstName,
                        MiddleName = viewModel.MiddleName,
                        Birthday = viewModel.Birthday,
                        Address = viewModel.Address,
                        Phone = viewModel.Phone,
                        SexId = viewModel.SexId
                    };

                    employee = new Employee
                    {
                        Person = person,
                        HireDate = viewModel.HireDate,
                        PositionId = viewModel.PositionId
                    };

                    db.Employees.Add(employee);
                }
                else
                {
                    employee.Person.UpdatedAt = DateTime.Now;
                    employee.Person.LastName = viewModel.LastName;
                    employee.Person.FirstName = viewModel.FirstName;
                    employee.Person.MiddleName = viewModel.MiddleName;
                    employee.Person.Birthday = viewModel.Birthday;
                    employee.Person.Address = viewModel.Address;
                    employee.Person.Phone = viewModel.Phone;
                    employee.Person.SexId = viewModel.SexId;
                    employee.PositionId = viewModel.PositionId;
                    employee.HireDate = viewModel.HireDate;
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
            var positions = await db.Positions
                 .Select(x => new
                 {
                     x.PositionId,
                     x.PositionName
                 })
                 .ToListAsync();

            return Json(new { Positions = positions }, JsonRequestBehavior.AllowGet);
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
