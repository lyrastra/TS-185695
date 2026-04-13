using System;
using System.Linq;
using Moedelo.Common.Enums.Attributes;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.Common.Enums.Extensions.TrialCard
{
    public static class TrialCardTypesExtentions
    {
        public static DateTime RaiseTrial(this DateTime startDate, TrialCardTypes trialCardTypes)
        {
            var memInfo = trialCardTypes.GetType().GetMember(trialCardTypes.ToString()).FirstOrDefault();
            if (memInfo == null)
            {
                return startDate;
            }

            var attribute = (TrialCardTypesInfoAttribute)memInfo.GetCustomAttributes(typeof(TrialCardTypesInfoAttribute), false).FirstOrDefault();

            if (attribute == null)
            {
                return startDate;
            }
            
            switch (attribute.TrialCardPeriodType)
            {
                case TrialCardPeriodType.Day:
                    return startDate.AddDays(attribute.Count);
                case TrialCardPeriodType.Month:
                    return startDate.AddMonths(attribute.Count);
                default:
                    return startDate;
            }
        }
    }
}