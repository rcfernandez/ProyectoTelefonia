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
    public class SubAreasController : Controller
    {
        private ModelDB db = new ModelDB();

        // GET: SubAreas
        public ActionResult Index()
        {
            return View(db.SubArea.OrderBy(p => p.Nombre).ToList());
        }

        // GET: SubAreas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubArea subArea = db.SubArea.Find(id);
            if (subArea == null)
            {
                return HttpNotFound();
            }
            return View(subArea);
        }

        // GET: SubAreas/Create
        public ActionResult Create()
        {
            ViewBag.DDLAreas = db.Area.OrderBy(a => a.Nombre).ToList();
            ViewBag.DDLPisos = db.Piso.OrderByDescending(p => p.Numero).ToList();

            return View();
        }

        // POST: SubAreas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreCompleto,Nombre,Referente,Area_id")] SubArea subArea, long id_piso)
        {
            if (ModelState.IsValid)
            {
                Piso pi = db.Piso.Find(id_piso);
                subArea.Pisos.Add(pi);
                db.SubArea.Add(subArea);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subArea);
        }

        // GET: SubAreas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubArea subArea = db.SubArea.Find(id);

            if (subArea == null)
            {
                return HttpNotFound();
            }

            ViewBag.DDLAreas = db.Area.ToList();
            ViewBag.DDLPisos = db.Piso.OrderBy(p => p.Numero).ToList();

            return View(subArea);
        }

        // POST: SubAreas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreCompleto,Nombre,Referente,Area_id")] SubArea subArea, long id_piso)
        {
            if (ModelState.IsValid)
            {
                // guardamos los datos de la subArea
                db.Entry(subArea).State = EntityState.Modified;
                db.SaveChanges();

                // recuperamos la coleccion de Pisos de la SubArea
                SubArea sa = db.SubArea.Include(s => s.Pisos).ToList().Find(su => su.Id == subArea.Id);

                //borramos los antiguos Piso.. ojo modificar esto luego ya que borra todos los p
                sa.Pisos.Clear();

                // añadimos el nuevo piso
                Piso pi = db.Piso.Find(id_piso);
                subArea.Pisos.Add(pi);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subArea);
        }

        // GET: SubAreas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubArea subArea = db.SubArea.Find(id);
            if (subArea == null)
            {
                return HttpNotFound();
            }
            return View(subArea);
        }

        // POST: SubAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SubArea subArea = db.SubArea.Find(id);
            db.SubArea.Remove(subArea);
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
