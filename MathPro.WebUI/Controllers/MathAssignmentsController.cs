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
    public class MathAssignmentsController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: MathAssignments
        public ActionResult Index()
        {
            var mathAssignments = db.MathAssignments.Include(m => m.Complexity).Include(m => m.Section);
            return View(mathAssignments.ToList());
        }

        // GET: MathAssignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MathAssignment mathAssignment = db.MathAssignments.Find(id);
            if (mathAssignment == null)
            {
                return HttpNotFound();
            }
            return View(mathAssignment);
        }

        // GET: MathAssignments/Create
        public ActionResult Create()
        {
            ViewBag.ComplexityId = new SelectList(db.Complexities, "ComplexityId", "Name");
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Name");
            return View();
        }

        // POST: MathAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MathAssignmentId,SectionId,ComplexityId,AssignmentText,PointsForAssignment,Answer")] MathAssignment mathAssignment)
        {
            if (ModelState.IsValid)
            {
                db.MathAssignments.Add(mathAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComplexityId = new SelectList(db.Complexities, "ComplexityId", "Name", mathAssignment.ComplexityId);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Name", mathAssignment.SectionId);
            return View(mathAssignment);
        }

        // GET: MathAssignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MathAssignment mathAssignment = db.MathAssignments.Find(id);
            if (mathAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComplexityId = new SelectList(db.Complexities, "ComplexityId", "Name", mathAssignment.ComplexityId);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Name", mathAssignment.SectionId);
            return View(mathAssignment);
        }

        // POST: MathAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MathAssignmentId,SectionId,ComplexityId,AssignmentText,PointsForAssignment,Answer")] MathAssignment mathAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mathAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComplexityId = new SelectList(db.Complexities, "ComplexityId", "Name", mathAssignment.ComplexityId);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Name", mathAssignment.SectionId);
            return View(mathAssignment);
        }

        // GET: MathAssignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MathAssignment mathAssignment = db.MathAssignments.Find(id);
            if (mathAssignment == null)
            {
                return HttpNotFound();
            }
            return View(mathAssignment);
        }

        // POST: MathAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MathAssignment mathAssignment = db.MathAssignments.Find(id);
            db.MathAssignments.Remove(mathAssignment);
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
