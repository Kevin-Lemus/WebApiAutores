using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 20, ErrorMessage = "El campo {0} no debe tener más de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public List<Libro> Libros { get; set; }

        //Propiedades para ejemplo de validaciones por modelo
        //[NotMapped]
        //public int numMayor { get; set; }
        //[NotMapped]
        //public int numMenor { get; set; }

        //validación por modelo
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (numMenor > numMayor) yield return 
        //            new ValidationResult($"{numMenor} debe ser menor que {numMayor}");

        //    if (!string.IsNullOrEmpty(Nombre))
        //    {
        //        var primeraLetra = Nombre[0].ToString();
        //        if (primeraLetra != primeraLetra.ToUpper())
        //            yield return new ValidationResult($"La primera de letra de {Nombre} debe ser mayúscula");
        //    }
        //}
    }
}
