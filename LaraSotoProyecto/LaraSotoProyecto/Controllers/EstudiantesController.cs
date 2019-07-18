using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using LaraSotoProyecto.Models;

namespace LaraSotoProyecto.Controllers
{
    public class EstudiantesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Estudiantes
        public ActionResult Index(string sortOrder, String searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var students = from s in db.Estudiantes
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Apellido.Contains(searchString)
                                       || s.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderBy(s => s.Apellido);
                    break;               
            }
            return View(students.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult _PeriodoEstudiante(int PeriodoID)
        {
            var estudiante = from c in db.Estudiantes where c.PeriodosID == PeriodoID select c;
            return PartialView(estudiante.ToList());
        }

        // GET: Estudiantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            if (estudiantes == null)
            {
                return HttpNotFound();
            }
            return View(estudiantes);
        }
        [Authorize(Roles = "Admin2")]
        // GET: Estudiantes/Create
        public ActionResult Create()
        {
            ViewBag.PeriodosID = new SelectList(db.Periodos, "PeriodosID", "Semestre");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstudiantesID,PeriodosID,Nombre,Apellido,Matricula,CalificacionFinal")] Estudiantes estudiantes)
        {
            if (ModelState.IsValid)
            {
                db.Estudiantes.Add(estudiantes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PeriodosID = new SelectList(db.Periodos, "PeriodosID", "Semestre", estudiantes.PeriodosID);
            return View(estudiantes);
        }
        [Authorize(Roles = "Admin2")]
        // GET: Estudiantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            if (estudiantes == null)
            {
                return HttpNotFound();
            }
            ViewBag.PeriodosID = new SelectList(db.Periodos, "PeriodosID", "Semestre", estudiantes.PeriodosID);
            return View(estudiantes);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstudiantesID,PeriodosID,Nombre,Apellido,Matricula,CalificacionFinal")] Estudiantes estudiantes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiantes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PeriodosID = new SelectList(db.Periodos, "PeriodosID", "Semestre", estudiantes.PeriodosID);
            return View(estudiantes);
        }
        [Authorize(Roles = "Admin2")]
        // GET: Estudiantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            if (estudiantes == null)
            {
                return HttpNotFound();
            }
            return View(estudiantes);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            db.Estudiantes.Remove(estudiantes);
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
