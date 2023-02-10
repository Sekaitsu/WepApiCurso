using System.ComponentModel.DataAnnotations;
using WepApiCurso.Controllers;

namespace WepApiCurso.Validaciones
{
    public class PrimeraLetraMaysuculaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //se crea la logica de validación
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeraLetra = value.ToString()[0].ToString();

            if(primeraLetra != primeraLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayusculas");
            }
            return ValidationResult.Success;
        }
    }
}
