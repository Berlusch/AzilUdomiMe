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

            modelBuilder.Entity<Pas>().HasOne(g => g.Status);
            modelBuilder.Entity<Upit>().HasOne(g => g.Pas);
            modelBuilder.Entity<Upit>().HasOne(g => g.Udomitelj);

        }


    }
}
