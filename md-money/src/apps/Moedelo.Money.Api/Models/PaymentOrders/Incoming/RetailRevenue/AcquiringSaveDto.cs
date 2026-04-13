using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.RetailRevenue
{
    public class AcquiringSaveDto : IValidatableObject
    {
        /// <summary>
        /// Признак: эквайринг
        /// </summary>
        public bool IsAcquiring { get; set; }

        /// <summary>
        /// Комиссия (эквайринг)
        /// </summary>
        public decimal? CommissionSum { get; set; }

        /// <summary>
        /// Дата комиссии
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? CommissionDate { get; set; }


        private static readonly SumValueAttribute SumValueAttribute = new SumValueAttribute();
        private static readonly DateValueAttribute DateValueAttribute = new DateValueAttribute();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (IsAcquiring)
            {
                Validator.TryValidateValue(CommissionSum, new ValidationContext(this, null, null) { MemberName = "CommissionSum" }, results, new[] { SumValueAttribute });
                Validator.TryValidateValue(CommissionDate, new ValidationContext(this, null, null) { MemberName = "CommissionDate" }, results, new[] { DateValueAttribute });
            }
            return results;
        }
    }
}