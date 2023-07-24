using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Contenido { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; }
    }
}
