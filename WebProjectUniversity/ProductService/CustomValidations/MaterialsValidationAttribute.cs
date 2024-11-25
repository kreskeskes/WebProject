using System.ComponentModel.DataAnnotations;

namespace ProductService.CustomValidations
{
    public class MaterialsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Dictionary<string, float> materials)
            {
                float total = materials.Values.Sum();

                if (total > 100)
                {
                    return new ValidationResult("The total percentage of all materials cannot exceed 100.");
                }
                else if (total < 0)
                {
                    return new ValidationResult("The total percentage of all materials cannot be less than 0.");
                }
                else if(total!=100)
                {
                    return new ValidationResult("The total percentage of all materials should be exctly 100.");

                }
            }

            return ValidationResult.Success;
        }
    }
}
