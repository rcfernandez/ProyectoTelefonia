using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoTelefonia.Controllers
{
    public class HomeController : Controller
    {
        ModelDB db = new ModelDB();

        // version del listado
        string versionListado = "2018.08.11";

        // GET: Index listado general
        public ActionResult Index()
        {
            ViewBag.version = versionListado;

            return View(db.SubArea
                            .OrderBy(s => s.Pisos.Select(p => p.Numero).FirstOrDefault())
                            .ThenBy(s => s.Nombre)
                            .ToList()
                            );
        }

        // action a vista parcial
        public ActionResult ListadoInternos()
        {
            ViewBag.version = versionListado;

            List<SubArea> listadoModel = db.SubArea
                                            .OrderBy(s => s.Pisos.Select(p => p.Numero).FirstOrDefault())
                                            .ThenBy(s => s.Nombre)
                                            .ToList();

            string headerFooter = "--print-media-type " +
                                  "--header-center \"-  I N T E R N O S        S . E . N . N . A . F .  - \" --header-font-size \"9\" --header-spacing 3 --header-font-name \"calibri light\" " +
                                  "--footer-center \"Pag.: [page]/[toPage] - Version: " + ViewBag.version + "\" --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\" ";

            return new Rotativa.PartialViewAsPdf("_ListadoInternos", listadoModel)
            {
                FileName = "ListadoDeInternosSENNAF_Pdf.pdf",
                PageMargins = new Rotativa.Options.Margins(10, 5, 15, 5),
                CustomSwitches = headerFooter
            };
        }

        //public ActionResult Correos()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    ViewBag.correoEjemplo = "AdministradoresGDE@senaf.gob.ar";

        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // GET Contact
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                db.Contacto.Add(contacto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contacto);

        }


        public ActionResult ListadoContactos()
        {
            return View(db.Contacto.ToList());
        }

    }
}