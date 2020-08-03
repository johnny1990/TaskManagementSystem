using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TaskManagementSystem.Services.Employee
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeService" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {

        [OperationContract]
        List<Models.Employee> GetAllEmployees();

        [OperationContract]
        int AddEmployee(string Name);

        [OperationContract]
        Models.Employee GetAllEmployeesById(int id);

        [OperationContract]
        int UpdateEmployee(int Id, string Name);

        [OperationContract]
        int DeleteEmployeeById(int Id);
    }
}
