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
    public class VarzybuController : ApiController
    {
        private UniEntities4 db = new UniEntities4();

        [HttpGet]
        public IQueryable<Varzybos> GetGameDetails()
        {

            return db.Varzybos;

        }

        public IHttpActionResult GetGame(int id)
        {
            Varzybos Varzybos = db.Varzybos.Find(id);
            if (Varzybos == null)
            {
                return NotFound();
            }

            return Ok(Varzybos);
        }


        // PUT: api/Varzybos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGame(int id, Varzybos Varzybos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Varzybos.VarzybuID)
            {
                return BadRequest();
            }

            db.Entry(Varzybos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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


        private bool GameExists(int id)
        {
            return db.Varzybos.Count(e => e.VarzybuID == id) > 0;
        }

        // POST: api/Varzybos
        [ResponseType(typeof(Varzybos))]
        public IHttpActionResult PostGame(Varzybos Varzybos)
        {

            db.Varzybos.Add(Varzybos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Varzybos.VarzybuID }, Varzybos);
        }

        // DELETE: api/Varzybos/5
        [ResponseType(typeof(Turnyras))]
        public IHttpActionResult DeleteGame(int id)
        {
            Varzybos Varzybos = db.Varzybos.Find(id);
            if (Varzybos == null)
            {
                return NotFound();
            }

            db.Varzybos.Remove(Varzybos);
            db.SaveChanges();

            return Ok(Varzybos);
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
