using System.ComponentModel.DataAnnotations;

namespace Reunite.Annotations
{
    public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
    {

        private readonly string[] _extensions = extensions;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"This file extension is not allowed. Allowed: {string.Join(", ", _extensions)}");
                }
            }

            return ValidationResult.Success;
        }

    }
}