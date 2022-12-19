using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalRepo.Models
{
    public class AppUser : IdentityUser
    {
        //Relationship
        public ICollection<UserProjectMapping> UserProjectMappings { get; set; }
        //public List<Project> Projects { get; set; }
    }
}
