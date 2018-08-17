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
    public class DirectosController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: Directos
        public ActionResult Index()
        {
            return View(db.Directo.OrderBy(d => d.Numero).ToList());
        }

        // GET: Directos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Directo directo = db.Directo.Find(id);

            if (directo == null)
            {
                return HttpNotFound();
            }
            return View(directo);
        }

        // GET: Directos/Create
        public ActionResult Create()
        {
            ViewBag.DDLSubareas = db.SubArea.OrderBy(s => s.Nombre).ToList();
            ViewBag.DDLPuestos = db.Puesto.ToList();

            ViewBag.DDLEstados = new List<SelectListItem>   {   new SelectListItem { Text = "Usado", Value = "usado" },
                                                                new SelectListItem { Text = "Libre", Value = "libre" },
                                                                new SelectListItem { Text = "Chequear", Value = "chequear"},
                                                                new SelectListItem { Text = "No Funciona", Value = "no funciona"}
            };

            return View();
        }

        // POST: Directos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Numero,Estado,NoMostrar,Observacion,SubArea_id,Puesto_id")] Directo directo)
        {
            if (ModelState.IsValid)
            {
                db.Directo.Add(directo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Puesto_id = new SelectList(db.Puesto, "Id", "NumeroTipo", directo.Puesto_id);
            ViewBag.SubArea_id = new SelectList(db.SubArea, "Id", "Nombre", directo.SubArea_id);
            return View(directo);
        }

        // GET: Directos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Directo directo = db.Directo.Find(id);

            if (directo == null)
            {
                return HttpNotFound();
            }

            ViewBag.DDLEstados = new List<SelectListItem>   {   new SelectListItem { Text = "Usado", Value = "usado" },
                                                                new SelectListItem { Text = "Libre", Value = "libre" },
                                                                new SelectListItem { Text = "Chequear", Value = "chequear"},
                                                                new SelectListItem { Text = "No Funciona", Value = "no funciona"}
             };

            ViewBag.DDLSubareas = db.SubArea.OrderBy(s => s.Nombre).ToList();
            ViewBag.DDLPuestos = db.Puesto.ToList();

            return View(directo);
        }

        // POST: Directos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Numero,Estado,NoMostrar,Observacion,SubArea_id,Puesto_id")] Directo directo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Puesto_id = new SelectList(db.Puesto, "Id", "NumeroTipo", directo.Puesto_id);
            ViewBag.SubArea_id = new SelectList(db.SubArea, "Id", "Nombre", directo.SubArea_id);
            return View(directo);
        }

        // GET: Directos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directo directo = db.Directo.Find(id);
            if (directo == null)
            {
                return HttpNotFound();
            }
            return View(directo);
        }

        // POST: Directos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Directo directo = db.Directo.Find(id);
            db.Directo.Remove(directo);
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
