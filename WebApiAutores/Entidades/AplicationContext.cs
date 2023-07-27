using Microsoft.EntityFrameworkCore;

namespace WebApiAutores.Entidades
{
    public class AplicationContext : DbContext
    {
        public AplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LibroAutor>().HasKey(la => new {la.AutorId, la.LibroId});
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<LibroAutor> LibrosAutores { get; set; }
    }
}
