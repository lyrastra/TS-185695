using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Api.Models.PaymentOrders
{
    public class NdsSaveDto : IValidatableObject
    {
        /// <summary>
        /// В том числе НДС
        /// </summary>
        public bool IncludeNds { get; set; }

        /// <summary>
        /// Ставка НДС:
        /// -1 - Без НДС, 
        /// 0 - НДС 0%, 
        /// 1 - НДС 10%, 
        /// 2 - НДС 18%, 
        /// 3 - НДС 10/110, 
        /// 4 - НДС 18/118, 
        /// 5 - НДС 20%,
        /// 6 - НДС 20/120,
        /// 7 - НДС 5%,
        /// 8 - НДС 5/105,
        /// 9 - НДС 7%,
        /// 10 - НДС 7/107,
        /// 11 - НДС 22%,
        /// 12 - НДС 22/122.
        /// </summary>
        public NdsType? Type { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal Sum { get; set; }



        private static readonly EnumValueAttribute EnumValueAttribute = new EnumValueAttribute(typeof(NdsType)) { AllowNull = true };
        private static readonly SumValueAttribute SumValueAttribute = new SumValueAttribute();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (IncludeNds)
            {
                Validator.TryValidateValue(Type, new ValidationContext(this, null, null) { MemberName = "Type" }, results, new[] { EnumValueAttribute });
                if (Type != NdsType.None && Type != NdsType.Nds0)
                {
                    Validator.TryValidateValue(Sum, new ValidationContext(this, null, null) { MemberName = "Sum" }, results, new[] { SumValueAttribute });
                }
            }
            return results;
        }
    }
}
