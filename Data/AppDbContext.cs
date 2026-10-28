using Microsoft.EntityFrameworkCore;
using Tienda_don_Pedro.Models;

namespace Tienda_don_Pedro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
    }
}
