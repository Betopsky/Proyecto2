using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaraSotoProyecto.Models;

namespace LaraSotoProyecto.Controllers
{
    public class ArchivosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Archivos
        public ActionResult Index()
        {
            var archivos = db.Archivos.Include(a => a.Foros);
            return View(archivos.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult _ArchivoForo(int ForoID)
        {
            var archivo = from c in db.Archivos where c.ForosID == ForoID select c;
            return PartialView(archivo.ToList());
        }

        // GET: Archivos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivos archivos = db.Archivos.Find(id);
            if (archivos == null)
            {
                return HttpNotFound();
            }
            return View(archivos);
        }
        [Authorize(Roles = "Profesor")]
        [Authorize(Roles = "Estudiante")]
        // GET: Archivos/Create
        public ActionResult Create()
        {
            ViewBag.ForosID = new SelectList(db.Foros, "ForosID", "Asunto");
            return View();
        }

        // POST: Archivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArchivosID,ForosID,Titulo,FechaSubida")] Archivos archivos)
        {
            if (ModelState.IsValid)
            {
                db.Archivos.Add(archivos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ForosID = new SelectList(db.Foros, "ForosID", "Asunto", archivos.ForosID);
            return View(archivos);
        }
        [Authorize(Roles = "Profesor")]
        [Authorize(Roles = "Estudiante")]
        // GET: Archivos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivos archivos = db.Archivos.Find(id);
            if (archivos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForosID = new SelectList(db.Foros, "ForosID", "Asunto", archivos.ForosID);
            return View(archivos);
        }

        // POST: Archivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArchivosID,ForosID,Titulo,FechaSubida")] Archivos archivos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(archivos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ForosID = new SelectList(db.Foros, "ForosID", "Asunto", archivos.ForosID);
            return View(archivos);
        }
        [Authorize(Roles = "Profesor")]
        [Authorize(Roles = "Estudiante")]
        // GET: Archivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivos archivos = db.Archivos.Find(id);
            if (archivos == null)
            {
                return HttpNotFound();
            }
            return View(archivos);
        }

        // POST: Archivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Archivos archivos = db.Archivos.Find(id);
            db.Archivos.Remove(archivos);
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
