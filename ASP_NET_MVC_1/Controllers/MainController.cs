using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_NET_MVC_1.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ASP_NET_MVC_1.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        // GET: Main
        public ActionResult EmpleadosView()
        {
            var dataList = new Models.db_pruebasEntities();
            var data = dataList.Empleado.ToList();



            return View(data);
        }

        // GET: Main/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        List<Models.Detalles_Empleado> detallesEmpleados;

        // GET: Main/Details/5
        public ActionResult DetailView(int id)
        {
            using (var dataModel = new Models.db_pruebasEntities())
            {
                detallesEmpleados = dataModel.Detalles_Empleado.Where(x=>x.fk_empleado==id).ToList();

                var entryPoint = (from ep in dataModel.Detalles_Empleado
                                  join e in dataModel.Empleado on ep.fk_empleado equals e.id
                                  where ep.fk_empleado == id
                                  select new Models.Detail_FK
                                  {
                                     Nombre = e.name,
                                     Descripcion = ep.Descripcion,
                                     Telefono = ep.Telefono
                                  }).ToList();

                var data = entryPoint as List<Models.Detail_FK>;
                return View(entryPoint);

            }
            //return View(detallesEmpleados);
        }


        // GET: Main/Create
        public ActionResult Create()
        {                
            return View();
        }

        public ActionResult NuevoEmpleadoView()
        {
            return View();
        }

        // POST: Main/Create
        [HttpPost]
        public ActionResult Create(Models.Empleado empleado)
        {
            try
            {
                using (var dataModel = new Models.db_pruebasEntities())
                {
                    var employee = dataModel.Empleado.Add(empleado);
                    var action = dataModel.SaveChanges();

                    return RedirectToAction("EmpleadosView");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Main/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Main/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Empleado empleado)
        {
            try
            {
                using (var dataModel = new Models.db_pruebasEntities())
                {
                    var employee = dataModel.Empleado.Where(x => x.id == id).FirstOrDefault();
                    employee.mail = empleado.mail;
                    employee.name = empleado.name;
                    employee.pass = empleado.pass;

                    var action = dataModel.SaveChanges();

                    return RedirectToAction("EmpleadosView");
                }                
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Main/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Main/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        //=================================================================================================
        //=================================================================================================
        //PartierView va a llamar una vista otro lado
        //redirec to action
        public ActionResult EditarRegistro(int id)
        {
            using (var dataModel = new Models.db_pruebasEntities())
            {                
                var employee = dataModel.Empleado.Where(x => x.id == id).FirstOrDefault();
                //var employee = new Models.Empleado();

                return View(employee);
            }            
        }

        public ActionResult Sent()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            var correosStrings = "ralvarado@stefaninicolombia.com";
            var fromMail = "carlosdiiz_algo@hotmail.com";

            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                //message.To.Add(new MailAddress("recipient@gmail.com"));  // replace with valid value 
                message.To.Add(new MailAddress(correosStrings));  // replace with valid value 
                message.From = new MailAddress(fromMail);  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = fromMail,  // replace with valid value
                        Password = "Spiderk33"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    //smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    //await smtp.SendMailAsync(message);
                    smtp.Send(message);
                    //return RedirectToAction("Sent");
                    return View();
                }
            }
            return View(model);
        }
    }
}
