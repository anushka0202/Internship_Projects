using Microsoft.EntityFrameworkCore;
using People.Models;

namespace PEOPLE.Data
{
    public class PeopleContext : DbContext
    {
            public PeopleContext(DbContextOptions<PeopleContext> opt) : base(opt)
            {

            }

            public DbSet<Person> People { get; set; }

  
    }
}

