using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services.Projects
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProjectsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProjectsService.svc or ProjectsService.svc.cs at the Solution Explorer and start debugging.
    public class ProjectsService : IProjectsService
    {
        public int AddProject(string Name, string Description)
        {
            TMSEntities tstDb = new TMSEntities();
            Models.Project prj = new Models.Project();
            prj.ProjectName = Name;
            prj.ProjectDescription = Description;
            tstDb.Projects.Add(prj);
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public int DeleteProjectById(int Id)
        {
            TMSEntities tstDb = new TMSEntities();
            Models.Project prj = new Models.Project();
            prj.ProjectId= Id;
            tstDb.Entry(prj).State = EntityState.Deleted;
            int Retval = tstDb.SaveChanges();
            return Retval;
        }

        public List<Project> GetAllProjects()
        {

            List<Models.Project> prjlst = new List<Models.Project>();
            TMSEntities tstDb = new TMSEntities();
            var lstPrj = from k in tstDb.Projects select k;
            foreach (var item in prjlst)
            {
                Models.Project prj = new Models.Project();
                prj.ProjectId = item.ProjectId;
                prj.ProjectName = item.ProjectName;
                prj.ProjectDescription = item.ProjectDescription;
                prjlst.Add(prj);

            }

            return prjlst;
        }

        public Project GetAllProjectsById(int? id)
        {
            TMSEntities tstDb = new TMSEntities();
            var lstPrj = from k in tstDb.Projects where k.ProjectId == id select k;
            Models.Project proj = new Models.Project();
            foreach (var item in lstPrj)
            {
                Models.Project prj = new Models.Project();

                prj.ProjectId = item.ProjectId;
                prj.ProjectName = item.ProjectName;
                prj.ProjectDescription = item.ProjectDescription;
            }
            return proj;
        }

        public int UpdateProject(int Id, string Name, string Description)
        {
            TMSEntities tstDb = new TMSEntities();
            Models.Project prj = new Models.Project();
            prj.ProjectId = Id;
            prj.ProjectName = Name;
            prj.ProjectDescription = Description;
            tstDb.Entry(prj).State = EntityState.Modified;

            int Retval = tstDb.SaveChanges();
            return Retval;
        }
    }
}
