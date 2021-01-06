using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TaskManagementSystem.Services.Projects
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProjectsService" in both code and config file together.
    [ServiceContract]
    public interface IProjectsService
    {
        [OperationContract]
        List<Models.Project> GetAllProjects();

        [OperationContract]
        int AddProject(string Name, string Description);

        [OperationContract]
        Models.Project GetAllProjectsById(int? id);

        [OperationContract]
        int UpdateProject(int Id, string Name, string Description);

        [OperationContract]
        int DeleteProjectById(int Id);
    }
}
