using Microsoft.EntityFrameworkCore;
using ProjectApprovalRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalRepo.Data.Repository
{
    public class UsersRepo : IUsersRepo
    {
        private readonly AppDbContext _context;

        public UsersRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetUserWithProjectsAsync(string id)
        {
            var user = await _context.AppUsers
                .Include("UserProjectMappings")
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}
