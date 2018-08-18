using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoTelefonia;
using ProyectoTelefonia.Services;

namespace ProyectoTelefonia.Controllers
{
    [Authorize(Roles ="administrador")]
    public class InternosController : Controller
    {
        private ModelDB db = new ModelDB();

        InternoService internoService = new InternoService();

        // GET: Internos
        public ActionResult Index()
        {
            return View(db.Interno.OrderBy(i => i.Numero).ToList());
        }


        // GET: Internos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Interno interno = db.Interno.Find(id);

            if (interno == null)
            {
                return HttpNotFound();
            }

            return View(interno);
        }

        // GET: Internos/Create
        public ActionResult Create()
        {
            ViewBag.DDLTipos = new List<SelectListItem> {   new SelectListItem { Text = "s/n", Value = "s/n" },
                                                            new SelectListItem { Text = "Analogico", Value = "analogico" },
                                                            new SelectListItem { Text = "Digital", Value = "digital"}
                                                        };

            ViewBag.DDLEstados = new List<SelectListItem>   {   new SelectListItem { Text = "Usado", Value = "usado" },
                                                                new SelectListItem { Text = "Libre", Value = "libre" },
                                                                new SelectListItem { Text = "Chequear", Value = "chequear"},
                                                                new SelectListItem { Text = "No Funciona", Value = "no funciona"}
                                                            };

            ViewBag.DDLSubareas = db.SubArea.OrderBy(s => s.Nombre).ToList();

            ViewBag.DDLPuestos = db.Puesto.ToList();
            ViewBag.DDLPuestosTel = new List<string> { "00 (sin puestoTel)", "01 superior", "02 superior", "03 superior", "04 superior", "05 superior", "06 superior", "07 superior", "08 superior", "09 superior", "10 superior", "11 superior", "12 superior", "13 superior", "14 superior", "15 superior", "16 superior", "17 superior", "18 superior", "19 superior", "20 superior", "21 superior", "22 superior", "23 superior", "24 superior",
                                                                            "01 inferior", "02 inferior", "03 inferior", "04 inferior", "05 inferior", "06 inferior", "07 inferior", "08 inferior", "09 inferior", "10 inferior", "11 inferior", "12 inferior", "13 inferior", "14 inferior", "15 inferior", "16 inferior", "17 inferior", "18 inferior", "19 inferior", "20 inferior", "21 inferior", "22 inferior", "23 inferior", "24 inferior"
                                                     };

            return View();
        }

        // POST: Internos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Numero,Tipo,Tn,PuestoTel,Estado,NoMostrar,Observacion,SubArea_id,Puesto_id")] Interno interno)
        {
            if (ModelState.IsValid)
            {
                db.Interno.Add(interno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interno);
        }

        // GET: Internos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Interno interno = db.Interno.Find(id);

            if (interno == null)
            {
                return HttpNotFound();
            }

            ViewBag.DDLTipos = new List<SelectListItem> {   new SelectListItem { Text = "s/n", Value = "s/n" },
                                                            new SelectListItem { Text = "Analogico", Value = "analogico" },
                                                            new SelectListItem { Text = "Digital", Value = "digital"}
             };

            ViewBag.DDLEstados = new List<SelectListItem>   {   new SelectListItem { Text = "Usado", Value = "usado" },
                                                                new SelectListItem { Text = "Libre", Value = "libre" },
                                                                new SelectListItem { Text = "Chequear", Value = "chequear"},
                                                                new SelectListItem { Text = "No Funciona", Value = "no funciona"}
             };

            ViewBag.DDLSubareas = db.SubArea.OrderBy(s => s.Nombre).ToList();
            ViewBag.DDLPuestos = db.Puesto.ToList();
            ViewBag.DDLPuestosTel = new List<string> { "00 (sin puesto)", "01 superior", "02 superior", "03 superior", "04 superior", "05 superior", "06 superior", "07 superior", "08 superior", "09 superior", "10 superior", "11 superior", "12 superior", "13 superior", "14 superior", "15 superior", "16 superior", "17 superior", "18 superior", "19 superior", "20 superior", "21 superior", "22 superior", "23 superior", "24 superior",
                                                                          "01 inferior", "02 abajo", "03 abajo", "04 inferior", "05 inferior", "06 inferior", "07 inferior", "08 inferior", "09 inferior", "10 inferior", "11 inferior", "12 inferior", "13 inferior", "14 inferior", "15 inferior", "16 inferior", "17 inferior", "18 inferior", "19 inferior", "20 inferior", "21 inferior", "22 inferior", "23 inferior", "24 inferior"
                                                     };

            return View(interno);
        }

        // POST: Internos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Numero,Tipo,Tn,PuestoTel,Estado,NoMostrar,Observacion,SubArea_id,Puesto_id")] Interno interno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interno);
        }

        // GET: Internos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interno interno = db.Interno.Find(id);
            if (interno == null)
            {
                return HttpNotFound();
            }
            return View(interno);
        }

        // POST: Internos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Interno interno = db.Interno.Find(id);
            db.Interno.Remove(interno);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // BUSCADOR DE INTERNOS
        [HttpPost]
        public ActionResult BuscarInterno(long? numeroInterno)
        {
            if (ModelState.IsValid)
            {
                List<Interno> listInternos = internoService.buscarInterno(numeroInterno);

                return View("Index", listInternos);
            }

            if (numeroInterno.Equals(null))
            {
                ViewBag.error = "No ha ingresado ningun valor";
                return View("Index");
            }

            ViewBag.error = "el modelo no fue valido";
            return View("Index");
        }


        // accion para saber si existe un interno ingresado al crear/editar
        public JsonResult ExisteInterno(long Numero, long? Id)
        {
            var internoXId = db.Interno.Find(Id);

            // NRO NO EXISTE + ID NULL = NUMERO NUEVO = PASA
            if (db.Interno.Where(i => i.Numero == Numero).Select(i => i.Numero).FirstOrDefault() != Numero && Id == null)
            {
                // devuelve true si no existe el interno
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            // NRO EXISTE + ID NULL = NRO YA EXISTE = NO PASA
            if (db.Interno.Where(i => i.Numero == Numero).Select(i => i.Numero).FirstOrDefault() == Numero && Id == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            // NRO EXISTE + ID NOT NULL = NRO YA EXISTE Y NO SE MODIFICA = PASA
            if (internoXId.Numero == Numero)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            // NRO NO EXISTE + ID NOT NULL = NRO NUEVO EN EDIT = PASA
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        // accion para saber si existe un tn ingresado al crear/editar
        public JsonResult ExisteTn(string Tn, long? Id)
        {
            var internoXId = db.Interno.Find(Id);

            // INTERNO NUEVO
            if (internoXId == null)
            {
                // TN EXISTE
                if (db.Interno.Where(i => i.Tn == Tn).Select(i => i.Tn).FirstOrDefault() == Tn)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

                // TN NO EXISTE
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            // INTERNO A EDITAR + TN NO CAMBIA
            if (internoXId.Tn == Tn)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            // INTERNO A EDITAR + TN EXISTE
            if (db.Interno.Where(i => i.Tn == Tn).Select(i => i.Tn).FirstOrDefault() == Tn)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
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
