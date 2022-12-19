using People.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PEOPLE.Data
{
    public class SqlPeopleRepo : IPeopleRepo
    {
        private readonly PeopleContext _context;

        public SqlPeopleRepo (PeopleContext context)
        {
            _context = context;
        }

        //Creates a single Person
        public void CreatePerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            _context.People.Add(person);
        }

        //Deletes a single People
        public void DeletePerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            _context.People.Remove(person);
        }

        //Get list of People
        public IEnumerable<Person> GetAllPeople()
        {
            return _context.People.ToList();
        }

        //Get Person by id
        public Person GetPersonById(Guid id)
        {
            return _context.People.FirstOrDefault(p => p.Id == id);
        }

        //Save Changes
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        //Update a single person
        public void UpdatePerson(Person person)
        {
            //nothing
        }
    }
}
