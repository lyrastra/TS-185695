using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Money.Providing.Api.Models
{
    public class NdsDto : IValidatableObject
    {
        /// <summary>
        /// В том числе НДС
        /// </summary>
        public bool IncludeNds { get; set; }

        /// <summary>
        /// Ставка НДС
        /// </summary>
        public NdsType? Type { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? Sum { get; set; }



        private static readonly EnumValueAttribute EnumValueAttribute = new EnumValueAttribute(typeof(NdsType)) { AllowNull = true };
        private static readonly RequiredValueAttribute RequiredValueAttribute = new RequiredValueAttribute();
        private static readonly SumValueAttribute SumValueAttribute = new SumValueAttribute();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (IncludeNds)
            {
                Validator.TryValidateValue(Type, new ValidationContext(this, null, null) { MemberName = "Type" }, results, new[] { EnumValueAttribute });
                if (Type != NdsType.None && Type != NdsType.Nds0)
                {
                    Validator.TryValidateValue(Sum, new ValidationContext(this, null, null) { MemberName = "Sum" }, results, new[] { RequiredValueAttribute });
                    Validator.TryValidateValue(Sum, new ValidationContext(this, null, null) { MemberName = "Sum" }, results, new[] { SumValueAttribute });
                }
            }
            return results;
        }
    }
}
