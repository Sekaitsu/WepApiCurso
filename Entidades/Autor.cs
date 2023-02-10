using System.ComponentModel.DataAnnotations;
using WepApiCurso.Validaciones;

namespace WepApiCurso.Entidades
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength( maximumLength:120, ErrorMessage ="El campo {0}, no debe tener más de {1} caracteres")]
        [PrimeraLetraMaysucula] 
        public string Name { get; set; }

        public List<Libro> Libros { get; set; }

        /// <summary>
        /// Validacion de la entidad
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var primeraLetra = Name[0].ToString();
                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                    new string[] { nameof(Name) });
                }
            }
            //if(Menor > Mayor)
            //{
            //    yield return new ValidationResult(" no puede ser mas grande que el campo mayor",
            //        new string[] {nameof(Menor)});
            //}
        }
    }
}
