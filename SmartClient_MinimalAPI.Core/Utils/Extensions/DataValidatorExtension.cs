using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace SmartClient.MinimalAPI.Core.Utils.Extensions
{
    public static class DataValidatorExtension
    {
        public static (List<ValidationResult>? Results, bool IsValid) DataAnnotationsValidate<T>(this T? model)where T : class
        {
            if(model ==  null)
            {
                return (null, false);
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);

            var isValid = Validator.TryValidateObject(model, context, results, true);

            return (results, isValid);
        }
    }
}
