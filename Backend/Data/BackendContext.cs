using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Backend.Data
{
    public class BackendContext : DbContext
    {
        public BackendContext(DbContextOptions<BackendContext> opcije) : base(opcije)
        {

        }


        public DbSet<Pas> Psi { get; set; }

        public DbSet<Udomitelj> Udomitelji { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // implementacija veze 1:n

            /* implementacija veze n:n
            modelBuilder.Entity<Grupa>()
                .HasMany(g => g.Polaznici)
                .WithMany(p => p.Grupe)
                .UsingEntity<Dictionary<string, object>>("clanovi",
                c => c.HasOne<Polaznik>().WithMany().HasForeignKey("polaznik"),
                c => c.HasOne<Grupa>().WithMany().HasForeignKey("grupa"),
                c => c.ToTable("clanovi")
                );*/

            base.OnModelCreating(modelBuilder);

            // Konfiguracija odnosa: jedan udomitelj može imati više pasa, ali pas ima samo jednog udomitelja
            // Konfiguracija odnosa: jedan udomitelj može imati više pasa, ali pas može imati jednog udomitelja ili ne
            modelBuilder.Entity<Udomitelj>()
                .HasMany(u => u.Psi)  // Jedan udomitelj može imati više pasa
                .WithOne(p => p.Udomitelj)    // Svaki pas ima jednog udomitelja (ako postoji)
                .HasForeignKey(p => p.UdomiteljSifra)  // UdomiteljSifra je strani ključ u entitetu Pas
                .IsRequired(false);  // Pas ne mora imati udomitelja, pa nije obavezno

        }


    }
}
