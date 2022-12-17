using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApprovalRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectApprovalRepo.Data.Repository
{
    public class ProjectsRepo : IProjectsRepo
    {
        private readonly AppDbContext _context;

        public ProjectsRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectById(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task<Project> GetProjectWithUserAsync(int id)
        {
            var project = await _context.Projects
                .Include("UserProjectMappings")
                .FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task<Project> CreateProject(Project project)
        {
            project.CreatedOn = DateTime.UtcNow;
            project.ModifiedOn = DateTime.UtcNow;
            //_context.Entry(project.AppUser).State = EntityState.Unchanged;
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateProject(int id, Project project)
        {
            project.ModifiedOn = DateTime.UtcNow;
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteProject(Project projectToDelete)
        {
            _context.Projects.Remove(projectToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

    }
}
