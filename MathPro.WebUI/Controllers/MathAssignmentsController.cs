using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MathPro.WebUI.DbContexts;
using MathPro.Domain.Entities;
using MathPro.WebUI.Models;

namespace MathPro.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MathAssignmentsController : Controller
    {
        private ApplicationDb db = new ApplicationDb();

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
            //ViewBag.ComplexityId = new SelectList(db.Complexities, "ComplexityId", "Name");
            //ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Name");

            MathAssignmentViewModel maSm = new MathAssignmentViewModel();
            maSm.subsections = new SubsectionViewModel();
            maSm.subsections.AvailableSubsection = new List<Subsection>(db.Subsections);
            maSm.subsections.SelectedSubsection = new List<Subsection>();
            maSm.sections = db.Sections;
            maSm.complexities = db.Complexities;

            return View(maSm);
        }

        // POST: MathAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MathAssignmentViewModel maSm)
        {
            if (ModelState.IsValid)
            {
                var availableSubsections = new List<Subsection>(db.Subsections);
                var selectedSubsections = new List<Subsection>();
                var postedSubsectionIds = new string[0];
                if (maSm.subsections.PostedSubsection == null) maSm.subsections.PostedSubsection = new PostedSubsections();

                // if a view model array of posted seubsections ids exists
                // and is not empty,save selected ids
                if (maSm.subsections.PostedSubsection.SubsectionIds != null && maSm.subsections.PostedSubsection.SubsectionIds.Any())
                {
                    postedSubsectionIds = maSm.subsections.PostedSubsection.SubsectionIds;
                }

                // if there are any selected ids saved, create a list of seubsections
                if (maSm.subsections.PostedSubsection.SubsectionIds.Any())
                {
                    selectedSubsections = availableSubsections
                     .Where(x => postedSubsectionIds.Any(s => x.SubsectionId.ToString().Equals(s)))
                     .ToList();
                }

                maSm.subsections.AvailableSubsection = new List<Subsection>(db.Subsections); ;
                maSm.subsections.SelectedSubsection = selectedSubsections;
                maSm.mathAssignment.Subsections = selectedSubsections;

                maSm.mathAssignment.Complexity = db.Complexities.Find(maSm.mathAssignment.ComplexityId);
                maSm.mathAssignment.Section = db.Sections.Find(maSm.mathAssignment.SectionId);

                //if there is no points from admin, then the default points will be here
                if (maSm.mathAssignment.PointsForAssignment == null)
                {
                    maSm.mathAssignment.PointsForAssignment =
                        db.Complexities.Find(maSm.mathAssignment.ComplexityId).DefaultPoints;
                }


                db.MathAssignments.Add(maSm.mathAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ComplexityId = new SelectList(db.Complexities, "ComplexityId", "Name", maSm.mathAssignment.ComplexityId);
            //ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Name", maSm.mathAssignment.SectionId);
            return View();
        }

        // GET: MathAssignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MathAssignmentViewModel maSm = new MathAssignmentViewModel();
            maSm.mathAssignment = db.MathAssignments.Find(id);
            maSm.subsections = new SubsectionViewModel();
            maSm.subsections.AvailableSubsection = new List<Subsection>(db.Subsections);
            maSm.subsections.SelectedSubsection = new List<Subsection>(maSm.mathAssignment.Subsections);
            maSm.sections = db.Sections;
            maSm.complexities = db.Complexities;

            MathAssignment mathAssignment = db.MathAssignments.Find(id);
            if (db.MathAssignments == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ComplexityId = new SelectList(db.Complexities, "ComplexityId", "Name", mathAssignment.ComplexityId);
            //ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Name", mathAssignment.SectionId);
            return View(maSm);
        }

        // POST: MathAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MathAssignmentViewModel maSm)
        {
            if (ModelState.IsValid)
            {
                var availableSubsections = new List<Subsection>(db.Subsections);
                var selectedSubsections = new List<Subsection>();
                var postedSubsectionIds = new string[0];
                var mathToChange = db.MathAssignments.Find(maSm.mathAssignment.MathAssignmentId);
                if (maSm.subsections.PostedSubsection == null) maSm.subsections.PostedSubsection = new PostedSubsections();

                // if a view model array of posted seubsections ids exists
                // and is not empty,save selected ids
                if (maSm.subsections.PostedSubsection.SubsectionIds != null && maSm.subsections.PostedSubsection.SubsectionIds.Any())
                {
                    postedSubsectionIds = maSm.subsections.PostedSubsection.SubsectionIds;
                }

                // if there are any selected ids saved, create a list of seubsections
                if (maSm.subsections.PostedSubsection.SubsectionIds.Any())
                {
                    selectedSubsections = availableSubsections
                     .Where(x => postedSubsectionIds.Any(s => x.SubsectionId.ToString().Equals(s)))
                     .ToList();
                }

                mathToChange.Subsections.Clear();
                mathToChange.Subsections = selectedSubsections;

                mathToChange.Complexity = db.Complexities.Find(maSm.mathAssignment.ComplexityId);
                mathToChange.Section = db.Sections.Find(maSm.mathAssignment.SectionId);

                //if there is no points from admin, then the default points will be here
                if (maSm.mathAssignment.PointsForAssignment == null)
                {
                    mathToChange.PointsForAssignment =
                        db.Complexities.Find(maSm.mathAssignment.ComplexityId).DefaultPoints;
                }
                else
                {
                    mathToChange.PointsForAssignment =
                        maSm.mathAssignment.PointsForAssignment;
                }
                mathToChange.Answer = maSm.mathAssignment.Answer;
                mathToChange.AssignmentText = maSm.mathAssignment.AssignmentText;

                db.Entry(mathToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ComplexityId = new SelectList(db.Complexities, "ComplexityId", "Name", maSm.mathAssignment.ComplexityId);
            //ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Name", maSm.mathAssignment.SectionId);
            return View(maSm.mathAssignment);
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
