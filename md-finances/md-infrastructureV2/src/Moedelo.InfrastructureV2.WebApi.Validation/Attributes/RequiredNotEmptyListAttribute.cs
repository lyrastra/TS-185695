using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class RequiredNotEmptyListAttribute : RequiredAttribute
    {
        public RequiredNotEmptyListAttribute()
        {
            ErrorMessage = "Список не может быть пустым (должен содержать хотя бы один элемент)";
        }

        public override bool IsValid(object value)
        {
            var list = value as IEnumerable<object>;

            return base.IsValid(value) && list != null && list.Any();
        }
    }
}