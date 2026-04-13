using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Providing.Api.Models
{
    public class MediationDto : IValidatableObject
    {
        /// <summary>
        /// Признак: посредничество
        /// </summary>
        public bool IsMediation { get; set; }

        /// <summary>
        /// Комиссия посредника
        /// </summary>
        public decimal? CommissionSum { get; set; }

        private static readonly SumValueAttribute SumValueAttribute = new SumValueAttribute();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (IsMediation)
            {
                Validator.TryValidateValue(CommissionSum, new ValidationContext(this, null, null) { MemberName = "CommissionSum" }, results, new[] { SumValueAttribute });
            }
            return results;
        }
    }
}
