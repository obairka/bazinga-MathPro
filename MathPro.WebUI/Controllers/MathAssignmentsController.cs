using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MathPro.WebUI.DbContexts;
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

            MathAssignmentSubsectionViewModel maSm = new MathAssignmentSubsectionViewModel();
            maSm.subsections = new List<Subsection>(db.Subsections);

            return View(maSm);
        }

        // POST: MathAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MathAssignmentId,SectionId,ComplexityId,AssignmentText,PointsForAssignment,Answer,Subsections")] MathAssignment mathAssignment)
        public string Create(MathAssignmentSubsectionViewModel maSm)
        {
            if (maSm.subsections.Count(x => x.IsSelected) == 0)
            {
                return "You haven't chosen anything, please try again";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You selected - ");
                foreach (var el in maSm.subsections)
                {
                    if (el.IsSelected)
                    {
                        sb.Append(el.Name + ", ");
                    }
                }
                sb.Remove(sb.ToString().LastIndexOf(","), 1);
                return sb.ToString();
            }
            //if (ModelState.IsValid)
            //{
            //    db.MathAssignments.Add(mathAssignment);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.ComplexityId = new SelectList(db.Complexities, "ComplexityId", "Name", mathAssignment.ComplexityId);
            //ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Name", mathAssignment.SectionId);
            //return View();
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
