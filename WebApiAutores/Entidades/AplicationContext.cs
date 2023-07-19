using Microsoft.EntityFrameworkCore;

namespace WebApiAutores.Entidades
{
    public class AplicationContext : DbContext
    {
        public AplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
    }
}
