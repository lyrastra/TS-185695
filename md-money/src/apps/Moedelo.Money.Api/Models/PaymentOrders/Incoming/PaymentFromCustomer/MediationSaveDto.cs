using Moedelo.Infrastructure.AspNetCore.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer
{
    public class MediationSaveDto : IValidatableObject
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