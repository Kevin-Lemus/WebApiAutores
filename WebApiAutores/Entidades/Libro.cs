using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 70, ErrorMessage = "El campo {0} no debe tener más de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
