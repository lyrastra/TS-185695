using Moedelo.Common.Enums.Attributes;

namespace Moedelo.Common.Enums.Enums.Partner
{
    public enum RegionalPartnerType
    {
        [RegionalPartnerTypeInfo(LeadSourceChannel.Own, LeadSourceChannel.DirectSales)]
        None = 0,

        [RegionalPartnerTypeInfo(LeadSourceChannel.Partner)]
        Usual = 1,

        [RegionalPartnerTypeInfo(LeadSourceChannel.Bank)]
        Bank = 2,

        // [RegionalPartnerTypeInfo(LeadSourceChannel.???)]
        Franchise = 3,
        
        /// <summary>
        /// специальное значение для "Моё Дело"
        /// </summary>
        MoeDelo = 4,
        
        /// <summary>
        /// специальное значение для "ГлавУчёт"
        /// </summary>
        GlavUchet = 5 
    }
}