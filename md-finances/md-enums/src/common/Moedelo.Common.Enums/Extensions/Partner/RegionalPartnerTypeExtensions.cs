using System.Reflection;
using Moedelo.Common.Enums.Attributes;
using Moedelo.Common.Enums.Enums.Partner;

namespace Moedelo.Common.Enums.Extensions.Partner
{
    public static class RegionalPartnerTypeExtensions
    {
        public static LeadSourceChannel[] GetLeadSourceChannels(this RegionalPartnerType regionalPartnerType)
        {
            var attr = EnumMethodExtensions.GetEnumAttribute<RegionalPartnerTypeInfoAttribute, RegionalPartnerType>(regionalPartnerType);
            if (attr == null)
            {
                throw new CustomAttributeFormatException($"regionalPartnerType: {regionalPartnerType} not fount in RegionalPartnerType type");
            }

            return attr.LeadSourceChannels;
        }
    }
}