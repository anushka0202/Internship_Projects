using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApprovalRepo.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public DateTime ModifiedOn { get; set; }

        //Relationship
        public ICollection<UserProjectMapping> UserProjectMappings { get; set; }
        //public string? AppUserId { get; set; }
        //[ForeignKey("AppUserId")]
        //public AppUser? AppUser { get; set; }
    }
}
