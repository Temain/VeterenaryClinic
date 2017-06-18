using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VeterinaryClinic.Domain.Context;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Web.Models;
using VeterinaryClinic.Web.Models.Person;

namespace VeterinaryClinic.Web.Controllers
{
    [Authorize]
    public class PersonController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Person
        public IQueryable<PersonViewModel> GetPersons()
        {
            var persons = db.Persons
                .Select(x => new PersonViewModel
                {
                    PersonId = x.PersonId,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    Birthday = x.Birthday,
                    Pets = x.Pets
                        .Select(p => new PetViewModel
                        {
                            PetId = p.PetId,
                            PetName = p.PetName,
                            PetTypeId = p.PetTypeId,
                            PetTypeName = p.PetType.PetTypeName
                        })
                        .ToList()
                });

            return persons;
        }

        // GET: api/Person/5
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> GetPerson(int id)
        {
            Person person = await db.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            var viewModel = new PersonViewModel
            {
                PersonId = person.PersonId,
                LastName = person.LastName,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                Birthday = person.Birthday,
                Pets = person.Pets
                    .Select(p => new PetViewModel
                    {
                        PetId = p.PersonId,
                        PetName = p.PetName,
                        PetTypeId = p.PetTypeId,
                    })
                    .ToList()
            };

            return Ok(viewModel);
        }

        // PUT: api/Person/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPerson(int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.PersonId)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Person
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Persons.Add(person);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = person.PersonId }, person);
        }

        // DELETE: api/Person/5
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> DeletePerson(int id)
        {
            Person person = await db.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.Persons.Remove(person);
            await db.SaveChangesAsync();

            return Ok(person);
        }

        [HttpGet]
        public IHttpActionResult GetDictionaries()
        {
            var petTypes = db.PetTypes.Select(x => new PetTypeViewModel
            {
                PetTypeId = x.PetTypeId,
                PetTypeName = x.PetTypeName
            });

            return Ok(new
            {
                PetTypes = petTypes
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.Persons.Count(e => e.PersonId == id) > 0;
        }
    }
}