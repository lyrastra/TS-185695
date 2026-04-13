using System;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.Common.Enums.Attributes
{
    public class TrialCardTypesInfoAttribute : Attribute
    {
        public int Count { get; private set; }
        public TrialCardPeriodType TrialCardPeriodType { get; private set; }

        public TrialCardTypesInfoAttribute(int count, TrialCardPeriodType trialCardPeriodType)
        {
            Count = count;
            TrialCardPeriodType = trialCardPeriodType;
        }
    }
}