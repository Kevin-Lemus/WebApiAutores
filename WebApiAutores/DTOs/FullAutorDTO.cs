namespace WebApiAutores.DTOs
{
    public class FullAutorDTO: AutorDTO
    {
        public List<LibroDTO> Libros { get; set; }
    }
}
