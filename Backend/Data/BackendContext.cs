using Backend.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Data
{
    /// <summary>
    /// Kontekst baze podataka za aplikaciju Edunova.
    /// </summary>
    /// <remarks>
    /// Konstruktor koji prima opcije za konfiguraciju konteksta.
    /// </remarks>
    /// <param name="opcije">Opcije za konfiguraciju konteksta.</param>
    public class BackendContext(DbContextOptions<BackendContext> opcije): DbContext(opcije)
    {
        /// <summary>
        /// Skup podataka za entitet Pas.
        /// </summary>
        public DbSet<Pas> Psi { get; set; }

        /// <summary>
        /// Skup podataka za entitet Udomitelj.
        /// </summary>
        public DbSet<Udomitelj> Udomitelji { get; set; }

        /// <summary>
        /// Skup podataka za entitet Status.
        /// </summary>
        public DbSet<Status> Statusi { get; set; }

        /// <summary>
        /// Skup podataka za entitet Upit.
        /// </summary>
        public DbSet<Upit> Upiti { get; set; }

        /// <summary>
        /// Skup podataka za entitet Operater.
        /// </summary>
        public DbSet<Operater> Operateri { get; set; }

        /// <summary>
        /// Konfiguracija modela prilikom kreiranja baze podataka.
        /// </summary>
        /// <param name="modelBuilder">Graditelj modela.</param>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //implementacija 1:n veza

            modelBuilder.Entity<Pas>().HasOne(g => g.Status);
            modelBuilder.Entity<Upit>().HasOne(g => g.Pas);
            modelBuilder.Entity<Upit>().HasOne(g => g.Udomitelj);

        }


    }
}
