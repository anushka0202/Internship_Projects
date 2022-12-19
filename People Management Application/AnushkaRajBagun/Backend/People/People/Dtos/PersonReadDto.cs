using System;

namespace PEOPLE.Dtos
{
    public class PersonReadDto
    {

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
