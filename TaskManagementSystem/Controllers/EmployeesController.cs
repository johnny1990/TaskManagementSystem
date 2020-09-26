//using Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.Services.Employee;

namespace TaskManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        EmployeeService emp = new EmployeeService();
        // GET: Employees
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Index()
        {
            List<Employee> lstRecord = new List<Employee>();

            var lst = emp.GetAllEmployees();

            foreach (var item in lst)
            {
                Employee e = new Employee();
                e.EmployeeId = item.EmployeeId;
                e.EmployeeName = item.EmployeeName;
                lstRecord.Add(e);

            }
            return View(lstRecord);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "EmployeeId,EmployeeName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee empl = new Employee();
                empl.EmployeeName = employee.EmployeeName;
                emp.AddEmployee(empl.EmployeeName);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = emp.GetAllEmployeesById(id); 
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "EmployeeId,EmployeeName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                emp.UpdateEmployee(employee.EmployeeId, employee.EmployeeName);
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }        
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = emp.GetAllEmployeesById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = emp.GetAllEmployeesById(id);
            emp.DeleteEmployeeById(id);
            return RedirectToAction("Index");
        }
    }
}
