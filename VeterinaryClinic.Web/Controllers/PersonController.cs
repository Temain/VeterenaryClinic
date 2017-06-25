using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VeterinaryClinic.Domain.Context;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Web.Models.Person;
using VeterinaryClinic.Web.Models;

namespace VeterinaryClinic.Web.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Person
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPersons()
        {
            var persons = db.Persons
                .Include(x => x.Employees)
                .Include(x => x.Pets.Select(p => p.PetType))
                .Include(x => x.Pets.Select(a => a.Appointments))
                .Where(x => !x.Employees.Any())
                .Select(x => new PersonViewModel
                {
                    PersonId = x.PersonId,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    Birthday = x.Birthday,
                    Address = x.Address,
                    Phone = x.Phone,
                    SexId = x.SexId,
                    LastAppointmentDate =
                        x.Pets.SelectMany(a => a.Appointments).Any()
                        ? x.Pets
                            .SelectMany(a => a.Appointments)
                            .OrderByDescending(a => a.AppointmentDate)
                            .Select(a => a.AppointmentDate)
                            .FirstOrDefault()
                            .ToString()
                        : "",
                    Pets = x.Pets
                        .Where(p => p.DeletedAt == null)
                        .Select(p => new PetViewModel
                        {
                            PetId = p.PetId,
                            PetName = p.PetName,
                            PetTypeId = p.PetTypeId,
                            PetTypeName = p.PetType.PetTypeName,
                            SexId = p.SexId,
                            HaveOperations = p.HaveOperations,
                            Allergies = p.Allergies,
                            ChronicDiseases = p.ChronicDiseases
                        })
                        .ToList()
                })
                .ToList();

            return Json(persons, JsonRequestBehavior.AllowGet);
        }

        // GET: api/Person/5
        [HttpGet]
        public ActionResult GetPerson(int id)
        {
            var person = db.Persons
                .Include(x => x.Pets)
                .Where(x => !x.Employees.Any())
                .SingleOrDefault(x => x.PersonId == id && x.DeletedAt == null);
            if (person == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PersonViewModel
            {
                PersonId = person.PersonId,
                LastName = person.LastName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                Birthday = person.Birthday,
                Address = person.Address,
                Phone = person.Phone,
                SexId = person.SexId,
                Pets = person.Pets
                    .Where(x => x.DeletedAt == null)
                    .Select(p => new PetViewModel
                    {
                        PetId = p.PetId,
                        PetName = p.PetName,
                        PetTypeId = p.PetTypeId,
                        SexId = p.SexId,
                        HaveOperations = p.HaveOperations,
                        Allergies = p.Allergies,
                        ChronicDiseases = p.ChronicDiseases
                    })
                    .ToList()
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
            var person = db.Persons.SingleOrDefault(x => x.PersonId == id && x.DeletedAt == null);
            if (person == null)
            {
                return HttpNotFound();
            }

            ViewBag.PersonId = id;

            return View();
        }

        public async Task<ActionResult> Save(PersonViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var person = db.Persons
                .Include(x => x.Pets)
                .SingleOrDefault(x => x.PersonId == viewModel.PersonId && x.DeletedAt == null);
            try
            {
                if (person == null)
                {
                    person = new Person
                    {
                        CreatedAt = DateTime.Now,
                        LastName = viewModel.LastName,
                        FirstName = viewModel.FirstName,
                        MiddleName = viewModel.MiddleName,
                        Birthday = viewModel.Birthday,
                        Address = viewModel.Address,
                        Phone = viewModel.Phone,
                        SexId = viewModel.SexId,
                        Pets = viewModel.Pets
                            .Select(x => new Pet
                            {
                                PetName = x.PetName,
                                PetTypeId = x.PetTypeId,
                                SexId = x.SexId,
                                HaveOperations = x.HaveOperations,
                                Allergies = x.Allergies,
                                ChronicDiseases = x.ChronicDiseases,
                                CreatedAt = DateTime.Now
                            })
                            .ToList()
                    };

                    db.Persons.Add(person);
                }
                else
                {
                    person.UpdatedAt = DateTime.Now;
                    person.LastName = viewModel.LastName;
                    person.FirstName = viewModel.FirstName;
                    person.MiddleName = viewModel.MiddleName;
                    person.Birthday = viewModel.Birthday;
                    person.Address = viewModel.Address;
                    person.Phone = viewModel.Phone;
                    person.SexId = viewModel.SexId;

                    foreach (var petViewModel in viewModel.Pets)
                    {
                        var pet = person.Pets.SingleOrDefault(x => x.PetId == petViewModel.PetId && petViewModel.PetId != 0 && x.DeletedAt == null);
                        if (pet == null)
                        {
                            pet = new Pet
                            {
                                PetName = petViewModel.PetName,
                                PetTypeId = petViewModel.PetTypeId,
                                SexId = petViewModel.SexId,
                                HaveOperations = petViewModel.HaveOperations,
                                Allergies = petViewModel.Allergies,
                                ChronicDiseases = petViewModel.ChronicDiseases,
                                CreatedAt = DateTime.Now
                            };

                            person.Pets.Add(pet);
                        }
                        else
                        {
                            pet.PetName = petViewModel.PetName;
                            pet.PetTypeId = petViewModel.PetTypeId;
                            pet.SexId = petViewModel.SexId;
                            pet.HaveOperations = petViewModel.HaveOperations;
                            pet.Allergies = petViewModel.Allergies;
                            pet.ChronicDiseases = petViewModel.ChronicDiseases;
                            pet.UpdatedAt = DateTime.Now;
                        }
                    }

                    foreach (var pet in person.Pets)
                    {
                        if (pet.PetId != 0 && pet.DeletedAt == null)
                        {
                            var petViewModel = viewModel.Pets.SingleOrDefault(x => x.PetId == pet.PetId);
                            if (petViewModel == null)
                            {
                                pet.DeletedAt = DateTime.Now;
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
        public async Task<ActionResult> GetDictionaries()
        {
            var petTypes = await db.PetTypes
                .Select(x => new PetTypeViewModel
                {
                    PetTypeId = x.PetTypeId,
                    PetTypeName = x.PetTypeName
                })
                .ToListAsync();

            return Json(new { PetTypes = petTypes }, JsonRequestBehavior.AllowGet);
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
