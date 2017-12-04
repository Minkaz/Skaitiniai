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
    public class KomandaController : ApiController
    {
        private UniEntities3 db = new UniEntities3();

        [HttpGet]
        public IQueryable<Komanda> GetTeamDetails()
        {

            return db.Komanda;

        }

        public IHttpActionResult GetTeam(int id)
        {
            Komanda Komanda = db.Komanda.Find(id);
            if (Komanda == null)
            {
                return NotFound();
            }

            return Ok(Komanda);
        }


        // PUT: api/Komanda/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeam(int id, Komanda Komanda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Komanda.KomandosID)
            {
                return BadRequest();
            }

            db.Entry(Komanda).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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


        private bool TeamExists(int id)
        {
            return db.Komanda.Count(e => e.KomandosID == id) > 0;
        }

        // POST: api/Komanda
        [ResponseType(typeof(Komanda))]
        public IHttpActionResult PostTeam(Komanda Komanda)
        {

            db.Komanda.Add(Komanda);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Komanda.KomandosID }, Komanda);
        }

        // DELETE: api/Komanda/5
        [ResponseType(typeof(Komanda))]
        public IHttpActionResult DeleteTeam(int id)
        {
            Komanda Komanda = db.Komanda.Find(id);
            if (Komanda == null)
            {
                return NotFound();
            }

            db.Komanda.Remove(Komanda);
            db.SaveChanges();

            return Ok(Komanda);
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

