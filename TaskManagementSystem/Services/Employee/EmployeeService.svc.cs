using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services.Employee
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeService.svc or EmployeeService.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeService : IEmployeeService
    {
        public List<Models.Employee> GetAllEmployees()
        {
            List<Models.Employee> emplst = new List<Models.Employee>();
            TMSEntities tstDb = new TMSEntities();
            var lstEmp =  from k in tstDb.Employees select k;
            foreach (var item in lstEmp)
            {
                Models.Employee emp = new Models.Employee();
                emp.EmployeeId = item.EmployeeId;
                emp.EmployeeName = item.EmployeeName;
                emplst.Add(emp);

            }

            return emplst;
        }

        public Models.Employee GetAllEmployeesById(int id)
        {
            TMSEntities tstDb = new TMSEntities();
            var lstEmp = from k in tstDb.Employees where k.EmployeeId == id select k;
            Models.Employee emp = new Models.Employee();
            foreach (var item in lstEmp)
            {
                emp.EmployeeId = item.EmployeeId;
                emp.EmployeeName = item.EmployeeName;
            }
            return emp;
        }

        public int AddEmployee(string Name)
        {
            TMSEntities tstDb = new TMSEntities();
            Models.Employee emp = new Models.Employee();
            emp.EmployeeName = Name;
            tstDb.Employees.Add(emp);
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int UpdateEmployee(int Id, string Name)
        {
            TMSEntities tstDb = new TMSEntities();
            Models.Employee emp = new Models.Employee();
            emp.EmployeeId = Id;
            emp.EmployeeName = Name;
            tstDb.Entry(emp).State = EntityState.Modified;

            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int DeleteEmployeeById(int Id)
        {
            TMSEntities tstDb = new TMSEntities();
            Models.Employee emp = new Models.Employee();
            emp.EmployeeId = Id;
            tstDb.Entry(emp).State = EntityState.Deleted;
            int Retval = tstDb.SaveChanges();
            return Retval;
        }
    }
}
