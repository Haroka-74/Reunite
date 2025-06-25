using System.ComponentModel.DataAnnotations;

namespace Reunite.Annotations
{
    public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
    {
        private readonly string[] _extensions = extensions;

        public long MaxFileSize { get; set; } = long.MaxValue;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"This file extension is not allowed. Allowed: {string.Join(", ", _extensions)}");
                }

                if (file.Length > MaxFileSize)
                {
                    var maxSizeMB = MaxFileSize / (1024.0 * 1024.0);
                    return new ValidationResult($"File size exceeds the maximum allowed size of {maxSizeMB:F1} MB");
                }
            }

            return ValidationResult.Success;
        }
    }
}