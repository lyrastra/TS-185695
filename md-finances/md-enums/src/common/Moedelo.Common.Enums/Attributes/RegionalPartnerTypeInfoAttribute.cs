using System;
using Moedelo.Common.Enums.Enums.Partner;

namespace Moedelo.Common.Enums.Attributes
{
    public class RegionalPartnerTypeInfoAttribute : Attribute
    {
        public LeadSourceChannel[] LeadSourceChannels { get; private set; }

        public RegionalPartnerTypeInfoAttribute(params LeadSourceChannel[] leadSourceChannels)
        {
            LeadSourceChannels = leadSourceChannels;
        }
    }
}