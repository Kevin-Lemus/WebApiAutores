using WebApiAutores.Entidades;

namespace WebApiAutores.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public IEnumerable<ComentarioDTO> Comentarios { get; set; }
    }
}
