using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CineMaster.Models;

namespace CineMaster.Controllers
{
    public class ClienteController : Controller
    {   
        
        private Database1Entities db = new Database1Entities();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\damian_pileckas\git\nt1\CineMaster2\CineMaster\App_Data\Database1.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public ActionResult Create()
        {
           
            if (Session["id"] != null && Session["dni"] != null && Session["nom"] != null && Session["tarjeta"] != null && Session["contra"] != null && Session["email"] != null)
            {
                var view = View();
                view.MasterName = "~/Views/Shared/_Layout2.cshtml";
                return view;
            }
            else
            {
                var view = View();
                view.MasterName = "~/Views/Shared/_Layout.cshtml";
                return view;
            }

        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre_Cliente,Dni_Cliente,Email,Nro_Tarjeta,Contrasenia")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {

                String cadSql = "Select * from Cliente where Dni_Cliente ='" + cliente.Dni_Cliente + "' ";

                SqlCommand command = new SqlCommand(cadSql, con);
                con.Open();


                SqlDataReader leer = command.ExecuteReader();

                if (leer.Read() != true)
                {
                    db.Cliente.Add(cliente);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home", new { area = "" });

                }
                else
                {
                    ViewBag.DescripcionError = "Ya te registraste";
                    var view = View();
                    view.MasterName = "~/Views/Shared/_Layout.cshtml";
                    return view;

                }

            }
            else {
                String busquedaCliente = "Select Dni_Cliente from Cliente where Logeado ='" + 1 + "' ";
                SqlCommand command = new SqlCommand(busquedaCliente, con);
                con.Open();
                SqlDataReader leer = command.ExecuteReader();

                if (leer.Read() == true)
                {
                    var view = View();
                    view.MasterName = "~/Views/Shared/_Layout2.cshtml";
                    return view;
                }
                else
                {
                    var view = View();
                    view.MasterName = "~/Views/Shared/_Layout.cshtml";
                    return view;
                }
            }
        }
        
        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cliente_ID,Nombre_Cliente,Dni_Cliente,Email,Nro_Tarjeta,Contrasenia")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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
