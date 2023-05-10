using Microsoft.EntityFrameworkCore;
using ContinentalAPI.Models;

namespace ContinentalAPI.Data
{
    public class PersonaContext : DbContext
    {
        public PersonaContext(DbContextOptions<PersonaContext> options)
            : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
    }
}