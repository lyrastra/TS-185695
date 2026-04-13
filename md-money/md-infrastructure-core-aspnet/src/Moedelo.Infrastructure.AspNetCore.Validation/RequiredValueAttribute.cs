using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredValueAttribute : RequiredAttribute
    {
        public RequiredValueAttribute()
        {
            ErrorMessage = "Это поле должно быть заполнено.";
        }
    }
}
