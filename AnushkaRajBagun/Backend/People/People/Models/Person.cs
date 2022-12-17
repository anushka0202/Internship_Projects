using System;
using System.ComponentModel.DataAnnotations;

namespace People.Models
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(17, 61, ErrorMessage = "The Age should be between 18 and 60 years")]
        public int Age { get; set; }
    }
}
