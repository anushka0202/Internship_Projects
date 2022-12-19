using System.ComponentModel.DataAnnotations;

namespace PEOPLE.Dtos
{
    public class PersonUpdateDto
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(17, 61, ErrorMessage = "The Age should be greater than or equal to 18 and lesser than or equal to 60.")]
        public int Age { get; set; }
    }
}
