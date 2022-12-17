using System;
using System.Collections.Generic;
using People.Models;

namespace PEOPLE.Data
{
    public class MockPeopleRepo : IPeopleRepo
    {
        public void CreatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAllPeople()
        {
            var peopleList = new List<Person>
            {
                new Person{Id=Guid.NewGuid(), FirstName="Anushka", LastName="Bagun", Age=22},
                new Person{Id=Guid.NewGuid(), FirstName="Abhay", LastName="Bagun", Age=25},
                new Person{Id=Guid.NewGuid(), FirstName="Ajit", LastName="Masih", Age=56}
            };

            return peopleList;
        }

        public Person GetPersonById(Guid id)
        {
            return new Person { Id = Guid.NewGuid(), FirstName = "Anushka", LastName = "Bagun", Age = 22 };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
