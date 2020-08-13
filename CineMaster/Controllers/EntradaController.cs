using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CineMaster.Models;

namespace CineMaster.Controllers
{
    public class EntradaController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Entradas
        public ActionResult Index()
        {
            return View(db.Entrada.ToList());
        }

        // GET: Entradas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrada entrada = db.Entrada.Find(id);
            if (entrada == null)
            {
                return HttpNotFound();
            }
            return View(entrada);
        }

        // GET: Entradas/Create
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

                return RedirectToAction("Login", "Home", new { area = "" });
            }

        }

        // POST: Entradas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Asiento,Fecha,Hora,Sala")] Entrada entrada, int pelicula)
        {
            
            if (ModelState.IsValid)
            {
                entrada.Pelicula = pelicula;
                db.Entrada.Add(entrada);
                
                db.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(entrada);
        }

        // GET: Entradas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrada entrada = db.Entrada.Find(id);
            if (entrada == null)
            {
                return HttpNotFound();
            }
            return View(entrada);
        }

        // POST: Entradas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Entrada_ID,Asiento,Fecha,Hora,Sala")] Entrada entrada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entrada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entrada);
        }

        // GET: Entradas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrada entrada = db.Entrada.Find(id);
            if (entrada == null)
            {
                return HttpNotFound();
            }
            return View(entrada);
        }

        // POST: Entradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entrada entrada = db.Entrada.Find(id);
            db.Entrada.Remove(entrada);
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
