using Microsoft.AspNetCore.Mvc;
using ProjectApprovalRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalRepo.Data.Repository
{
    public interface IProjectsRepo
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int id);
        Task<Project> GetProjectWithUserAsync(int id);
        Task<Project> CreateProject(Project project);
        Task<Project> UpdateProject(int id, Project project);
        Task<bool> DeleteProject(Project projectToDelete);
        Task<bool> SaveChangesAsync();
    }
}
