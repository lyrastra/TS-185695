using Moedelo.Common.Enums.Attributes;

namespace Moedelo.Common.Enums.Enums.Billing
{
    /// <summary> Увеличение триального доступа. </summary>
    public enum TrialCardTypes
    {
        [TrialCardTypesInfo(1, TrialCardPeriodType.Month)]
        Month = 1,

        [TrialCardTypesInfo(3, TrialCardPeriodType.Month)]
        ThreeMonth = 2,

        [TrialCardTypesInfo(2, TrialCardPeriodType.Month)]
        TwoMonth = 3,

        [TrialCardTypesInfo(7, TrialCardPeriodType.Day)]
        Week = 4,
        
        [TrialCardTypesInfo(6, TrialCardPeriodType.Month)]
        SixMonth = 5
    }
}