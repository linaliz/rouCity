using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBRouCity;

namespace RCITYWEB.Controllers
{
    public class viajesController : Controller
    {
        private RouCityEntities db = new RouCityEntities();

        // GET: viajes
        public ActionResult Index()
        {
            var viaje = db.Viaje.Include(v => v.Usuario);
            return View(viaje.ToList());
        }

        // GET: viajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viaje viaje = db.Viaje.Find(id);
            if (viaje == null)
            {
                return HttpNotFound();
            }
            return View(viaje);
        }

        // GET: viajes/Create
        public ActionResult Create()
        {
            ViewBag.IdUChofer = new SelectList(db.Usuario, "IdUsuario", "Nombre");
            return View();
        }

        // POST: viajes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdViaje,IdUChofer,latOrigen,longOrigen,latDestino,longDestino,fecha,precio,hora,cocheDescripcion,asientosDisponibles,calificacion,comentario,cantParada,tiempoParada")] Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                db.Viaje.Add(viaje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUChofer = new SelectList(db.Usuario, "IdUsuario", "Nombre", viaje.idUChofer);
            return View(viaje);
        }

        // GET: viajes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viaje viaje = db.Viaje.Find(id);
            if (viaje == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUChofer = new SelectList(db.Usuario, "IdUsuario", "Nombre", viaje.idUChofer);
            return View(viaje);
        }

        // POST: viajes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdViaje,IdUChofer,latOrigen,longOrigen,latDestino,longDestino,fecha,precio,hora,cocheDescripcion,asientosDisponibles,calificacion,comentario,cantParada,tiempoParada")] Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUChofer = new SelectList(db.Usuario, "IdUsuario", "Nombre", viaje.idUChofer);
            return View(viaje);
        }

        // GET: viajes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viaje viaje = db.Viaje.Find(id);
            if (viaje == null)
            {
                return HttpNotFound();
            }
            return View(viaje);
        }

        // POST: viajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Viaje viaje = db.Viaje.Find(id);
            db.Viaje.Remove(viaje);
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
