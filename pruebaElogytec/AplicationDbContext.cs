using Microsoft.EntityFrameworkCore;
using pruebaElogytec.Models;

namespace pruebaElogytec
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Productos> productos { get; set; }
        public DbSet<Marcas> marcas { get; set; }
    }
}
