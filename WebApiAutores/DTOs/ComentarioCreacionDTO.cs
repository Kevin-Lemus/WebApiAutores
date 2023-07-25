using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTOs
{
    public class ComentarioCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Contenido { get; set; }
    }
}
