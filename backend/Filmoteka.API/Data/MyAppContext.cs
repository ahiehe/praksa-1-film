using Filmoteka.API.Models;
using Microsoft.EntityFrameworkCore;
using praktika1.Models;

namespace praktika1.Data
{
    public class MyAppContext : DbContext 
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }
        public DbSet<Film> Filmovi { get; set; }
        public DbSet<Zanr> Zanrovi { get; set; }
        public DbSet<Reziser> Reziseri { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sala> Salas { get; set; }

    }
}
