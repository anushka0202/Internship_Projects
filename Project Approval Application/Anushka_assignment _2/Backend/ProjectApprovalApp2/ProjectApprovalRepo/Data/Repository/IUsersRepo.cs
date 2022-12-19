using ProjectApprovalRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalRepo.Data.Repository
{
    public interface IUsersRepo
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserWithProjectsAsync(string id);
    }
}
