namespace WebApiAutores.DTOs
{
    public class FullLibroDTO: LibroDTO
    {
        public IEnumerable<ComentarioDTO> Comentarios { get; set; }
        public List<AutorDTO> Autores { get; set; }
    }
}
