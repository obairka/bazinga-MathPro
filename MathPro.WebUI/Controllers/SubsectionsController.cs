using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MathPro.Domain.Concrete;
using MathPro.Domain.Entities;

namespace MathPro.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubsectionsController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Subsections
        public ActionResult Index()
        {
            return View(db.Subsections.ToList());
        }

        // GET: Subsections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subsection subsection = db.Subsections.Find(id);
            if (subsection == null)
            {
                return HttpNotFound();
            }
            return View(subsection);
        }

        // GET: Subsections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subsections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubsectionId,Name")] Subsection subsection)
        {
            if (ModelState.IsValid)
            {
                db.Subsections.Add(subsection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subsection);
        }

        // GET: Subsections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subsection subsection = db.Subsections.Find(id);
            if (subsection == null)
            {
                return HttpNotFound();
            }
            return View(subsection);
        }

        // POST: Subsections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubsectionId,Name")] Subsection subsection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subsection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subsection);
        }

        // GET: Subsections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subsection subsection = db.Subsections.Find(id);
            if (subsection == null)
            {
                return HttpNotFound();
            }
            return View(subsection);
        }

        // POST: Subsections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subsection subsection = db.Subsections.Find(id);
            db.Subsections.Remove(subsection);
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
