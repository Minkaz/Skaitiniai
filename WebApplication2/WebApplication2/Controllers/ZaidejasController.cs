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
    public class ZaidejasController : ApiController
    {
        private UniEntities5 db = new UniEntities5();

        [HttpGet]
        public IQueryable<Zaidejas> GetPlayerDetails()
        {

            return db.Zaidejas;

        }

        public IHttpActionResult GetPlayer(int id)
        {
            Zaidejas Zaidejas = db.Zaidejas.Find(id);
            if (Zaidejas == null)
            {
                return NotFound();
            }

            return Ok(Zaidejas);
        }


        // PUT: api/Zaidejas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayer(int id, Zaidejas Zaidejas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Zaidejas.ZaidejoID)
            {
                return BadRequest();
            }

            db.Entry(Zaidejas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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


        private bool PlayerExists(int id)
        {
            return db.Zaidejas.Count(e => e.ZaidejoID == id) > 0;
        }

        // POST: api/Zaidejas
        [ResponseType(typeof(Zaidejas))]
        public IHttpActionResult PostPlayer(Zaidejas Zaidejas)
        {

            db.Zaidejas.Add(Zaidejas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Zaidejas.ZaidejoID }, Zaidejas);
        }

        // DELETE: api/Zaidejas/5
        [ResponseType(typeof(Komanda))]
        public IHttpActionResult DeletePlayer(int id)
        {
            Zaidejas Zaidejas = db.Zaidejas.Find(id);
            if (Zaidejas == null)
            {
                return NotFound();
            }

            db.Zaidejas.Remove(Zaidejas);
            db.SaveChanges();

            return Ok(Zaidejas);
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
