using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MathPro.WebUI.DbContexts;
using MathPro.Domain.Entities;

namespace MathPro.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ComplexitiesController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Complexities
        public ActionResult Index()
        {
            return View(db.Complexities.ToList());
        }

        // GET: Complexities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complexity complexity = db.Complexities.Find(id);
            if (complexity == null)
            {
                return HttpNotFound();
            }
            return View(complexity);
        }

        // GET: Complexities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Complexities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComplexityId,Name,DefaultPoints")] Complexity complexity)
        {
            if (ModelState.IsValid)
            {
                db.Complexities.Add(complexity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complexity);
        }

        // GET: Complexities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complexity complexity = db.Complexities.Find(id);
            if (complexity == null)
            {
                return HttpNotFound();
            }
            return View(complexity);
        }

        // POST: Complexities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComplexityId,Name,DefaultPoints")] Complexity complexity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complexity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(complexity);
        }

        // GET: Complexities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complexity complexity = db.Complexities.Find(id);
            if (complexity == null)
            {
                return HttpNotFound();
            }
            return View(complexity);
        }

        // POST: Complexities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Complexity complexity = db.Complexities.Find(id);
            db.Complexities.Remove(complexity);
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
