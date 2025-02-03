using Backend.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Data
{
    public class BackendContext : DbContext
    {

        public BackendContext(DbContextOptions<BackendContext> options) : base(options)
        {
            // ovdje bi upravljali s razlicitim opcijama, za sada nista

        }

        public DbSet<Udomitelj> Udomitelji { get; set; } // zbog ovog ovdje Smjerovi se tablica zove u mnozini





    }
}
