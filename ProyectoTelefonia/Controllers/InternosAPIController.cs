using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProyectoTelefonia;

namespace ProyectoTelefonia.Controllers
{
    public class InternosAPIController : ApiController
    {
        private ModelDB db = new ModelDB();

        // GET: api/InternosAPI
        public IHttpActionResult GetInternos()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return Ok(db.Interno.ToList());
        }


        // GET: api/InternosAPI/5
        [ResponseType(typeof(Interno))]
        public IHttpActionResult GetInterno(long id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            Interno interno = db.Interno.Find(id);
            if (interno == null)
            {
                return NotFound();
            }

            return Ok(interno);
        }

        // PUT: api/InternosAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInterno(long id, Interno interno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interno.Id)
            {
                return BadRequest();
            }

            db.Entry(interno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InternoExists(id))
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

        // POST: api/InternosAPI
        [ResponseType(typeof(Interno))]
        public IHttpActionResult PostInterno(Interno interno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Interno.Add(interno);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = interno.Id }, interno);
        }

        // DELETE: api/InternosAPI/5
        [ResponseType(typeof(Interno))]
        public IHttpActionResult DeleteInterno(long id)
        {
            Interno interno = db.Interno.Find(id);
            if (interno == null)
            {
                return NotFound();
            }

            db.Interno.Remove(interno);
            db.SaveChanges();

            return Ok(interno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InternoExists(long id)
        {
            return db.Interno.Count(e => e.Id == id) > 0;
        }
    }
}