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
    public class ForosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Foros
        public ActionResult Index(string sortOrder, String searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var foro = from s in db.Foros
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                foro = foro.Where(s => s.Asunto.Contains(searchString)
                                       || s.Descripcion.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    foro = foro.OrderBy(s => s.Asunto);
                    break;
            }
            return View(foro.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult _ForosProfesor(int ProfesorID)
        {
            var foros = from c in db.Foros where c.ProfesoresID == ProfesorID select c;
            return PartialView(foros.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult _ForosEstudiante(int EstudianteID)
        {
            var foros = from c in db.Foros where c.EstudiantesID == EstudianteID select c;
            return PartialView(foros.ToList());
        }


        // GET: Foros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foros foros = db.Foros.Find(id);
            if (foros == null)
            {
                return HttpNotFound();
            }
            return View(foros);
        }
        [Authorize(Roles = "Profesor")]
        // GET: Foros/Create
        public ActionResult Create()
        {
            ViewBag.EstudiantesID = new SelectList(db.Estudiantes, "EstudiantesID", "Nombre");
            ViewBag.ProfesoresID = new SelectList(db.Profesores, "ProfesoresID", "NombreProfesor");
            return View();
        }

        // POST: Foros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForosID,EstudiantesID,ProfesoresID,Asunto,Descripcion,FechaCreacion")] Foros foros)
        {
            if (ModelState.IsValid)
            {
                db.Foros.Add(foros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstudiantesID = new SelectList(db.Estudiantes, "EstudiantesID", "Nombre", foros.EstudiantesID);
            ViewBag.ProfesoresID = new SelectList(db.Profesores, "ProfesoresID", "NombreProfesor", foros.ProfesoresID);
            return View(foros);
        }

        // GET: Foros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foros foros = db.Foros.Find(id);
            if (foros == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstudiantesID = new SelectList(db.Estudiantes, "EstudiantesID", "Nombre", foros.EstudiantesID);
            ViewBag.ProfesoresID = new SelectList(db.Profesores, "ProfesoresID", "NombreProfesor", foros.ProfesoresID);
            return View(foros);
        }

        // POST: Foros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForosID,EstudiantesID,ProfesoresID,Asunto,Descripcion,FechaCreacion")] Foros foros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstudiantesID = new SelectList(db.Estudiantes, "EstudiantesID", "Nombre", foros.EstudiantesID);
            ViewBag.ProfesoresID = new SelectList(db.Profesores, "ProfesoresID", "NombreProfesor", foros.ProfesoresID);
            return View(foros);
        }

        // GET: Foros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foros foros = db.Foros.Find(id);
            if (foros == null)
            {
                return HttpNotFound();
            }
            return View(foros);
        }

        // POST: Foros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Foros foros = db.Foros.Find(id);
            db.Foros.Remove(foros);
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
