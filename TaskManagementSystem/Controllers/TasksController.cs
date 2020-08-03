using Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private TMSEntities db = new TMSEntities();

        // GET: Tasks
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Index()
        {
            //var tasks = db.Tasks.Include(t => t.Employee).Include(t => t.TaskStatus);
            //return View(tasks.ToList());
            return View(db.Tasks.ToList());
        }


        public ActionResult Search(string SearchBox)
        {
            var tasks = from t in db.Tasks select t;
            DateTime searchDate;

            if (!string.IsNullOrEmpty(SearchBox))
            {
                bool isdateSearch = DateTime.TryParse(SearchBox, out searchDate);
                if (isdateSearch)
                {
                    tasks = tasks.Where(s => s.AssignDate.Equals(searchDate));
                }
                else
                {
                    tasks = tasks.Where(t =>
                        t.TaskName.Contains(SearchBox)
                        || t.TaskDescription.Contains(SearchBox)
                        || t.Employee.EmployeeName.Contains(SearchBox)
                        || t.TaskStatus.StatusName.Contains(SearchBox));


                }
            }

            return View("Index", tasks.ToList());
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.StatusId = new SelectList(db.TaskStatus1, "StatusId", "StatusName");
            return View();
        }

        // POST: /Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "TaskId,TaskName,TaskDescription,AssignDate,EmployeeId,StatusId")] Task task)
        {

            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", task.EmployeeId);
            ViewBag.StatusId = new SelectList(db.TaskStatus1, "StatusId", "StatusName", task.StatusId);
            return View(task);
        }

        // GET: /Task/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", task.EmployeeId);
            ViewBag.StatusId = new SelectList(db.TaskStatus1, "StatusId", "StatusName", task.StatusId);
            return View(task);
        }

        // POST: /Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "TaskId,TaskName,TaskDescription,AssignDate,EmployeeId,StatusId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", task.EmployeeId);
            ViewBag.StatusId = new SelectList(db.TaskStatus1, "StatusId", "StatusName", task.StatusId);
            return View(task);
        }

        // GET: /Task/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: /Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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