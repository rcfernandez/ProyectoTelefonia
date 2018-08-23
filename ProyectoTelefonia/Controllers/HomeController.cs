using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Net.Mail;
//using System.Net;
//using Aspose.Email.Exchange;
//using Aspose.Email.Mail;


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

                //MailMessage mail = new MailMessage();

                //mail.To.Add("robert19arg@gmail.com");
                //mail.From = new MailAddress("robert19arg@gmail.com", "elrober");
                //mail.Subject = "sugerencia de interno sennaf de "+ contacto.Email;
                //mail.Body = contacto.Observacion;
                //mail.IsBodyHtml = true;

                //NetworkCredential credential = new NetworkCredential("robert19arg@gmail.com", "XXXXX");
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                //smtp.EnableSsl = true;  //Esto es para que vaya a través de SSL que es obligatorio con GMail
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = credential;
                //smtp.Send(mail);


                //IEWSClient clienteEx = EWSClient.GetEWSClient("https://correo.senaf.gob.ar", "rcfernandez", "XXXX", "conaf.net");

                //MailMessage mail = new MailMessage();

                //mail.From = "rcfernandez@senaf.gob.ar";
                //mail.To = "rcfernandez@outlook.com.ar";
                //mail.Subject = "Sugerencia de: " + contacto.Email;
                //mail.HtmlBody = "<h3>sending message from exchange server</h3>";

                //clienteEx.Send(mail);

                //NetworkCredential credential = new NetworkCredential("conaf\rcfernandez", "XXXX");
                //SmtpClient smtp = new SmtpClient("correo.senaf.gob.ar");
                ////smtp.Host = "correo.senaf.gob.ar";
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.UseDefaultCredentials = false;
                //smtp.EnableSsl = false;
                //smtp.Credentials = credential;
                //smtp.Send(mail);



                db.Contacto.Add(contacto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contacto);

        }


        public ActionResult ListadoSugerencias()    // tabla contacto 
        {
            return View(db.Contacto.ToList());
        }

    }
}