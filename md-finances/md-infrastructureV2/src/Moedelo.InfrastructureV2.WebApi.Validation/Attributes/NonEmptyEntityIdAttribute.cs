using System;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [Obsolete("Use RequiredEntityIdAttribute or OptionalEntityIdAttribute")]
    public class NonEmptyEntityIdAttribute : PositiveNumberAttribute
    {
        public NonEmptyEntityIdAttribute() : base("Значение должно быть ненулевым целочисленным идентификатором")
        {
        }
    }
}