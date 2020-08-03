using Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class TaskStatusController : Controller
    {
        private TMSEntities db = new TMSEntities();

        [Authorize(Roles = "Admin, Employee")]
        // GET: TaskStatus
        public ActionResult Index()
        {
            return View(db.TaskStatus1.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: TaskStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "StatusId,StatusName")] TaskStatus taskStatus)
        {
            if (ModelState.IsValid)
            {
                db.TaskStatus1.Add(taskStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskStatus);
        }

        // GET: TaskStatus/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskStatus taskStatus = db.TaskStatus1.Find(id);
            if (taskStatus == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus);
        }

        // POST: TaskStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StatusId,StatusName")] TaskStatus taskStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskStatus);
        }

        // GET: TaskStatus/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskStatus taskStatus = db.TaskStatus1.Find(id);
            if (taskStatus == null)
            {
                return HttpNotFound();
            }
            return View(taskStatus);
        }

        // POST: TaskStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskStatus taskStatus = db.TaskStatus1.Find(id);
            db.TaskStatus1.Remove(taskStatus);
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
