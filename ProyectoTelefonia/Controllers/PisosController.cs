using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoTelefonia;

namespace ProyectoTelefonia.Controllers
{
    [Authorize(Roles = "administrador")]
    public class PisosController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Pisos
        public ActionResult Index()
        {
            return View(db.Piso.OrderBy(p => p.Numero).ToList());
        }

        // GET: Pisos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piso piso = db.Piso.Find(id);
            if (piso == null)
            {
                return HttpNotFound();
            }
            return View(piso);
        }

        // GET: Pisos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Numero")] Piso piso)
        {
            if (ModelState.IsValid)
            {
                db.Piso.Add(piso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(piso);
        }

        // GET: Pisos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piso piso = db.Piso.Find(id);
            if (piso == null)
            {
                return HttpNotFound();
            }
            return View(piso);
        }

        // POST: Pisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Numero")] Piso piso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(piso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(piso);
        }

        // GET: Pisos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piso piso = db.Piso.Find(id);
            if (piso == null)
            {
                return HttpNotFound();
            }
            return View(piso);
        }

        // POST: Pisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Piso piso = db.Piso.Find(id);
            db.Piso.Remove(piso);
            db.SaveChanges();
            return RedirectToAction("Index");
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
