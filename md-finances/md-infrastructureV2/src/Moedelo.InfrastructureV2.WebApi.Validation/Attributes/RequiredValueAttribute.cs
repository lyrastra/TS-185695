using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredValueAttribute : RequiredAttribute
    {
        public const string Message = "Это поле должно быть заполнено";

        public RequiredValueAttribute()
        {
            ErrorMessage = Message;
        }
    }
}