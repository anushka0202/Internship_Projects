using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalRepo.Models
{
    public class UserProjectMapping
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
