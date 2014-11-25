using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MathPro.Domain.Entities;
using MathPro.WebUI.DbContexts;

namespace MathPro.WebUI.Controllers
{
    public class TaskCommentsController : Controller
    {
        private ApplicationDb db = new ApplicationDb();

        // GET: TaskComments
        public ActionResult Index()
        {
            var taskComments = db.TaskComments.Include(t => t.ApplicationUser).Include(t => t.MathAssignment);
            return View(taskComments.ToList());
        }

        // GET: TaskComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskComment taskComment = db.TaskComments.Find(id);
            if (taskComment == null)
            {
                return HttpNotFound();
            }
            return View(taskComment);
        }

        // GET: TaskComments/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.MathAssignmentId = new SelectList(db.MathAssignments, "MathAssignmentId", "AssignmentText");
            return View();
        }

        // POST: TaskComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskCommentId,ApplicationUserId,MathAssignmentId,Details,PostedTime")] TaskComment taskComment)
        {
            if (ModelState.IsValid)
            {
                db.TaskComments.Add(taskComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", taskComment.ApplicationUserId);
            ViewBag.MathAssignmentId = new SelectList(db.MathAssignments, "MathAssignmentId", "AssignmentText", taskComment.MathAssignmentId);
            return View(taskComment);
        }

        // GET: TaskComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskComment taskComment = db.TaskComments.Find(id);
            if (taskComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", taskComment.ApplicationUserId);
            ViewBag.MathAssignmentId = new SelectList(db.MathAssignments, "MathAssignmentId", "AssignmentText", taskComment.MathAssignmentId);
            return View(taskComment);
        }

        // POST: TaskComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskCommentId,ApplicationUserId,MathAssignmentId,Details,PostedTime")] TaskComment taskComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", taskComment.ApplicationUserId);
            ViewBag.MathAssignmentId = new SelectList(db.MathAssignments, "MathAssignmentId", "AssignmentText", taskComment.MathAssignmentId);
            return View(taskComment);
        }

        // GET: TaskComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskComment taskComment = db.TaskComments.Find(id);
            if (taskComment == null)
            {
                return HttpNotFound();
            }
            return View(taskComment);
        }

        // POST: TaskComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskComment taskComment = db.TaskComments.Find(id);
            db.TaskComments.Remove(taskComment);
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
