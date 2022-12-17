using People.Models;
using System;
using System.Collections.Generic;

namespace PEOPLE.Data
{
    public interface IPeopleRepo
    {
        bool SaveChanges();

        IEnumerable<Person> GetAllPeople();
        Person GetPersonById(Guid id);
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}
