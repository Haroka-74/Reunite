using System.ComponentModel.DataAnnotations;

namespace Reunite.Annotations
{
    public class MaxFileSizeAttribute(int maxFileSize) : ValidationAttribute
    {

        private readonly int _maxFileSize = maxFileSize;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.Length > _maxFileSize)
            {
                return new ValidationResult($"Maximum allowed file size is {_maxFileSize / (1024 * 1024)} MB.");
            }

            return ValidationResult.Success;
        }

    }
}