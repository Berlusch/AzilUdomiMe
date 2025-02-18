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

        public DbSet<Status> Statusi { get; set; }
        public DbSet<Upit> Upiti { get; set; }



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


            //Jedan udomitelj može imati više pasa, ali pas može imati jednog udomitelja ili ne
            modelBuilder.Entity<Udomitelj>()
                .HasMany(u => u.Psi)  // Jedan udomitelj može imati više pasa
                .WithOne(p => p.Udomitelj)    // Pas ima jednog udomitelja (ako postoji)
                .HasForeignKey(p => p.UdomiteljSifra)
                .IsRequired(false);  // Pas ne mora imati udomitelja pa nije obavezno

            modelBuilder.Entity<Pas>()
                .HasOne(p => p.StatusOpis)  // Pas može imati samo jedan status
                .WithMany(s => s.Psi)  // Status može imati više pasa
                .HasForeignKey(p => p.StatusSifra)
                .IsRequired();

            modelBuilder.Entity<Upit>()
                .HasOne(u => u.Pas)  // Upit je povezan s jednim psom
                .WithMany(p => p.Upiti)  // Pas može imati više upita
                .HasForeignKey(u => u.PasSifra)
                .IsRequired();

        }


    }
}
