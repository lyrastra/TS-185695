using Moedelo.Infrastructure.AspNetCore.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class ChargePaymentSaveDto : IValidatableObject
    {
        /// <summary>
        /// Начисление в ЗП
        /// </summary>
        [IdLongValue]
        [DefaultValue(null)]
        public long? ChargeId { get; set; }

        /// <summary>
        /// Связь выплаты с начислением (фиксируется в ЗП)
        /// </summary>
        [IdIntValue]
        [DefaultValue(null)]
        public int? ChargePaymentId { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }


        private static readonly RequiredValueAttribute RequiredValueAttribute = new RequiredValueAttribute();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (ChargeId > 0)
            {
                Validator.TryValidateValue(Description, new ValidationContext(this, null, null) { MemberName = "Description" }, results, new[] { RequiredValueAttribute });
            }
            return results;
        }
    }
}
