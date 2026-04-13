using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class StartsWithAttribute : ValidationAttribute
    {
        private readonly string substring;

        public StartsWithAttribute(string substring)
        {
            this.substring = substring;
        }

        public override bool IsValid(object value)
        {
            return value == null || value.ToString().StartsWith(substring);
        }
    }
}