using ProjectApprovalRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalApp2.DTOs.Project
{
    public class ProjectReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        //Relationship
        public ICollection<UserProjectMapping> UserProjectMappings { get; set; }
    }
}
