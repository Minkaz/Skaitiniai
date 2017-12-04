using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TournamentController : ApiController
    {
        private UniEntities2 db = new UniEntities2();

        [HttpGet]
        public IQueryable<Turnyras> GetTourneyDetails()
        {

            return db.Turnyras;

        }

        public IHttpActionResult GetTournament(int id)
        {
            Turnyras Turnyras = db.Turnyras.Find(id);
            if (Turnyras == null)
            {
                return NotFound();
            }

            return Ok(Turnyras);
        }


        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTourney(int id, Turnyras turnyras)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != turnyras.TurnyroID)
            {
                return BadRequest();
            }

            db.Entry(turnyras).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourneyExists(id))
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


        private bool TourneyExists(int id)
        {
            return db.Turnyras.Count(e => e.TurnyroID == id) > 0;
        }

        // POST: api/Employees
        [ResponseType(typeof(Turnyras))]
        public IHttpActionResult PostTourney(Turnyras turnyras)
        {

            db.Turnyras.Add(turnyras);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = turnyras.TurnyroID }, turnyras);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Turnyras))]
        public IHttpActionResult DeleteTourney(int id)
        {
            Turnyras turnyras = db.Turnyras.Find(id);
            if (turnyras == null)
            {
                return NotFound();
            }

            db.Turnyras.Remove(turnyras);
            db.SaveChanges();

            return Ok(turnyras);
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

